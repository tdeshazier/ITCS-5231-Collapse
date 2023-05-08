using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Melee_Boss : Base_Enemy_Actions
{
    [Header("Melee Stats")]
    public int AttackSpeed = 3;
    public int AttackDamage = 3;

    

    // Start is called before the first frame update
    protected override void Start() => base.Start();

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        /*
        if (faceTarget)
        {
            FaceTarget(Target.transform.position);

            float dist_player = Vector3.Distance(transform.position, Target.transform.position);

            if (dist_player <= attackRange)
            {
                agent.isStopped = true;
                if (!isAttacking)
                    melee_attack();
            }
            else
            {
                agent.isStopped = false;
                ChasePlayer();
            }
        }
        */
    }



    private void TryDamage()
    {
        if (Target != null && !animation_done)
        {
            //SetParams(false, false, true);
            float dist_player = Vector3.Distance(transform.position, Target.transform.position);
            if (dist_player <= attackRange)
            {
                target.damage_taken(AttackDamage);
            }
        }
    }

    void melee_attack_down() 
    {
        isAttacking= false;
        SetParams(false, false, false);
        animation_done = true;
    }

    #region Melee_States
    protected override void Chase()
    {
        base.Chase();
        //agent.SetDestination(target.transform.position);
    }

    protected override void Chase_start()
    {
        base.Chase_start();
    }

    protected override void Chase_stop()
    {
        base.Chase_stop();
    }

    protected override void Attack()
    {
        base.Attack();

        if (target == null)
            return;

        FaceTarget(target.transform.position);
        RaycastHit hit;
        Ray shoot_ray = new Ray(transform.position, transform.forward);

        bool facing_player = Physics.Raycast(shoot_ray, out hit, attackRange);
        Debug.DrawLine(transform.position, hit.point);

        if (facing_player && !isAttacking)
        {

            isAttacking = true;

            Invoke("TryDamage", 1.33f);
            Invoke("melee_attack_down", 2.667f);
        }
    }
    #endregion
}
