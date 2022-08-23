using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;
    private CardManager CM;

    void Start()
    {
        //Updating Card Resources
        this.gameObject.name = "Card[" + card.name + "]";
        gameObject.transform.GetChild(0).GetComponent<Image>().sprite = card.sprite;

        if(card.startingFunction != string.Empty)
        {
            Invoke(card.startingFunction, 0);
        }

        CM = GameObject.Find("MainCanvas").transform.Find("InGameUI").transform.Find("CardManager").GetComponent<CardManager> ();
    }
    
    public void ActivateCard()
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

        CM.RemoveFromHand(this.gameObject);
    }

    void SetResource(ResourceType type, int amount)
    {
        switch(type)
        {
            case ResourceType.Food:
                break;
        }
    }

    public void SetActiveCard(bool set)
    {
        if (set)
        {
            CM.activeCard = this.gameObject;
            gameObject.transform.SetAsLastSibling();
        }
        else
        {
            CM.activeCard = null;
            gameObject.transform.SetSiblingIndex(CM.GetIndexInHand(this.gameObject));
        }
    }
}
