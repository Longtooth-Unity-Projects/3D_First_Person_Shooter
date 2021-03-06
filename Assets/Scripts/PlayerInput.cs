using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, Input_Actions_3D_First_Person_Shooter.IPlayerActions
{
    //cached references
    Input_Actions_3D_First_Person_Shooter controls;

    Weapon weapon;
    WeaponSwitcher weaponSwitcher;
    Flashlight flashlight;


    private void Awake()
    {
        if (controls == null)
            controls = new Input_Actions_3D_First_Person_Shooter();
        controls.Player.SetCallbacks(this);
    }


    private void OnEnable()
    {
        controls.Enable();
        //ControlsEnable();
    }


    private void Start()
    {
        GetActivePlayerInfo();
    }


    private void Update()
    {
        //Vector2 test = controls.Player.Move.ReadValue<Vector2>();
        //var test = controls.Player.WeaponSwitch.ReadValue<float>();
        //Debug.Log($"scroll test: {test}");
    }


    private void OnDisable()
    {
        controls.Disable();
        //ControlsDisable();
    }





    //********************input methods**************************
    public void OnFire(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Disabled:
                break;
            case InputActionPhase.Waiting:
                break;
            case InputActionPhase.Started:
                break;
            case InputActionPhase.Performed:
                StartCoroutine(weapon.WeaponAttack());
                break;
            case InputActionPhase.Canceled:
                break;
            default:
                break;
        }
    }


    public void OnLook(InputAction.CallbackContext context)
    {
        //Debug.Log("OnLook context:  " + context.phase.ToString());
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        //Debug.Log("OnMove context:  " + context.phase.ToString());
    }


    public void OnZoom(InputAction.CallbackContext context)
    {
        if(context.performed)
            weapon.ToggleZoom();
    }


    public void OnWeaponSelect(InputAction.CallbackContext context)
    {
        //TODO learn how to do this by comparing the actual InputControl value https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/api/UnityEngine.InputSystem.InputAction.CallbackContext.html

        if (context.performed)
        {
            //use the name of the control passed in instead of checking for each keystroke
            weaponSwitcher.WeaponSelect(int.Parse(context.control.name) - 1);
            GetActivePlayerInfo();
        }
    }


    public void OnWeaponScroll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            weaponSwitcher.WeaponScroll(context.ReadValue<Vector2>().y);
            GetActivePlayerInfo();
        }
    }


    public void OnTestAction(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Disabled:
                break;
            case InputActionPhase.Waiting:
                break;
            case InputActionPhase.Started:
                break;
            case InputActionPhase.Performed:
                //var testFlt = context.ReadValue<float>();
                //Debug.Log($"OnTestAction {testFlt}");
                break;
            case InputActionPhase.Canceled:
                break;
            default:
                break;
        }


    }



    public void OnFlashlight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
            flashlight.ToggleLight();
    }




    //****************custom methods***************
    public void AdjustMouseSensitivity(float newSensitivity)
    {
        //do something
    }





    //***************utility methods*******************
    private void GetActivePlayerInfo()
    {
        weapon = GetComponentInChildren<Weapon>();
        weaponSwitcher = GetComponentInChildren<WeaponSwitcher>();
        flashlight = GetComponentInChildren<Flashlight>();
    }

}//end of class
