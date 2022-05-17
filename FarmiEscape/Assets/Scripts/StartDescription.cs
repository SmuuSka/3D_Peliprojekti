using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDescription : MonoBehaviour
{
    private void Awake()
    {
        ActivatePanel();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GoPlay();
        }

    }
    private void ActivatePanel ()
    {
        Time.timeScale = 0;
        //Cursor.lockState = CursorLockMode.Locked;
    }
    private void GoPlay()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false); 
        
    }
}
