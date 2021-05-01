using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class Enemy : MonoBehaviour
{
    //TODO figure out how to have attack and run play at same time
    //TODO figure out how to link animation updates to a listener so they only get called when changed


    public enum MissionType
    {
        stationary = 0,
        patrollerRandom = 1,
        patrollerDirected = 2,
        patrollerRoute = 3
    }


    public enum EnemyState
    {
        dead = 0,
        idle = 1,
        shambling = 2,
        walkingTowards = 3,
        runningTowards = 4,
        attacking = 5
    }
    private string enemyStateName = "EnemyState";
    private EnemyState currentState = EnemyState.idle;




    [Header("Basic Attributes")]
    [SerializeField] private MissionType enemyType = MissionType.stationary;
    [SerializeField] private int maxHealthPoints = 100;
    [SerializeField] private int damage = 10;
    [SerializeField] private float chaseRange = 8f;


    [Header("Movement Fields")]
    [SerializeField] private float speedWalking = 0.4f;
    [SerializeField] private float speedRunning = 3.5f;
    [SerializeField] private float speedTurning = 5f;
    [SerializeField] private Transform directedDestination;

    [Header("Cached References")]
    [SerializeField] private AudioClip soundStandingIdle;
    [SerializeField] private AudioClip soundHitting;




    private int currentHealthPoints;
    private bool isProvoked = false;
    private float distanceToTargetPlayer = Mathf.Infinity;
    private Transform destination;


    //cached references
    private NavMeshAgent navMeshAgent;
    private Player targetPlayer;
    private AudioSource audioSource;
    private Animator enemyAnimator;
    private Waypoints waypoints;




    void Start()
    {
        GetCachedReferences();
        StartNewSudioLoop(audioSource, soundStandingIdle);
        currentHealthPoints = maxHealthPoints;

        switch (enemyType)
        {
            case MissionType.stationary:
                //do nothing
                break;
            case MissionType.patrollerRandom:
                destination = waypoints.GetRandomDestination();
                break;
            case MissionType.patrollerDirected:
                destination = directedDestination;
                break;
            case MissionType.patrollerRoute:
                break;
            default:
                break;
        }
    }

   
    void Update()
    {
        //all types need to do this
        CheckPlayerDistance();

        if(!isProvoked)
        {
            switch (enemyType)
            {
                case MissionType.stationary:
                    //stand there, drool, and look cool
                    break;
                case MissionType.patrollerRandom:
                    PatrolRandomly();
                    break;
                case MissionType.patrollerDirected:
                    PatrolDirected();
                    break;
                case MissionType.patrollerRoute:
                    //TODO add this in later
                    break;
                default:
                    break;
            }
        }
    }





    //***********custom methods**********
    private void CheckPlayerDistance()
    {
        if (currentState != EnemyState.dead)
        {
            distanceToTargetPlayer = Vector3.Distance(targetPlayer.transform.position, transform.position);

            if (isProvoked)
                EngageTarget();
            else if (distanceToTargetPlayer <= chaseRange)
                isProvoked = true;
        }
    }


    private void PatrolRandomly()
    {
        ShambleTowards(destination);

        if (Vector3.Distance(transform.position, destination.position) <= chaseRange)
            destination = waypoints.GetRandomDestination();
    }


    private void PatrolDirected()
    {
        bool destinationReached = Vector3.Distance(transform.position, destination.position) <= chaseRange;

        if (destination == null || destinationReached)
        {
            destination = waypoints.GetRandomDestination();
            enemyType = MissionType.patrollerRandom;
        }
        else
            ShambleTowards(destination);
    }

    public void TakeDamage(int amountToReduce)
    {
        isProvoked = true;
        currentHealthPoints -= amountToReduce;

        if (currentHealthPoints <= 0)
            ProcessDeath();
    }


    private void ProcessDeath()
    {
        SetEnemyAnimationState(EnemyState.dead);
        StartNewSudioLoop(audioSource, null);

        enabled = false;
        navMeshAgent.enabled = false;
    }


    private void StartNewSudioLoop(AudioSource audioSource, AudioClip clipToStart)
    {
        //dont need to reinitiate since it is a loop
        if (audioSource.isPlaying.Equals(clipToStart))
            return;

        if (clipToStart == null)
        {
            audioSource.Stop();
        }
        else
        {
            audioSource.Stop();
            audioSource.loop = true;
            audioSource.clip = clipToStart;
            audioSource.PlayDelayed(UnityEngine.Random.Range(0f, 2f));
        }
    }


    private void SetEnemyAnimationState(EnemyState stateToSet)
    {
        if (currentState == stateToSet)
            return;
        else
        {
            currentState = stateToSet;
            enemyAnimator.SetInteger(enemyStateName, (int)currentState);
        }
            
    }



    //TODO called from animation, decouple this and attack from enemy class
    private void EnemyAttackHitEvent()
    {
        if (targetPlayer == null) return;

        targetPlayer.ReduceHealth(damage);
        targetPlayer.GetComponent<DisplayDamage>().ActivateBloodSplatter();
        audioSource.PlayOneShot(soundHitting);
    }








//**************************Enemy AI**********************************
    //called every update if provoked and alive
    private void EngageTarget()
    {
        bool inAttackRange = distanceToTargetPlayer <= navMeshAgent.stoppingDistance;

        FaceTarget();

        if (!inAttackRange)
            RunTowardsTarget();
        else if (inAttackRange)
            AttackTarget();
    }


    //shuffles toward destination
    private void ShambleTowards(Transform destination)
    {
        SetEnemyAnimationState(EnemyState.shambling);
        navMeshAgent.speed = speedWalking;
        navMeshAgent.SetDestination(destination.position);
    }


    //walks with arms out
    private void WalkTowardsTarget()
    {
        SetEnemyAnimationState(EnemyState.walkingTowards);
        navMeshAgent.speed = speedWalking;
        navMeshAgent.SetDestination(targetPlayer.transform.position);
    }


    //runs with arms out
    private void RunTowardsTarget()
    {
        SetEnemyAnimationState(EnemyState.runningTowards);
        navMeshAgent.speed = speedRunning;
        navMeshAgent.SetDestination(targetPlayer.transform.position);
    }


    private void AttackTarget()
    {
        SetEnemyAnimationState(EnemyState.attacking);
    }


    private void FaceTarget()
    {
        Vector3 direction = (targetPlayer.transform.position - transform.position).normalized;
        Quaternion lookRoation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRoation, Time.deltaTime * speedTurning);
    }





 //***************utility methods***********************************
    private void GetCachedReferences()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        targetPlayer = FindObjectOfType<Player>();
        audioSource = GetComponent<AudioSource>();
        waypoints = FindObjectOfType<Waypoints>();
    }






 //***************Debugging and testing******************************
    //TODO debugging and testing
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
