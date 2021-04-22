using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] private float zoomedInFOV = 20f;
    [SerializeField] private float zoomedMouse_X_Sensitivity = 1f;
    [SerializeField] private float zoomedMouse_Y_Sensitivity = 1f;

    private float defaultFOV;
    private float defaultMouse_X_Sensitiviy;
    private float defaultMouse_Y_Sensitiviy;

    private bool zoomedInToggle = false;

    //chached references
    [SerializeField] private Camera fpsCamera;
    private RigidbodyFirstPersonController fpsController;

    private void Start()
    {
        if (fpsCamera == null)
            fpsCamera = GetComponentInChildren<Camera>();

        fpsController = GetComponent<RigidbodyFirstPersonController>();

        defaultFOV = fpsCamera.fieldOfView;
        defaultMouse_X_Sensitiviy = fpsController.mouseLook.XSensitivity;
        defaultMouse_Y_Sensitiviy = fpsController.mouseLook.YSensitivity;
    }


    public void ToggleZoom()
    {
        if(zoomedInToggle == false)
        {
            zoomedInToggle = true;

            fpsCamera.fieldOfView = zoomedInFOV;
            fpsController.mouseLook.XSensitivity = zoomedMouse_X_Sensitivity;
            fpsController.mouseLook.YSensitivity = zoomedMouse_Y_Sensitivity;
        }
        else
        {
            zoomedInToggle = false;

            fpsCamera.fieldOfView = defaultFOV;
            fpsController.mouseLook.XSensitivity = defaultMouse_X_Sensitiviy;
            fpsController.mouseLook.YSensitivity = defaultMouse_Y_Sensitiviy;
        }
    }

}
