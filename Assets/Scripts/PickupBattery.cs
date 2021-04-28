using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBattery : MonoBehaviour
{
    [SerializeField] float intensityAmount = 10f;
    [SerializeField] float angleAmount = 20f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponentInChildren<Flashlight>().RestoreLightIntensity(intensityAmount);
            other.GetComponentInChildren<Flashlight>().RestoreLightAngle(angleAmount);
            Destroy(gameObject);
        }
    }
}
