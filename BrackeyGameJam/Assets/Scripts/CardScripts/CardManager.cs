using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public GameObject cardObject;
    public Card tempCard;

    public int minCardMargin;
    public int maxCardMargin;
    public int totalHorizontalSize = 1000;

    public GameObject activeCard;

    Stack<Card> deck = new Stack<Card>();
    List<GameObject> hand = new List<GameObject>();
    List<Card> discardPile = new List<Card>();


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 200; i++)
        {
            deck.Push(tempCard);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(hand.Count > 0)
        {
            for(int i = 0; i < hand.Count; i++)
            {
                Vector2 newSize = new Vector2(1.0f, 1.0f);
                Vector2 newPos = new Vector2(Math.Clamp((totalHorizontalSize / hand.Count()), minCardMargin, maxCardMargin) * i, 0);
                if (hand[i] == activeCard)
                {
                    newPos.y += 250;
                    newSize = new Vector2(1.5f, 1.5f);
                }
                hand[i].GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(hand[i].GetComponent<RectTransform>().anchoredPosition, newPos, 0.05f);
                hand[i].GetComponent<RectTransform>().localScale = Vector2.Lerp(hand[i].GetComponent<RectTransform>().localScale, newSize, 0.05f);
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

    private void DrawCard(ushort amountOfTimes)
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
}
