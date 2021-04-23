using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO add this functionality to the weapon class
public class Ammo : MonoBehaviour
{
    [SerializeField] AmmoSlot[] ammoSlots;


    [System.Serializable]
    private class AmmoSlot
    {
        public AmmoType ammoType;
        public int ammoAmount;
    }


    public int GetCurrentAmmoAmount(AmmoType ammoType) 
    {
        return GetAmmoSlot(ammoType).ammoAmount;
    }


    public void ReduceAmmoAmount(AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).ammoAmount--;
    }

    public void IncreaseAmmoAmount(AmmoType ammoType, int amountToIncrease)
    {
        GetAmmoSlot(ammoType).ammoAmount += amountToIncrease;
    }

    private AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        //TODO see if we can do this with a cached array insstead of running a loop each time
        //assumes ammotypes are unique, i.e. there is only one of each type
        foreach (AmmoSlot ammoSlot in ammoSlots)
        {
            if (ammoSlot.ammoType == ammoType)
                return ammoSlot;
        }

        return null;
    }
}
