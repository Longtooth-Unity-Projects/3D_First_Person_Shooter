using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    [SerializeField] private Canvas loadingCanvas;


    // Start is called before the first frame update
    void Start()
    {
        loadingCanvas.enabled = false;
    }

    public void ActivateCanvas()
    {
        loadingCanvas.enabled = true;
    }
}
