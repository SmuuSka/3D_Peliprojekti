using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class ClientInstance : NetworkBehaviour
{
    public static ClientInstance Instance;

    public static Action<GameObject> OnOwnerCharacterSpawned;

    public void InvokeCharacterSpawned(GameObject gameObject)
    {
        OnOwnerCharacterSpawned?.Invoke(gameObject);
    }

    [Tooltip("Prefab for the player")]
    [SerializeField] private NetworkIdentity _playerPrefab = null;


    [Command]
    private void CmdRequestSpawn()
    {
        NetworkSpawnPlayer();
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        Instance = this;
        CmdRequestSpawn();
    }

    [Server]
    private void NetworkSpawnPlayer()
    {
        GameObject gameObject = Instantiate(_playerPrefab.gameObject, transform.position, Quaternion.identity);
        NetworkServer.Spawn(gameObject, base.connectionToClient);
    }

    public static ClientInstance ReturnClientInstance(NetworkConnection connection = null)
    {
        if (NetworkServer.active && connection != null)
        {
            NetworkIdentity localPlayer;
            if (GettingStartedNetworkManager.LocalPlayers.TryGetValue(connection, out localPlayer))
                return localPlayer.GetComponent<ClientInstance>();
            else
            {
                return null;
            }

        }
        else
        {
            return Instance;
        }
    }
}
