using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class OwnerSpawnerAnnouncer : NetworkBehaviour
{
    public override void OnStartAuthority()
    {
        base.OnStartAuthority();
        AnnounceSpawned();
    }

    private void AnnounceSpawned()
    {
        ClientInstance ci = ClientInstance.ReturnClientInstance();
    }
}
