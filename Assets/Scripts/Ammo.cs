using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO add this functionality to the weapon class
public class Ammo : MonoBehaviour
{
    [SerializeField] private int ammoAmount = 10;
    public int AmmoAmount { get { return ammoAmount; } }

    public void ReduceAmmoAmount()
    {
        ammoAmount--;
    }
}
