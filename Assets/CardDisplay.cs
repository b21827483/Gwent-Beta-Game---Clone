using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public Image artwork;
    public TextMeshProUGUI HealthText;
    private string description;
    
    
    // Start is called before the first frame update
    void Start()
    {
        artwork.sprite = card.artwork;
        HealthText.text = card.health.ToString();
        description = card.description;
    }

    
}
