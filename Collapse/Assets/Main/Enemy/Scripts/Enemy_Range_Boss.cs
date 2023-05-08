using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using UnityEngine.Animations.Rigging;

public class Enemy_Range_Boss : Base_Enemy_Actions
{
    [Header("Range Stats")]
    public int bullet_speed = 3;
    public float fire_angle = 10;
    public float check_angle = 0;
    public float changePositions_timer = 10;
    public float flee_distance = 10;
    public float reposition_distance = 10;
    public MultiAimConstraint spineRig;
    public MultiAimConstraint ShoulderRig;
    public MultiAimConstraint HandRig;
    public RigBuilder rigB;
   

    public int burstAmount = 5;
    
    // Start is called before the first frame update
    protected override void Start() => base.Start();

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        //if (faceTarget)
        //{
        //    FaceTarget(Target.transform.position);

        //    float dist_player = Vector3.Distance(transform.position, Target.transform.position);

        //    if (dist_player <= attackRange)
        //    {
        //        agent.isStopped = true;
        //        if (weapon.CanShoot())
        //            weapon.Fire_Weapon();
        //    }
        //    else
        //    {
        //        agent.isStopped = false;
        //        ChasePlayer();
        //    }
        //}
    }


    void takeShot() 
    {
        if(weapon.CanShoot()) 
        {
            weapon.Fire_Weapon(this.gameObject);
        }
    }

    void range_attack_down() 
    {
        isAttacking = false;
        //SetParams(false, false, false);
    }

 
    #region Range_States
    protected override void Chase()
    {
        base.Chase();
        
        
        //agent.SetDestination(target.transform.position);


    }

    protected override void Chase_start()
    {
        base.Chase_start();
        get_target = true;
    }

    protected override void Chase_stop()
    {
        base.Chase_stop();
    }

    protected override void Attack()
    {
        base.Attack();
        
        changePositions_timer -= Time.deltaTime;
   
        if (changePositions_timer <= 0.0f)
        {
            float reposition_chance = Random.Range(0.0f, 1.0f);
            if (reposition_chance <= 1.0f)
            {
                Debug.Log("Changing Positions");
                brain.Push_State(Reposition, Reposition_start, Reposition_stop);
            }
            else
                changePositions_timer = 10.0f;
        }
        else
        {
            FaceTarget(target.transform.position);
            //transform.LookAt(target.transform.position);

            RaycastHit hit;
            Ray shoot_ray = new Ray(weapon.muzzle.transform.position, weapon.muzzle.transform.forward);

            bool facing_player = Physics.Raycast(shoot_ray, out hit, attackRange);

            //bool facing_player = Vector3.Angle(Target.transform.forward, transform.position - Target.transform.position) <= fire_angle;
            //check_angle = Vector3.Angle(Target.transform.forward, transform.position - Target.transform.position);
            Debug.DrawLine(weapon.muzzle.transform.position, weapon.muzzle.transform.forward * attackRange, Color.red, 0.0f, true);
            Debug.DrawLine(transform.position, transform.forward * attackRange, Color.red, 0.0f, true);

            if (facing_player && !isAttacking)
            {
                SetParams(false, false, false);
                isAttacking = true;
                Invoke("takeShot", 1f);
                Invoke("range_attack_down", 2.533f);
                //if (weapon.CanShoot())
                //{
                //    for(int i = 0; i < burstAmount; i++) 
                //    { 
                //        weapon.Fire_Weapon(this.gameObject);
                //    }
                //}
            }
        }
    }

    protected void Reposition() 
    {
        brain.current_state = "Reposition";
        state_text.text = brain.current_state;
        if (agent.remainingDistance <= 0.25f)
        {
            changePositions_timer = 10.0f;
            brain.Pop_State();
        }
    }

    protected void Reposition_start() 
    {
        agent.ResetPath();
        float dist_to = Vector3.Distance(transform.position, target.transform.position);
        SetParams(true, true, false);
        agent.SetDestination(GetLocation(90, reposition_distance, reposition_distance));
    }

    protected void Reposition_stop() 
    {
        SetParams(false, false, false);
    }
    #endregion

    float GetDestinationAngle(Vector3 targetPos) 
    {
        return Vector3.Angle(transform.position - target.transform.position, transform.position + targetPos);
    }

    Vector3 GetLocation(float angle_to, float loc_distance, float playerDistance) 
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * loc_distance), out hit, loc_distance, NavMesh.AllAreas);
        int i = 0;
        while(GetDestinationAngle(hit.position) > angle_to || playerDistance < 10) 
        {
            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * loc_distance), out hit, loc_distance, NavMesh.AllAreas);
            i++;
            if (i == 30)
                break;
        }

        return hit.position;
    }
}
