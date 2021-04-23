using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 25f;
    [SerializeField] float turnSpeed = 5f;

    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    //cached references
    private NavMeshAgent navMeshAgent;
    private Enemy me;

    private Animator animator;
    private string animatorBoolAttack = "attack";
    private string animatorTriggerMove = "move";
    private string animatorTriggerIdle = "idle";
    private string animatorTriggerDie = "die";








    private void OnEnable()
    {
        Enemy.DamageTaken += OnDamageTaken;
        Enemy.Died += OnDeath;
    }


    void Start()
    {
        me = GetComponent<Enemy>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        target = FindObjectOfType<Player>().transform;
    }

   
    void Update()
    {
        if(me.isAlive)
        {
            distanceToTarget = Vector3.Distance(target.position, transform.position);

            if (isProvoked)
                EngageTarget();
            else if (distanceToTarget <= chaseRange)
                isProvoked = true;
        }
    }


    private void OnDisable()
    {
        Enemy.DamageTaken -= OnDamageTaken;
        Enemy.Died -= OnDeath;
    }









    //***********custom methods**********
    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    //called every update if provoked
    private void EngageTarget()
    {
        bool inAttackRange = distanceToTarget <= navMeshAgent.stoppingDistance;

        FaceTarget();

        if (!inAttackRange)
            ChaseTarget();
        else if (inAttackRange)
            AttackTarget();
    }


    private void ChaseTarget()
    {
        animator.SetBool(animatorBoolAttack, false);
        animator.SetTrigger(animatorTriggerMove);
        navMeshAgent.SetDestination(target.position);
    }


    private void AttackTarget()
    {
        //TODO this calls the attack method from Enemy, rework and maybe place in attack script/enemy script
        animator.SetBool(animatorBoolAttack, true);
    }


    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRoation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRoation, Time.deltaTime * turnSpeed);
    }


    private void OnDeath()
    {
        animator.SetTrigger(animatorTriggerDie);
    }

    //TODO debugging and testing
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
