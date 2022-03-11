using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class SetCrosshair : MonoBehaviour
{
    [SerializeField] private Image crossHair;

    private void Update()
    {
        if (NetworkManagerHUD.setMouseLock)
        {
            crossHair.gameObject.SetActive(true);
        }

        crossHair.rectTransform.localScale = new Vector3(0.15f, 0.15f);
    }

}
