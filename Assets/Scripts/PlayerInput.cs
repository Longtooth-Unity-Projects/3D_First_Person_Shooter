using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour//, Input_Actions_3D_First_Person_Shooter.IPlayerActions
{
    //cached references
    Input_Actions_3D_First_Person_Shooter controls;

    Weapon weapon;
    WeaponZoom weaponZoom;


    private void Awake()
    {
        if (controls == null)
            controls = new Input_Actions_3D_First_Person_Shooter();
    }


    private void OnEnable()
    {
        ControlsEnable();
    }


    private void Start()
    {
        weapon = GetComponentInChildren<Weapon>();
        weaponZoom = GetComponent<WeaponZoom>();
    }


    private void Update()
    {
        //Vector2 test = controls.Player.Move.ReadValue<Vector2>();
    }


    private void OnDisable()
    {
        ControlsDisable();
    }





    //input methods
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
                weapon.WeaponAttack();
                break;
            case InputActionPhase.Canceled:
                break;
            default:
                break;
        }
    }


    public void OnLook(InputAction.CallbackContext context)
    {
        Debug.Log("OnLook context:  " + context.phase.ToString());
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("OnMove context:  " + context.phase.ToString());
    }


    public void OnZoom(InputAction.CallbackContext context)
    {
        weaponZoom.ToggleZoom();
    }


    public void OnWeaponSwitch(InputAction.CallbackContext context)
    {
        Debug.Log("OnWeaponSwitch");
    }


    //custom methods
    public void AdjustMouseSensitivity(float newSensitivity)
    {
        //do something
    }


    //utility methods
    private void ControlsEnable()
    {
        controls.Player.Fire.performed += OnFire;
        controls.Player.Look.performed += OnLook;
        controls.Player.Move.performed += OnMove;
        controls.Player.Zoom.performed += OnZoom;
        controls.Player.WeaponSwitch.performed += OnWeaponSwitch;
        controls.Player.Enable();
    }

    private void ControlsDisable()
    {
        controls.Player.Fire.performed -= OnFire;
        controls.Player.Look.performed -= OnLook;
        controls.Player.Move.performed -= OnMove;
        controls.Player.Zoom.performed -= OnZoom;
        controls.Player.WeaponSwitch.performed -= OnWeaponSwitch;
        controls.Player.Disable();
    }
}
