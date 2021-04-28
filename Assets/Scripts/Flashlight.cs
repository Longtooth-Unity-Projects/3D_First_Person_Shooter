using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private float angleDecay = 1f;
    [SerializeField] private float minAngle = 40f;
    [SerializeField] private float lightDecay = 0.1f;


    private float maxAngle = 60f;
    private float maxIntensity = 10f;
    private bool isActivated = true;

    //cached references
    Light lightSource;

    private void Start()
    {
        lightSource = GetComponent<Light>();
        maxAngle = lightSource.spotAngle;
        maxIntensity = lightSource.intensity;
    }

    private void Update()
    {
        if (isActivated)
        {
            if(lightSource.spotAngle > minAngle)
                lightSource.spotAngle -= angleDecay * Time.deltaTime;

            lightSource.intensity -= lightDecay * Time.deltaTime;
        }


    }


    public void RestoreLightAngle(float angleToAdd)
    {
        float newAngle = lightSource.spotAngle + angleToAdd;

        if (newAngle > maxAngle)
            lightSource.spotAngle = maxAngle;
        else
            lightSource.spotAngle = newAngle;
    }

    public void RestoreLightIntensity(float intensityAmountToAdd)
    {
        float newIntensity = lightSource.intensity + intensityAmountToAdd;

        if (newIntensity > maxIntensity)
            lightSource.intensity = maxIntensity;
        else
            lightSource.intensity = newIntensity;
    }
}
