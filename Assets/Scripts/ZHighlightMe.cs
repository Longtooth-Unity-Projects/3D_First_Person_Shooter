using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZHighlightMe : MonoBehaviour
{
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 3f);
    }
}
