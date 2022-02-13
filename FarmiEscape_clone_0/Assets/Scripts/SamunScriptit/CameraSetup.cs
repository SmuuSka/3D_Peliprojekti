using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CameraSetup : NetworkBehaviour
{
    [SerializeField] private Transform _playerCamera;

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        _playerCamera.gameObject.SetActive(true);
    }
}
