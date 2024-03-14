using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FlipCard : MonoBehaviour
{
    public Sprite Cardback;
    public Sprite Cardfront;

    public void Flip()
    {
        Sprite currentSprite = gameObject.GetComponent<Image>().sprite;

        if (currentSprite == Cardfront)
        {
            gameObject.GetComponent<Image>().sprite = Cardback;
        }

        else
        {
            gameObject.GetComponent<Image>().sprite = Cardfront;
        }
    }
}
