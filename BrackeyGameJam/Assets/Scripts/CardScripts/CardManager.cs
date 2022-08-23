using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    Stack<Card> deck = new Stack<Card>();
    List<Card> hand = new List<Card>();
    List<Card> discardPile = new List<Card>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DrawCard(int amountOfTimes)
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
                hand.Add(deck.Pop());
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
