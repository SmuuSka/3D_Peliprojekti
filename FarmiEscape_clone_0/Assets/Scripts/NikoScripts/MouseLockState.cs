using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MouseLockState : MonoBehaviour
{


    private void Update()
    {

        if (NetworkManagerHUD.setMouseLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
