using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.Examples.CCU;

public class TargetClick : NetworkBehaviour
{
    public PlayerManager PlayerManager;

    public void OnTargetClick()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();

        if (hasAuthority)
        {
            PlayerManager.CmdTargetItself();
        }
        else
        {
            PlayerManager.CmdTargetOther(gameObject);
        }
    }

}
