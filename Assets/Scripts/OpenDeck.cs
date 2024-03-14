using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UIElements;

public class OpenDeck : MonoBehaviour
{
    public GameObject Panel;
    public GameObject ScrollPanel;
    public PlayerManager PlayerManager;

    private void Start()
    {
        ScrollPanel = Panel.transform.Find("Scroll").gameObject.transform.Find("ScrollPanel").gameObject;
    }

    public void OpenPanel()
    {
        if (Panel != null)
        {
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive);
        }
    }
    
    public void PlaceCards()
    {
        if (Panel != null)
        {
            NetworkIdentity networkIdentity = NetworkClient.connection.identity;
            PlayerManager = networkIdentity.GetComponent<PlayerManager>();
            
            foreach (GameObject card in PlayerManager.playerDeck)
            {
                card.transform.parent = ScrollPanel.transform;
            }
        }
    }

    
}
