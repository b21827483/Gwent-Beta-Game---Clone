using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Mirror;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerManager : NetworkBehaviour
{
    public GameObject card1;
    public GameObject card2;
    public GameObject card3;
    public GameObject card4;
    public GameObject playerArea;
    public GameObject enemyArea;
    public GameObject PlayerDropZone;
    public GameObject PDZMeleeRow;
    public GameObject PDZRangedRow;
    public GameObject EnemyDropZone;
    public GameObject PlayerDeck;
    public GameObject EnemyDeck;
    public GameObject PlayerHandTextNo;
    public GameObject PlayerDeckTextNo;
   
    
    
    private List<GameObject> Cards = new List<GameObject>();
    public List<GameObject> playerDeck = new List<GameObject>();
    public List<GameObject> enemyDeck = new List<GameObject>();

    public override void OnStartClient()
    {
        base.OnStartClient();

        playerArea = GameObject.Find("PlayerArea");
        enemyArea = GameObject.Find("EnemyArea");
        PlayerDropZone = GameObject.Find("NewPlayerDropZone");
        PDZMeleeRow = PlayerDropZone.gameObject.transform.Find("MeleeRow").gameObject;
        PDZRangedRow = PlayerDropZone.gameObject.transform.Find("RangedRow").gameObject;
        EnemyDropZone = GameObject.Find("EnemyDropZone");
        PlayerDeck = GameObject.Find("PlayerDeck");
        EnemyDeck = GameObject.Find("EnemyDeck");
        PlayerHandTextNo = GameObject.Find("GameTexts").gameObject.transform.Find("HandNoText").gameObject;
        PlayerDeckTextNo = GameObject.Find("GameTexts").gameObject.transform.Find("DeckNoText").gameObject;
        
        Debug.Log("CLIENT STARTED");
    }

    [Server]
    public override void OnStartServer()
    {
        base.OnStartServer();
        
        Cards.Add(card1);
        Cards.Add(card2);
        Cards.Add(card3);
        Cards.Add(card4);
        
        for (int i = 0; i < 15; i++)
        {   
            GameObject card = Instantiate(Cards[Random.Range(0, Cards.Count)], new Vector2(0, 0), Quaternion.identity);
            EnableDisableComp(card);
            playerDeck.Add(card);
            enemyDeck.Add(card);
        }
        
        Debug.Log("SERVER STARTED");
    }

    void EnableDisableComp(GameObject card)
    {
        card.GetComponent<DragDrop>().enabled = !card.GetComponent<DragDrop>().enabled;
        card.GetComponent<FlipCard>().enabled = !card.GetComponent<FlipCard>().enabled;
        card.GetComponent<BoxCollider2D>().enabled = !card.GetComponent<BoxCollider2D>().enabled;
    }
    
    [Command]
    public void CmdDealCards()
    {   
        Debug.Log("DEALT");
        if (playerDeck.Count == 0)
        {
            return;
        }

        int playerDeckCount = playerDeck.Count;
        
        for (int i = 0; i < 10; i++)
        {
            GameObject card = playerDeck[Random.Range(0, playerDeck.Count)];
            playerDeck.Remove(card);
            EnableDisableComp(card);
            card.GetComponent<DragDrop>().isDraggable = true;
            NetworkServer.Spawn(card, connectionToClient);
            RpcShowCard(card, "Dealt");
            Debug.Log("PlayerDeck Count: "+ playerDeck.Count);
        }
        Debug.Log("cmd");
    }

    public void PlayCard(GameObject card)
    {
        CmdPlayCard(card);
    }

    [Command]
    void CmdPlayCard(GameObject card)
    {
        RpcShowCard(card, "Played");
    }

    [ClientRpc]
    public void RpcShowCard(GameObject card, string action)
    {
        if (action == "Dealt")
        {
            if (hasAuthority)
            {
                card.transform.SetParent(playerArea.transform, false);
                setNumberText(action);
            }

            else
            {
                card.transform.SetParent(enemyArea.transform, false);
                card.GetComponent<FlipCard>().Flip();
            }
        }

        else if (action == "Played")
        {
            if (hasAuthority)
            {
                if (card.GetComponent<DragDrop>().PlayerDropZone == PDZMeleeRow)
                {   
                    card.transform.SetParent(PDZMeleeRow.transform, false);
                    setNumberText(action);
                }

                if (card.GetComponent<DragDrop>().PlayerDropZone == PDZRangedRow)
                {
                    card.transform.SetParent(PDZRangedRow.transform, false);
                    setNumberText(action);
                }
                    
            }

            else
            {
                card.transform.SetParent(EnemyDropZone.transform, false);
                card.GetComponent<FlipCard>().Flip();
            }
        }
    }

    public void setNumberText(string action)
    {
        if (action == "Played" )
        {
            if (hasAuthority)
            {
                PlayerHandTextNo.GetComponent<TextMeshProUGUI>().text = (playerArea.transform.childCount).ToString();
            }
            else
            {
                
            }
        }

        else if (action == "Dealt")
        {
            if (hasAuthority)
            {
                PlayerHandTextNo.GetComponent<TextMeshProUGUI>().text = playerArea.transform.childCount.ToString();
            }
            else
            {
                
            }
        }
    }

    [Command]
    public void CmdTargetItself()
    {
        TargetItself();
    }

    [Command]
    public void CmdTargetOther(GameObject target)
    {
        NetworkIdentity opponentIdentity = target.GetComponent<NetworkIdentity>();
        TargetOther(opponentIdentity.connectionToClient);
    }

    [TargetRpc]
    void TargetItself()
    {
        
    }

    [TargetRpc]
    void TargetOther(NetworkConnection target)
    {
        
    }

    [Command]
    public void CmdIncrementClick(GameObject gameObject)
    {
        RpcIncrementClick(gameObject);
    }

    [ClientRpc]
    void RpcIncrementClick(GameObject gameObject)
    {
        gameObject.GetComponent<IncrementClick>().NoClicks++;
    }
}
