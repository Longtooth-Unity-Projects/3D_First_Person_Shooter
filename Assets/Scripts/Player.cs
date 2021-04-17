using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, Input_Actions_3D_First_Person_Shooter.IPlayerActions
{

    Input_Actions_3D_First_Person_Shooter controls;


    private void OnEnable()
    {
        if(controls == null)
        {
            controls = new Input_Actions_3D_First_Person_Shooter();
            controls.Player.SetCallbacks(this);
        }

        controls.Player.Enable();
    }


    private void Update()
    {
        //Vector2 test = controls.Player.Move.ReadValue<Vector2>();
    }








    private void OnDisable()
    {
        controls.Player.Disable();
    }





    //input functions
    public void OnFire(InputAction.CallbackContext context)
    {
        Debug.Log("OnFire context:  " + context.phase.ToString());
    }


    public void OnLook(InputAction.CallbackContext context)
    {
        Debug.Log("OnLook context:  " + context.phase.ToString());
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("OnMove context:  " + context.phase.ToString());

        switch (context.phase)
        {
            case InputActionPhase.Disabled:
                break;
            case InputActionPhase.Waiting:
                break;
            case InputActionPhase.Started:
                break;
            case InputActionPhase.Performed:
                break;
            case InputActionPhase.Canceled:
                break;
            default:
                break;
        }
    }


}
