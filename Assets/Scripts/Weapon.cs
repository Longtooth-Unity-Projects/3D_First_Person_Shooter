using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityStandardAssets.Characters.FirstPerson;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour
{
    [Header("Basic Weapon Stats")]
    [SerializeField] float weaponRange = 100f;
    [SerializeField] int weaponDamage = 1;
    [SerializeField] float shotDelay = 0.1f;

    [Header("Weapon Zoom Fields")]
    [SerializeField] private float zoomedInFOV = 20f;
    [SerializeField] private float zoomedMouse_X_Sensitivity = 1f;
    [SerializeField] private float zoomedMouse_Y_Sensitivity = 1f;
    private float defaultFOV;
    private float defaultMouse_X_Sensitiviy;
    private float defaultMouse_Y_Sensitiviy;
    private bool zoomedInToggle = false;

    private float temporaryObjectDestroyDelay = .1f;
    private bool canShoot = true;


    //cached references
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private AudioClip soundGunshot;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private Ammo ammoSlot;
    [SerializeField] private AmmoType ammoType;


    private Camera fpsCamera;
    private RigidbodyFirstPersonController fpsController;
    private AudioSource audioSource;
    private Animation anim;

    //TODO move ammocount text to game manager
    [SerializeField] TextMeshProUGUI ammoCountText;
    [SerializeField] string containerName = "ContainerForRuntime";
    GameObject container;


    private void OnEnable()
    {
        UpdateAmmoDisplay();
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        fpsCamera = GetComponentInParent<Camera>();
        fpsController = GetComponentInParent<RigidbodyFirstPersonController>();
        anim = GetComponent<Animation>();

        defaultFOV = fpsCamera.fieldOfView;
        defaultMouse_X_Sensitiviy = fpsController.mouseLook.XSensitivity;
        defaultMouse_Y_Sensitiviy = fpsController.mouseLook.YSensitivity;

        FindRuntimeContainer();
    }

    private void OnDisable()
    {
        if (zoomedInToggle)
            ToggleZoom();
    }

    //custom methods
    public IEnumerator WeaponAttack()
    {
        if (ammoSlot.GetCurrentAmmoAmount(ammoType) > 0 && canShoot)
        {
            ammoSlot.ReduceAmmoAmount(ammoType);
            UpdateAmmoDisplay();
            muzzleFlash.Play();
            audioSource.PlayOneShot(soundGunshot);
            if(anim != null)
                anim.Play();


            RaycastHit hit;
            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, weaponRange))
            {
                CreateHitEffect(hit);

                Enemy target = hit.transform.GetComponent<Enemy>();
                if (target != null)
                    target.TakeHit(hit, weaponDamage);
            }

            canShoot = false;
            yield return new WaitForSeconds(shotDelay);
            canShoot = true;
        }

    }//end of WeaponAttack()


    private void CreateHitEffect(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal), container.transform);
        Destroy(impact, temporaryObjectDestroyDelay);
    }


    public void ToggleZoom()
    {
        if (zoomedInToggle == false)
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


    //***********utility methods*****************
    public void UpdateAmmoDisplay()
    {
        ammoCountText.text = $"Ammo: {ammoSlot.GetCurrentAmmoAmount(ammoType)}";
    }




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

}
