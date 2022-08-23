using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    void Start()
    {
        //Updating Card Resources
        this.gameObject.name = "Card[" + card.name + "]";
        gameObject.GetComponent<Image>().sprite = card.sprite;

        if(card.startingFunction != string.Empty)
        {
            Invoke(card.startingFunction, 0);
        }
    }
    
    void ActivateCard()
    {
        //Card Starting Effects
        if (card.resourceType != ResourceType.NULL || card.deltaResource != 0)
        {
            SetResource(card.resourceType, card.deltaResource);
        }


        if (card.ExternalFunction != string.Empty)
        {
            Invoke(card.ExternalFunction, 0);
        }
    }

    void SetResource(ResourceType type, int amount)
    {
        switch(type)
        {
            case ResourceType.Food:
                break;
        }
    }
}
