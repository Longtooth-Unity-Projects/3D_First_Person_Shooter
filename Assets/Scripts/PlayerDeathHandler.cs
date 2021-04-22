using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO move all of this functionality to other scripts, possibly game manager
public class PlayerDeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;

    private void Start()
    {
        gameOverCanvas.enabled = false;
    }

    public void HandlePlayerDeath()
    {
        gameOverCanvas.enabled = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }



}
