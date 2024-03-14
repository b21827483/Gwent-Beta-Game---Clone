using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckPanelOpener : MonoBehaviour
{
    public GameObject Panel;
    public GameObject ScrollPanel;
    public GameObject PlayerManager;

    public void OpenPanel()
    {
        if (Panel != null)
        {
            bool isActive = Panel.activeSelf;
            
            Panel.SetActive(isActive);
        }
    }

    public void PlaceCards()
    {
        if (Panel != null)
        {
            
        }
    }
}
