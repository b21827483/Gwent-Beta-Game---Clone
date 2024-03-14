using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public int turn = 0;

    public void updateTurnsPlayed()
    {
        turn++;
    }
    
    
}
