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
    NavMeshAgent navMeshAgent;


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (isProvoked)
            EngageTarget();
        else if (distanceToTarget <= chaseRange)
            isProvoked = true;
    }

    private void EngageTarget()
    {
        bool inAttackRange = distanceToTarget <= navMeshAgent.stoppingDistance;

        if (!inAttackRange)
            ChaseTarget();  //TODO remove this method if no more functionality is added to it
        else if (inAttackRange)
            AttackTarget();
    }


    private void ChaseTarget()
    {
        navMeshAgent.SetDestination(target.position);
    }


    private void AttackTarget()
    {
        Debug.Log($"{name} has seeked and is attacking {target.name}");
    }


    //TODO debugging
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
