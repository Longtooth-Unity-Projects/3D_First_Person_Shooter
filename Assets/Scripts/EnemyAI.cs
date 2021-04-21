using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 5f;

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
        //this calls the attack method from EnemyAttack
        animator.SetBool(animatorBoolAttack, true);
    }


    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRoation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRoation, Time.deltaTime * turnSpeed);
    }


    //TODO debugging and testing
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
