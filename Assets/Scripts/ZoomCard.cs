using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class ZoomCard : NetworkBehaviour
{
    private GameObject canvas;
    private GameObject panel;
    public GameObject ZoomedCard; // ZoomCard Prefab
    private GameObject Panel;
    private GameObject zoomCard;
    private Sprite zoomSprite;
    private bool isDragging;
    private GameObject description;
    
    public void Awake()
    {
        canvas = GameObject.Find("Main Canvas");
        isDragging = gameObject.GetComponent<DragDrop>().isDragging;
        zoomSprite = gameObject.GetComponent<CardDisplay>().card.artwork;
        panel = GameObject.Find("Panel");
    }

    private void Update()
    {
        isDragging = gameObject.GetComponent<DragDrop>().isDragging;
    }

    public void HoverOver()
    {

        
        if (!isDragging)
        {
            zoomCard = Instantiate(ZoomedCard, new Vector2(1680, 680), Quaternion.identity);
            zoomCard.transform.Find("ImageMask").gameObject.transform.Find("Image").gameObject.GetComponent<Image>().sprite = zoomSprite;
            zoomCard.transform.Find("HealthText").gameObject.GetComponent<TextMeshProUGUI>().text = gameObject.GetComponent<CardDisplay>().card.health.ToString();
            
            zoomCard.transform.Find("CardName").gameObject.GetComponent<Text>().text = gameObject.GetComponent<CardDisplay>().card.name; 
            description = zoomCard.transform.Find("Description").gameObject;
            description.GetComponent<Text>().text = gameObject.GetComponent<CardDisplay>().card.description;

            if (panel != null)
            {
                zoomCard.transform.SetParent(panel.transform, true);
            }
            
            zoomCard.transform.SetParent(canvas.transform, true);
        }
    }

    public void ExitHover()
    {
        Destroy(zoomCard);
        Debug.Log("zoomed card destroyed");
    }
}
