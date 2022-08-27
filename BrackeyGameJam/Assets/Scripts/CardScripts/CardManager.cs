using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class CardManager : MonoBehaviour
{
    public GameObject cardObject;
    public Card tempCard;
    private Canvas mainCanvas;

    public int minCardMargin;
    public int maxCardMargin;
    public int totalHorizontalSize = 1000;

    public GameObject activeCard;
    private PartyManager PM;

    public GameObject draggingCard;
    public Vector2 dragCardPos;

    Stack<Card> deck = new Stack<Card>();
    public List<GameObject> hand = new List<GameObject>();
    List<Card> discardPile = new List<Card>();


    // Start is called before the first frame update
    void Start()
    {
        mainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
        PM = GameObject.Find("GameManager").GetComponent<PartyManager>();
        for (int i = 0; i < 200; i++)
        {
            deck.Push(tempCard);
        }
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
                        newPos.y += 400;
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

        if(Input.GetKeyDown(KeyCode.P))
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
        for(int i = 0; i < amountOfTimes; i++)
        {
            if (deck.Count() <= 0)
            {
                ShuffleDiscard();
                deck = new Stack<Card>(discardPile);
                discardPile.Clear();
            }
            else
            {
                GameObject card = Instantiate(cardObject, this.transform.GetChild(0));
                hand.Add(card);
                card.GetComponent<RectTransform>().Translate(Vector2.down * 5);
                card.GetComponent<CardDisplay>().card = deck.Pop();
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
            if(card.GetComponent<Card>().cardName == "Illness")
            {
                foreach(Survivor person in PM.party)
                {
                    person.Health -= 2;
                }
            }
        }
    }

    public void Infection(int amount)
    {
        foreach (Survivor person in PM.party)
        {
            person.Sanity -= amount;
        }
    }
}
