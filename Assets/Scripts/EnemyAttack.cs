using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] int damage = 10;

    //cached references
    private PlayerHealth target;

    private void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
    }


    //TODO called from animation, decouple this
    private void EnemyAttackHitEvent()
    {
        if (target == null) return;

        target.ReduceHealth(damage);
    }
}
