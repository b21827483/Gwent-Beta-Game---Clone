using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DragDrop : NetworkBehaviour
{
    private GameObject Canvas;
    public PlayerManager PlayerManager;
    private GameObject startParent;
    public Vector2 startPosition;
    public bool isDragging = false;
    public bool isDraggable = true;
    public bool isOverPlayerDropZone = false;
    public GameObject PlayerDropZone;
    

    private void Start()
    {
        Canvas = GameObject.Find("Main Canvas");
        
        if (!hasAuthority)
        {
            isDraggable = false;
        }
        startPosition = transform.position;
        
    }

    void Update()
    {
        if (isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.SetParent(Canvas.transform, true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == GameObject.Find("NewPlayerDropZone").gameObject.transform.Find("MeleeRow").gameObject || 
            collision.gameObject == GameObject.Find("NewPlayerDropZone").gameObject.transform.Find("RangedRow").gameObject)
        {
            isOverPlayerDropZone = true;
            PlayerDropZone = collision.gameObject;
            Debug.Log("Entered: " + PlayerDropZone.name);
        }

    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject == PlayerDropZone)
        {   
            Debug.Log("Exited:" + PlayerDropZone.name);
            isOverPlayerDropZone = false;
            PlayerDropZone = null;
            
        }
    }

    public void startDrag()
    {
        if (!isDraggable) return;
        startParent = transform.parent.gameObject;
        startPosition = transform.position;
        isDragging = true;
    }

    public void stopDrag()
    {
        if (!isDraggable) return;
        
        isDragging = false;
        if (isOverPlayerDropZone)
        {   
            //Card number on a row can't exceed 9 
            if (PlayerDropZone.transform.childCount >= 9)
            {
                transform.position = startPosition;
                transform.SetParent(startParent.transform, false);
                return;
            }
            
            transform.SetParent(PlayerDropZone.transform, false);
            isDraggable = false;
            NetworkIdentity networkIdentity = NetworkClient.connection.identity;
            PlayerManager = networkIdentity.GetComponent<PlayerManager>();
            PlayerManager.PlayCard(gameObject);
        }
        else
        {
            transform.position = startPosition;
            transform.SetParent(startParent.transform, false);
        }
    }
    
}
