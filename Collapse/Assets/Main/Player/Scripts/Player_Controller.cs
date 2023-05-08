using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.ProBuilder;
using System;
using Unity.VisualScripting;

[System.Serializable]
public class Player_Controller : MonoBehaviour
{
   
    public Camera[] allCams;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private GameManager gameManager;
    [SerializeField]private bool player_grounded;
    private bool player_dead;
    public float player_speed = 10.0f;
    private float dodge_speed = 10.0f;
    private float player_jumpHeight = 2.0f;
    private float gravity = -9.81f;
    [SerializeField] private float gravity_multiplier = 3.0f;
    private float _velocity;
    Rigidbody rig_body;
    Vector2 turn_vec;
    float sensitivity = 0.5f;
    float angle = 0;
    bool inHub = false;
   
    [Header("Placements")]
    public float y_offset = 1.0f;
    public double direction_offset = 90;
    public bool north_face = false;
    public bool west_face = false;
    public bool east_face = false;
    public bool south_face = false;

    [Header("Weapon")]
    public GameObject weapon_slot;
    public GameObject holster_hip_slot;
    public GameObject holster_back_slot;
    public Weapon_Func weapon;
    public List<Weapon_Func> weapons;
    public Transform hand;
    public bool has_Weapon;
    public bool weapon_change;
    public bool anim_change = false;
    public int current_weapon = 0;
    public int num_of_weapons = 0;

    [Header("Stats")]
    public int hp = 100;
    public int max_hp = 100;
    int mp = 100;

    [SerializeField] public int speed = 1;
    [SerializeField] Transform check_ground;
    [SerializeField] LayerMask ground;

    public GameObject nearestObj;
    float nearestDist = 100000;

    public NavMeshAgent agent;
    public GameObject obj;
    public Find_Objective questArrow;
    public Find_Objective questArrowPrefab;
    public Player_Equipment equip;
    public Animator anim;
    public GameObject facingDirection;
    public GameObject facing;

    bool god = false;
    


    // Start is called before the first frame update
    void Start()
    {

;
        controller = GetComponent<CharacterController>();
    
        gameManager = GameManager.instance;
        equip = Player_Equipment.instance;
        //questArrow = GetComponentInChildren<Find_Objective>();
        if (questArrow == null)
        {
            questArrow = Instantiate(questArrowPrefab, transform.position, Quaternion.identity);
            if(!gameManager.inHub)
                questArrow.SetObjective();
        }
        //gameObject.GetComponentInChildren<Camera>().gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (obj != null) 
        {
            //Debug.Log(Vector3.Distance(transform.position, obj.transform.position));
        }
        nearestObj = GetNearestGameObject();

        if (Input.GetKeyUp("e"))
        {
            Interact_with();
        }

        if (gameManager.gamePaused) return;

        if (weapon_change)
        {
            anim_change = true;
            weapon_change = false;
        }
        num_of_weapons = weapons.Count;

        aim_mouse();


        //if we collide with a weapon. Pick it up and set it as a child

        if (Input.GetButton("Fire1") && !gameManager.inHub) 
        {
            //check to make sure we have a weapon
            if (weapon != null)
            {

                if (weapon.CanShoot())
                    weapon.Fire_Weapon(this.gameObject);
                else if(!weapon.CanShoot() && weapon.current_ammo <= 0) 
                {
                    weapon.reload_needed = true; 
                }
            }
        }
        // drop weapon and release it from its slot;
        if(Input.GetKeyUp("q") && weapon != null) 
        {
            bool still_has = false;

            if (weapons.Count > 1)
                still_has = true;

            current_weapon--;
            if (current_weapon < 0)
                current_weapon = 1;

            Weapon_Func temp_remove = weapon;
            //swap weapon if there is one
            if (still_has)
            {
                weapons[current_weapon].gameObject.SetActive(true);
                weapon = weapons[current_weapon];
                current_weapon = 0;
                weapon_change = true;
            }
            else
            {
                weapon = null;
                has_Weapon = false;
            }
            //remove from slot;
            weapons.Remove(temp_remove);

            //unattach from player
            temp_remove.transform.SetParent(null);

            
        }

        if(Input.GetKeyUp("t")) 
        {
            anim_change = true;
            equip.SwapWeapons();

        }

        if (Input.GetKeyUp("r")) 
        {
            weapon.reload_needed = true;
        }



        //debug toggle
        if (Input.GetKeyUp("`")) 
        {
            gameManager.GetComponent<DebugController>().ToggleDebug();
        }
       
    }

    
    private void FixedUpdate()
    {
        if (gameManager.gamePaused) return;
        ApplyGravity();
        Movement();
    
    }

    private void ApplyGravity() 
    {
        if (controller.isGrounded)
        {
            _velocity = -1.0f;
        }
        else
        {

            _velocity += gravity + gravity_multiplier * Time.deltaTime;
        }
        transform.position = new Vector3(transform.position.x,  _velocity, transform.position.z); 
        
    }
    private void Movement() 
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), transform.position.y, Input.GetAxis("Vertical"));
        
        if (move != Vector3.zero)
        {
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 30, 0));
            var skewed = matrix.MultiplyPoint3x4(move);
            checkDirection();
            if (player_sprinting())
            {
                controller.Move(skewed * Time.deltaTime * (player_speed * 3));
            }
            else
                controller.Move(skewed * Time.deltaTime * player_speed);

            //if(transform.rotation.z != 0)
                gameObject.transform.forward = skewed * transform.rotation.z;

            UpdateAnimator(move);
        }



        if (move.x == 0 && move.z == 0)
            anim.SetBool("NoMovement", true);

    }
    
    void checkDirection() 
    {
       
        Vector3 forward = transform.forward;
        forward.y = 0;
        float headingAngle = Quaternion.LookRotation(forward).eulerAngles.y;

        //if (headingAngle > 180f) 
        //    headingAngle -= 360f;

        //north_face = within_range(headingAngle, 45, 315);
        //west_face = within_range(headingAngle, -35, 44);
        //east_face = within_range(headingAngle, -131, 150);
        //south_face = within_range(headingAngle, -130, -34);
        north_face = facing_compass(headingAngle) == "North";
        east_face = facing_compass(headingAngle) == "East";
        south_face = facing_compass(headingAngle) == "South";
        west_face = facing_compass(headingAngle) == "West";

        
        //
        //Debug.Log(headingAngle);
        //if (headingAngle > 0)
        //    return false;
        //else
        //    return true;
    }

    string facing_compass(double headingAngle) 
    {
        string compass = "N/A";

        if ((headingAngle > 315 && headingAngle <= 360) || (headingAngle > 0 && headingAngle <= 45))
            compass = "North";
        else if (headingAngle > 45 && headingAngle <= 135)
            compass = "East";
        else if (headingAngle > 135 && headingAngle <= 225)
            compass = "South";
        else if (headingAngle > 225 && headingAngle <= 315)
            compass = "West";

        return compass;
    }
    bool within_range(double target, double min, double max) 
    {
       return (target >= min && target <= max);
    }
    void UpdateAnimator(Vector3 move) 
    {
        anim.SetBool("MovingForward", false);
        anim.SetBool("MovingBackwards", false);
        anim.SetBool("MovingLeft", false);
        anim.SetBool("MovingRight", false);
        anim.SetBool("Running", false);
        anim.SetBool("NoMovement", false);

      
       Vector3 localVel = transform.InverseTransformDirection(move);

        //Debug.Log(localVel);
        anim.SetBool("Running", player_sprinting());

        if (south_face)
        {
            if (localVel.z > 0.1f)
                anim.SetBool("MovingBackwards", true);
            else if (localVel.z < -0.1f)
                anim.SetBool("MovingForward", true);
            else if (localVel.x > 0.1f)
                anim.SetBool("MovingLeft", true);
            else if (localVel.x < -0.1f)
                anim.SetBool("MovingRight", true);
        }
        else if (north_face)
        {
            if (localVel.z > 0.1f)
                anim.SetBool("MovingForward", true);
            else if (localVel.z < -0.1f)
                anim.SetBool("MovingBackwards", true);
            else if (localVel.x > 0.1f)
                anim.SetBool("MovingRight", true);
            else if (localVel.x < -0.1f)
                anim.SetBool("MovingLeft", true);
        }
        else if(west_face) 
        {
            if (localVel.z > 0.1f)
                anim.SetBool("MovingRight", true);
            else if (localVel.z < -0.1f)
                anim.SetBool("MovingLeft", true);
            else if (localVel.x > 0.1f)
                anim.SetBool("MovingBackwards", true);
            else if (localVel.x < -0.1f)
                anim.SetBool("MovingForward", true);
        }
        else if (east_face) 
        {
            if (localVel.z > 0.1f)
                anim.SetBool("MovingLeft", true);
            else if (localVel.z < -0.1f)
                anim.SetBool("MovingRight", true);
            else if (localVel.x > 0.1f)
                anim.SetBool("MovingForward", true);
            else if (localVel.x < -0.1f)
                anim.SetBool("MovingBackwards", true);
        }
        

        if (localVel.x == 0 && localVel.z == 0)
            anim.SetBool("NoMovement", true);

    }
    bool player_on_ground() 
    {
        return Physics.CheckSphere(check_ground.position, .1f, ground);
    }
    
    bool player_sprinting() 
    {
        
        return (Input.GetKey(KeyCode.LeftShift)) ? true : false;

    }

    void aim_mouse() 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float distance;
      

        if (groundPlane.Raycast(ray, out distance)) 
        {
            angle = Vector3.Angle(transform.position, transform.position - ray.GetPoint(distance));
      
            Vector3 get_point = ray.GetPoint(distance);
            aim_at(get_point);
        }
    }

    void aim_at(Vector3 look_At) 
    {
        Vector3 correct_height = new Vector3(look_At.x, transform.position.y, look_At.z);
        transform.LookAt(correct_height);
    }

    public void damage_taken(int damage) 
    {
        if(!god)
            hp -= damage;

        if (hp <= 0) 
            death();
        
    }

    public void death() 
    {
        //restart scene
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //gameManager.GetComponent<LevelControl>().RestartLevel();
        gameManager.missionSuccess = false;
        gameManager.GetComponent<LevelControl>().LoadLevel("RewardScene");
    }

    private GameObject GetNearestGameObject() 
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Interact");
        nearestDist = 10000;

        for(int i = 0; i < allObjects.Length; i++) 
        {
            float distance = Vector3.Distance(transform.position, allObjects[i].transform.position);

            if(distance < nearestDist) 
            {
                nearestObj = allObjects[i];
                nearestDist = distance;
            }
        }

        return nearestObj;
    }

    private void Interact_with() 
    {
        if (nearestObj == null)
            return;

        var interactable = nearestObj.GetComponent<IInteractable>();

        if (interactable == null)
            return;

        float dist = Vector3.Distance(transform.position, nearestObj.transform.position);
        float interact_dist = 2.0f;
        if (!inHub)
        {
            interact_dist = 4.0f;
        }
        
        if(dist <= interact_dist)
            interactable.Interact();
    }

    public void SetWeapons() 
    {
        weapons.Clear();
        for(int i = 0; i < Player_Equipment.instance.weapons.Count; i++) 
            weapons.Add(Player_Equipment.instance.weapons[i]);
        
        weapon = weapons[0];
    }

    public List<Weapon_Func> GetWeapons() { return weapons; }


    private void OnCollisionEnter(Collision collision)
    {
        
        Debug.Log(collision.gameObject.name);
    }

    #region cheats

    public void GodMode() 
    {
        god = !god;
    }
    #endregion

    #region maybe_code
    /*
     * 
     * 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (Input.GetMouseButtonDown(1))
            {
              agent.destination = hit.point;

            }
         }

            if (player_sprinting())
        {
            agent.speed = 100;
        }
        else
            agent.speed = player_speed;
     */
    #endregion
}
