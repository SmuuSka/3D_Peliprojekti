using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class PlayerName : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    [SyncVar(hook = nameof(OnNameUpdated))]
    private string _synchronziedName = string.Empty;


    private void Update()
    {
        if (base.isClient)
        {
            Debug.Log(_synchronziedName);
        }
    }

    private void OnNameUpdated(string prev, string next)
    {
        _text.text = next;
    }

    [Client]
    public void SetName(string name)
    {
        CmdSetName(name);
    }

    [Command]
    private void CmdSetName(string name)
    {
        _synchronziedName = name;
    }

}
