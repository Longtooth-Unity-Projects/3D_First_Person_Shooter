using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO move this possibly to player and generate a cached list of weapons
public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] int currentWeaponIndex = 0;
    private int previousWeaponIndex = 0;
    private int numOfWeapons = 0;


    private void Start()
    {
        numOfWeapons = transform.childCount;
        SetWeaponActive();
    }

    private void SetWeaponActive()
    {
        int weaponIndex = 0;

        foreach(Transform weapon in transform)
        {
            if (weaponIndex == currentWeaponIndex)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);

            ++weaponIndex;
        }
    }


    public void WeaponSelect(int requestedWeaponIndex)
    {
        //TODO figurea out a way to have a cached array so we can access directly and combine with setweaponactive
        if (requestedWeaponIndex < 0 ||
            requestedWeaponIndex > numOfWeapons - 1 ||
            requestedWeaponIndex == currentWeaponIndex)
        { return; }

        currentWeaponIndex = requestedWeaponIndex;
        SetWeaponActive();
    }

    public void WeaponScroll(float yAxisValue)
    {
        previousWeaponIndex = currentWeaponIndex;

        if (yAxisValue > 0)
            if (currentWeaponIndex < numOfWeapons - 1) //guarding against out of index errors
                ++currentWeaponIndex;
            else return;

        if (yAxisValue < 0)
            if (currentWeaponIndex > 0)                //guarding against out of index errors
                --currentWeaponIndex;
            else return;
        
        SetWeaponActive();
    }
}
