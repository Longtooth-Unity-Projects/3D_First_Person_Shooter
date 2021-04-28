using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDamage : MonoBehaviour
{
    [SerializeField] Canvas bloodSplatter;
    [SerializeField] float impactime = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        bloodSplatter.enabled = false;
    }



    public void ActivateBloodSplatter()
    {
        StartCoroutine(ShowBloodSplatter());
    }


    private IEnumerator ShowBloodSplatter()
    {
        bloodSplatter.enabled = true;
        yield return new WaitForSeconds(impactime);
        bloodSplatter.enabled = false;
    }

}
