using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{

    [SerializeField] float weaponRange = 100f;
    [SerializeField] int weaponDamage = 1;

    private float temporaryObjectDestroyDelay = .1f;


    //cached references
    //TODO rework this input so it is called from a centeral input script using new input system
    //have a list of weapons and update active weapon
    Mouse mouse;
    [SerializeField] Camera FPCamera;   //TODO serialzized for debugging purposes
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;

    [SerializeField] string containerName = "ContainerForRuntime";
    GameObject container;

    private void Start()
    {
        mouse = Mouse.current;
        FPCamera = GetComponentInParent<Camera>();

        FindRuntimeContainer();
    }


    void Update()
    {
        //if(mouse.leftButton.isPressed)
        if(mouse.leftButton.wasPressedThisFrame)
        {
            WeaponAttack();
        }
    }



    //custom methods
    //TODO move this functionality to the game manager
    private void FindRuntimeContainer()
    {
        container = GameObject.Find(containerName);
        if (container == null)
        {
            container = new GameObject(containerName);
            container.transform.position = new Vector3(0, 0, 0);
        }
    }


    private void WeaponAttack()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if(Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, weaponRange))
        {
            Debug.Log($"{name} shooting... hit {hit.transform.name}");

            CreateHitEffect(hit);

            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target != null)
                target.ReduceHealth(weaponDamage);
        }
    }//end of WeaponAttack()


    private void CreateHitEffect(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal), container.transform);
        Destroy(impact, temporaryObjectDestroyDelay);
    }

}