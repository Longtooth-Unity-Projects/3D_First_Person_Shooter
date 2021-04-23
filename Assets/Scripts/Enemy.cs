using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int maxHealthPoints = 100;
    [SerializeField] private int damage = 10;

    [SerializeField] private float chaseRange = 25f;
    [SerializeField] private float turnSpeed = 5f;

    private int currentHealthPoints;
    private bool isProvoked = false;
    private float distanceToTarget = Mathf.Infinity;
    private bool isDead = true;

    //cached references
    private NavMeshAgent navMeshAgent;
    private Player target;


    private Animator animator;
    private string animatorBoolAttack = "attack";
    private string animatorTriggerMove = "move";
    private string animatorTriggerIdle = "idle";
    private string animatorTriggerDie = "die";



    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        target = FindObjectOfType<Player>();

        currentHealthPoints = maxHealthPoints;
        isDead = false;
    }

   
    void Update()
    {
        if (!isDead)
        {
            distanceToTarget = Vector3.Distance(target.transform.position, transform.position);

            if (isProvoked)
                EngageTarget();
            else if (distanceToTarget <= chaseRange)
                isProvoked = true;
        }
    }



    //***********custom methods**********
    public void TakeDamage(int amountToReduce)
    {
        isProvoked = true;
        currentHealthPoints -= amountToReduce;

        if (currentHealthPoints <= 0)
            ProcessDeath();
    }


    //TODO add physics material to stop slide when die
    private void ProcessDeath()
    {
        isDead = true;
        animator.SetTrigger(animatorTriggerDie);

        enabled = false;
        navMeshAgent.enabled = false;
    }


    //TODO called from animation, decouple this
    private void EnemyAttackHitEvent()
    {
        if (target == null) return;

        target.ReduceHealth(damage);
    }








//**************************Enemy AI**********************************
    //called every update if provoked and alive
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
        navMeshAgent.SetDestination(target.transform.position);
    }


    private void AttackTarget()
    {
        //TODO this calls the EnemyAttackHitEvent() method from Enemy, rework and maybe place in attack script/enemy script
        animator.SetBool(animatorBoolAttack, true);
    }


    private void FaceTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRoation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRoation, Time.deltaTime * turnSpeed);
    }













 //*********************************************
        //TODO debugging and testing
        private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
