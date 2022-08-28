using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class CardManager : MonoBehaviour
{
    public GameObject cardObject;
    public Card tempCard;
    private Canvas mainCanvas;

    [HideInInspector]
    public int enhance_card;

    public int minCardMargin;
    public int maxCardMargin;
    public int totalHorizontalSize = 1000;

    public GameObject activeCard;
    private PartyManager PM;

    public GameObject draggingCard;

    List<Card> deck = new List<Card>();
    public List<GameObject> hand = new List<GameObject>();
    List<Card> discardPile = new List<Card>();
    


    // Start is called before the first frame update
    void Start()
    {
        mainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
        PM = GameObject.Find("GameManager").GetComponent<PartyManager>();

        generateDeck();
    }

    public void generateDeck()
    {
        if (deck.Count != 0) deck.Clear();
        List<Survivor> survivors = GameObject.Find("GameManager").GetComponent<PartyManager>().party;
        for (int i = 0; i < GameObject.Find("GameManager").GetComponent<PartyManager>().eventDeck.Count; i++)
        {
            deck.Add(GameObject.Find("GameManager").GetComponent<PartyManager>().eventDeck[i]);
        }
        for (int i = 0; i < survivors.Count; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                switch (j)
                {
                    case 0:
                        for (int k = 0; k <= survivors[i].hunterSkill; k++)
                        {
                            Debug.Log("Output");
                            deck.Add(GameObject.Find("GameManager").GetComponent<PartyManager>().hunterDeck[k]);
                        }
                        break;
                    case 1:
                        for (int k = 0; k <= survivors[i].medicSkill; k++)
                        {
                            deck.Add(GameObject.Find("GameManager").GetComponent<PartyManager>().medicDeck[k]);
                        }
                        break;
                    case 2:
                        for (int k = 0; k < survivors[i].chiefSkill; k++)
                        {
                            deck.Add(GameObject.Find("GameManager").GetComponent<PartyManager>().chefDeck[k]);
                        }
                        break;
                    case 3:
                        for (int k = 0; k < survivors[i].navigatorSkill; k++)
                        {
                            deck.Add(GameObject.Find("GameManager").GetComponent<PartyManager>().navigatorDeck[k]);
                        }
                        break;
                    case 4:
                        for (int k = 0; k < survivors[i].forgerSkill; k++)
                        {
                            deck.Add(GameObject.Find("GameManager").GetComponent<PartyManager>().foragerDeck[k]);
                        }
                        break;
                    case 5:
                        for (int k = 0; k <= survivors[i].mysticSkill; k++)
                        {
                            deck.Add(GameObject.Find("GameManager").GetComponent<PartyManager>().mysticDeck[k]);
                        }
                        break;
                }
            }
        }
        ShuffleDeck();
        //hand.Clear();
    }

    void Update()
    {
        if(hand.Count > 0)
        {
            for(int i = 0; i < hand.Count; i++)
            {
                if (hand[i] != draggingCard)
                {
                    Vector2 newSize = new Vector2(1.0f, 1.0f);
                    Vector2 newPos = new Vector2(Math.Clamp((totalHorizontalSize / hand.Count()), minCardMargin, maxCardMargin) * i, 0);
                    if (hand[i] == activeCard)
                    {
                        newPos.y += 500;
                        newSize = new Vector2(1.25f, 1.25f);
                    }
                    hand[i].GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(hand[i].GetComponent<RectTransform>().anchoredPosition, newPos, 0.05f);
                    hand[i].GetComponent<RectTransform>().localScale = Vector2.Lerp(hand[i].GetComponent<RectTransform>().localScale, newSize, 0.05f);
                }
                else
                {
                    Vector2 newSize = new Vector2(0.5f, 0.5f);
                    hand[i].GetComponent<RectTransform>().localScale = Vector2.Lerp(hand[i].GetComponent<RectTransform>().localScale, newSize, 0.05f);
                }

            }
        }

        if(Input.GetKeyDown(KeyCode.Home))
        {
            DrawCard(1);
        }
    }

    public int GetIndexInHand(GameObject gm)
    {
        for (int i = 0; i < hand.Count; i++)
        {
            if (gm == hand[i])
                return i;
        }

        return -1;
    }

    public void RemoveFromHand (GameObject gm)
    {
        hand.Remove(gm);
    }

    public void DrawCard(ushort amountOfTimes)
    {
        if(deck.Count > 0)
        {
            for (int i = 0; i < amountOfTimes; i++)
            {
                GameObject card = Instantiate(cardObject, this.transform.GetChild(0));
                hand.Add(card);
                card.GetComponent<RectTransform>().Translate(Vector2.down * 5);
                card.GetComponent<CardDisplay>().card = deck[0];
                deck.RemoveAt(0);
            }
        }
    }

    private void ShuffleDiscard()
    {
        System.Random rand = new System.Random();

        for (int i = 0; i < discardPile.Count; i++)
        {
            Card temp = discardPile[i];
            int randomIndex = rand.Next(i, discardPile.Count);
            discardPile[i] = discardPile[randomIndex];
            discardPile[randomIndex] = temp;
        }
    }

    public void CheckForIllness()
    {
        foreach(GameObject card in hand)
        {
            if(card.GetComponent<CardDisplay>().card.cardName == "Illness")
            {
                foreach(Survivor person in PM.party)
                {
                    person.Health = -2;
                }
            }
        }

        GameObject.Find("MainCanvas").transform.Find("InGameUI").transform.Find("EventDescription").transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "These cards [Illness, Infection, Amplifier] will cause effects while they're in hand.";

    }

    public void Infection(int amount)
    {
        foreach (Survivor person in PM.party)
        {
            person.Sanity = -amount;
        }
        GameObject.Find("MainCanvas").transform.Find("InGameUI").transform.Find("EventDescription").transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "These cards [Illness, Infection, Amplifier] will cause effects while they're in hand.";

    }

    public void BoostNextCard(int amount)
    {
        enhance_card += amount;
    }

    private void ShuffleDeck()
    {
        System.Random rand = new System.Random();

        for (int i = 0; i < deck.Count; i++)
        {
            Card temp = deck[i];
            int randomIndex = rand.Next(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

}
