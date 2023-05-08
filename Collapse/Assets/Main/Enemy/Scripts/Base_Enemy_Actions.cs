using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System.Xml;
using Unity.VisualScripting;


public class Base_Enemy_Actions : MonoBehaviour
{
    public GameManager manager;
    private SphereCollider sphereCollider;
    public GameObject Target;
    public bool faceTarget = false;

    public float turn_speed = 30f;

    [Header("Stats")]
    public int hp;
    public int maxHP;

    [Header("Cost")]
    public int Cost = 0; //the cost of this particular enemy during wave spawn event and when bosses summon help.

    [Header("Movement")]
    public float moveSpeed;
    public float attackRange;
    public float chaseRange;
    public float yPathOffset;
    public Animator anim;
    

    private List<Vector3> path;
    private Vector3 last_known_position;
    private Vector3 starting_position;


    protected Weapon_Func weapon;
    protected Player_Controller target;
    protected bool isDead;
    protected bool isAttacking;
    protected bool animation_done;
    protected bool isAlert;
    public bool tookDamage;
    public bool get_target = false;

    protected StateMachine brain;
    protected Enemy_Awareness sensory;
    public NavMeshAgent agent;

    private float changeMind;
    private float searchTime = 15;
    public float chase_timer = 30;
    public float max_chase_time = 30;

    public float dist_player = 0;
    public bool WaveAttack = false;
    public bool isQuestObj = false;
    [Header("Debug")]
    public UnityEngine.UI.Text state_text;
    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        weapon = GetComponentInChildren<Weapon_Func>();
        target = FindObjectOfType<Player_Controller>();
        brain = GetComponent<StateMachine>();
        sensory = GetComponentInChildren<Enemy_Awareness>();

        if (brain != null)
            brain.Push_State(Idle, Idle_start, Idle_stop);

        if (WaveAttack)
        {
            brain.Push_State(Chase, Chase_start, Chase_stop);
        }

        InvokeRepeating("CalculatePath", 0.0f, 0.8f);
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        if (brain.current_state == "Chase" || brain.current_state == "Attack")
        {

            chase_timer -= Time.deltaTime;

            if (chase_timer <= 0)
            {
                tookDamage = false;
                faceTarget = false;
                isAlert = false;
            }
        }


    }

    public GameObject GetTarget()
    {
        return target.gameObject;
    }

 
    #region actions
    protected virtual void FaceTarget(Vector3 target_direction)
    {
        Vector3 look_target = target_direction - transform.position;

        look_target.y = 0;

        Quaternion rotate = Quaternion.LookRotation(look_target);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotate, turn_speed * Time.deltaTime);
        
    }
    protected virtual void ChasePlayer() 
    {
        if (path.Count == 0)
            return;

        Vector3 y_off = new Vector3(0, yPathOffset, 0);
        transform.position = Vector3.MoveTowards(transform.position, path[0] + y_off, moveSpeed * Time.deltaTime);

        if (transform.position == path[0] + y_off)
            path.RemoveAt(0);
    }

    protected virtual void last_known() 
    {
        if (path.Count == 0)
            return;

        Vector3 y_off = new Vector3(0, yPathOffset, 0);
        transform.position = Vector3.MoveTowards(transform.position, path[0] + y_off, moveSpeed * Time.deltaTime);

        if (transform.position == path[0] + y_off)
            path.RemoveAt(0);
    }
    #endregion

    protected virtual void CalculatePath() 
    {
        if (!isAttacking)
        {

            NavMeshPath nav_path = new NavMeshPath();
            NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, nav_path);

            path = nav_path.corners.ToList();
        }
    }

    protected virtual void clearPath() 
    {
        path.Clear();
    }

    protected void SetParams(bool move, bool chase, bool attack) 
    {
        anim.SetBool("Moving", move);
        anim.SetBool("Chasing", chase);
        anim.SetBool("Attacking", attack);
    }
    public virtual void damage_taken(int damage) 
    {
        hp -= damage;
        chase_timer = max_chase_time;
        if (brain.current_state != "Chase" && brain.current_state != "Attack" && brain.current_state != "Reposition")
        {
            if(!isAlert)
                isAlert = true;

            tookDamage = true;
            faceTarget = true;

            brain.Push_State(Chase, Chase_stop, Chase_stop);
        }

        if (hp <= 0)
            Death();
    }

    void Death() 
    {
        if(isQuestObj)
            QuestManager.instance.current_to++;
        Destroy(gameObject);    
    }

    #region states
    void Idle() 
    {
        brain.current_state = "Idle";
        state_text.text = brain.current_state;
        changeMind -= Time.deltaTime;
        SetParams(false, false, false);
        if (faceTarget) 
        {
            brain.Push_State(Chase, Chase_start, Chase_stop);
        }
        else if(changeMind <= 0) 
        {
            brain.Push_State(Wander, Wander_start, Wander_stop);
            changeMind = Random.Range(4, 10);
        }
    }

    void Idle_start() 
    {
        agent.ResetPath();
    }
    
    void Idle_stop() 
    {

    }
    
    void Wander() 
    {
        brain.current_state = "Wander";
        state_text.text = brain.current_state;
        if (agent.remainingDistance <= 0.5f) 
        {
            agent.ResetPath();
            brain.Push_State(Idle, Idle_start, Idle_stop);
        }

        if(faceTarget) 
        {
            brain.Push_State(Chase, Chase_start, Chase_stop);
        }
    }

    void Wander_start() 
    {
        Vector3 wanderDir = (Random.insideUnitSphere * 4f) + transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(wanderDir, out navHit, 3f, NavMesh.AllAreas);
        Vector3 wanderDes = navHit.position;
        SetParams(true, false, false);
        agent.SetDestination(wanderDes); 
    }

    void Wander_stop() 
    {

    }

    protected virtual void Chase() 
    {
        brain.current_state = "Chase";
        state_text.text = brain.current_state;
        
        if (faceTarget)
        {
            //FaceTarget(target.transform.position);

           

            dist_player = Vector3.Distance(transform.position, target.transform.position);

            if (dist_player <= attackRange)
            {
               
                agent.isStopped = true;
                agent.SetDestination(transform.position);

                if (!isAttacking)
                {
                    //isAttacking = true;
                    brain.Push_State(Attack, Attack_start, null);
                }
            }
            else
            {
                agent.isStopped = false;
                //isAttacking = false;
                //ChasePlayer();
                SetParams(true, true, false);
                agent.SetDestination(target.transform.position);
            }
        }
        else
        {
            //isAttacking = false;
            brain.Push_State(Idle, Idle_start, Idle_stop);
        }
    }

    protected virtual void Chase_start() 
    {
        agent.ResetPath();
    }

    protected virtual void Chase_stop() 
    {

    }

    protected virtual void Attack()
    {
        brain.current_state = "Attack";
        state_text.text = brain.current_state;
        dist_player = Vector3.Distance(transform.position, target.transform.position);
        
        if (dist_player > attackRange)
            brain.Pop_State();
    }

    protected virtual void Attack_start() 
    {
        SetParams(false, false, true);
        animation_done = false;
        agent.ResetPath();
    }

    protected virtual void Attack_stop() 
    {
        //SetParams(false, false, false);
    }
    /*
    protected virtual void Investigate() 
    {

        brain.current_state = "Investigate";
        state_text.text = brain.current_state;
        agent.SetDestination(last_known_position);

        if (faceTarget)
        {
            brain.Push_State(Chase, Chase_start, Chase_stop);
            return;
        }

        float dist_to = Vector3.Distance(transform.position, last_known_position);
        if(dist_to <= 0.5f) 
        {
            searchTime = Random.Range(15,20);
            changeMind = 5;
            brain.Push_State(Search, Search_start, null);
        }

    }
    protected virtual void Investigate_start() 
    {
        agent.ResetPath();
    }

    protected virtual void Investigate_stop() 
    {

    }

    protected virtual void Search() 
    {
        searchTime -= Time.deltaTime;
        changeMind -= Time.deltaTime;
        brain.current_state = "Search";
        state_text.text = brain.current_state;

        if (faceTarget)
        {
            brain.Push_State(Chase, Chase_start, Chase_stop);
            return;
        }

        if (searchTime <= 0)
            brain.Push_State(Return, Return_start, null);
        else if (changeMind <= 0)
        {

            Vector3 serachDir = (Random.insideUnitSphere * 4f) + last_known_position;
            NavMeshHit navHit;
            NavMesh.SamplePosition(serachDir, out navHit, 3f, NavMesh.AllAreas);
            Vector3 searchDes = navHit.position;
            agent.SetDestination(searchDes);
            changeMind = Random.Range(5, 6);
        }

        
     
    }

    protected virtual void Search_start() 
    {
        agent.ResetPath();
    }

    protected virtual void Search_stop() 
    {

    }
    protected virtual void Return() 
    {
        brain.current_state = "Return";
        state_text.text = brain.current_state;
        float dist_to = Vector3.Distance(transform.position, starting_position);

        if (faceTarget)
        {
            brain.Push_State(Chase, Chase_start, Chase_stop);
            return;
        }

        if (dist_to < 0.3f) 
        {
            brain.Push_State(Idle, Idle_start, Idle_stop);
        }
    }

    protected virtual void Return_start() 
    {
        agent.ResetPath();
        agent.SetDestination(starting_position);
    }

    protected virtual void Return_stop() 
    {

    }
    */
    #endregion
}
