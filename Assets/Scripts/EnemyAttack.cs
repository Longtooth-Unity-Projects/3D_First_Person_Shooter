using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] Transform target; //TODO debugging and testing
    [SerializeField] int damage = 10;


    void Start()
    {
        
    }

    //TODO called from animation, decouple this
    private void EnemyAttackHitEvent()
    {
        if (target == null) return;

        Debug.Log($"Hitting {target.name}");
    }
}
