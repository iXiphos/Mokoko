using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Card card;
    private CardManager CM;
    private GameObject Characters;
    private Survivor target;
    private SceneManager SM;
    private Canvas mainCanvas;
    private PartyManager PM;

    void Start()
    {
        //Updating Card Resources
        this.gameObject.name = "Card[" + card.name + "]";
        UpdateCardInfo();
        gameObject.transform.GetChild(0).GetComponent<Image>().sprite = card.sprite;

        if(card.startingFunction != string.Empty)
        {
            Invoke(card.startingFunction, 0);
        }

        if(card.resourceType.Count != card.deltaResource.Count)
        {
            Debug.LogError(card.cardName +  ": Card MUST have the same amount of types and deltas");
        }

        target = null;
        mainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
        PM = GameObject.Find("GameManager").GetComponent<PartyManager>();
        SM = GameObject.Find("GameManager").GetComponent<SceneManager>();
        Characters = GameObject.Find("MainCanvas").transform.Find("InGameUI").Find("Characters").gameObject;
        CM = GameObject.Find("MainCanvas").transform.Find("InGameUI").transform.Find("CardManager").GetComponent<CardManager>();
    }
    
    public void ActivateCard()
    {
        //Card Starting Effects
        for(int i = 0; i < card.resourceType.Count; i++)
        {
            if (card.resourceType[i] != ResourceType.NULL || card.deltaResource[i] != 0)
            {
                SetResource(card.resourceType[i], card.deltaResource[i]);
            }
        }


        if (card.ExternalFunction != string.Empty)
        {
            Invoke(card.ExternalFunction, 0);
        }

        if(card.eventDescription != string.Empty)
        {
            Invoke("ShowEventDiscription", 1);
        }
    }

    private void ShowEventDiscription()
    {
        //Change to in game text box
        Debug.Log(target.name + card.eventDescription);
        GameObject.Find("MainCanvas").transform.Find("InGameUI").transform.Find("EventDescription").transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = card.eventDescription;
    }

    void SetResource(ResourceType type, int amount)
    {
        switch(type)
        {
            case ResourceType.Health:
                target.Health = amount;
                break;

            case ResourceType.Sanity:
                target.Sanity = amount;
                break;

            case ResourceType.Hunger:
                target.Hunger = amount;
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

    public void OnDrag(PointerEventData eventData)
    {
        this.GetComponent<RectTransform>().anchoredPosition += eventData.delta / mainCanvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        CM.draggingCard = this.gameObject;
        for (int i = 0; i < PM.party.Count; i++)
        {
            Characters.transform.Find("Character" + PM.party[i].id).GetComponent<CharStatsUI>().SelectState = true;
        }
    }

    public void UpdateCardInfo()
    {
        gameObject.transform.Find("Title").GetComponent<TextMeshProUGUI>().text = card.cardName;
        gameObject.transform.Find("Description").GetComponent<TextMeshProUGUI>().text = card.description;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        CM.draggingCard = null;
        for (int i = 0; i < PM.party.Count; i++)
        {
            float distanceBetweenElements = Vector2.Distance(this.GetComponent<RectTransform>().position, Characters.transform.GetChild(i).GetComponent<RectTransform>().position);
            if (distanceBetweenElements <= 0.2f)
            {
                target = PM.party[i];
                ActivateCard();
                Debug.Log("Card Targeted: " + Characters.transform.GetChild(i).name);
                CM.RemoveFromHand(this.gameObject);
                Destroy(this.gameObject);
            }

        }

        for (int i = 0; i < PM.party.Count; i++)
        {
            if (Characters.transform.Find("Character" + PM.party[i].id).gameObject != null)
            {
                Characters.transform.Find("Character" + PM.party[i].id).GetComponent<CharStatsUI>().SelectState = false;
            }
        }
    }

    //Unique Functions ---------------------------------------------------------------

    public void NobleSacrifice()
    {
        foreach(Survivor sur in PM.party)
        {
            if(sur != target)
            {
                sur.Sanity = 3;
                sur.Health = 3;
                sur.Hunger = 3;

            }
        }

        target.Health = -100;

    }

    public void Panecea()
    {
        foreach(GameObject card in CM.hand)
        {
            if(card.GetComponent<CardDisplay>().card.name == "Illness" || card.GetComponent<CardDisplay>().card.name == "Infection")
            {
                CM.RemoveFromHand(card);
                return;
            }
        }
    }

    public void Gamble()
    {
        target.Health = -3;
        if(CM.hand.Count > 0)
            CM.hand.RemoveAt(0);
    }

    public void Search()
    {
        CM.DrawCard(1);
    }

    public void Reorginzation()
    {
        CM.hand.Clear();
        CM.DrawCard(6);
    }

    public void Guide()
    {
        target.hunterSkill = -1;
        target.preventDamage = 2;
    }
}
