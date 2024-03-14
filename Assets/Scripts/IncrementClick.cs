using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class IncrementClick :NetworkBehaviour
{
    public PlayerManager PlayerManager;

    [SyncVar] public int NoClicks = 0;

    public void IncrementClicks()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        PlayerManager.CmdIncrementClick(gameObject);
    }

}
