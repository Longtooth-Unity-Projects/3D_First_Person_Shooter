using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;


    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    //cached references
    private NavMeshAgent navMeshAgent;

    private Animator animator;
    private string animatorTriggerMove = "move";
    private string animatorTriggerIdle = "idle";
    private string animatorBoolAttack = "attack";


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

   
    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (isProvoked)
            EngageTarget();
        else if (distanceToTarget <= chaseRange)
            isProvoked = true;
    }


    //called every update if provoked
    private void EngageTarget()
    {
        bool inAttackRange = distanceToTarget <= navMeshAgent.stoppingDistance;

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
        animator.SetBool(animatorBoolAttack, true);
    }


    //TODO debugging and testing
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
