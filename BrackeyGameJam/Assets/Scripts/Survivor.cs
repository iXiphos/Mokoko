using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class Survivor
{
    public string name;
    public Sprite sprite;
    public int id;

    [Header("Stats")]
    private int health;
    private int hunger;
    private int sanity;

    private const int maxHealth = 10;
    private const int maxHunger = 10;
    private const int maxSanity = 10;

    public int Sanity
    {
        get
        {
            return sanity;
        }
        set
        {
            sanity += value;
            Mathf.Clamp(sanity, 0, maxSanity);
            Debug.Log("--[" + this.name + "] has taken " + value + " sanity!" + "Now has " + this.sanity);
            GameObject.Find("MainCanvas").transform.Find("InGameUI").Find("Characters").Find("Character" + id).GetComponent<CharStatsUI>().UpdateSanity(sanity, maxSanity);

            if (sanity <= 0)
            {
                Debug.Log("--[" + this.name + "] has DIED from sanity. L");
            }
        }
    }

    public int Hunger
    {
        get
        {
            return hunger;
        }
        set
        {
            hunger += value;
            Mathf.Clamp(sanity, 0, maxHunger);
            Debug.Log("--[" + this.name + "] has taken " + value + " hunger! Now has " + this.hunger);
            GameObject.Find("MainCanvas").transform.Find("InGameUI").Find("Characters").Find("Character" + id).GetComponent<CharStatsUI>().UpdateHunger(hunger, maxHunger);

            if (hunger <= 0)
            {
                Debug.Log("--[" + this.name + "] has DIED from hunger. L");
            }

        }
    }

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health += value;

            //Infection
            foreach(GameObject card in GameObject.Find("MainCanvas").transform.Find("InGameUI").transform.Find("CardManager").GetComponent<CardManager>().hand)
            {
                if(card.GetComponent<CardDisplay>().card.cardName == "Infection")
                {
                    GameObject.Find("MainCanvas").transform.Find("InGameUI").transform.Find("CardManager").GetComponent<CardManager>().Infection(value);
                }
                else if (card.GetComponent<CardDisplay>().card.cardName == "Amplifyer")
                {
                    health-=1;
                }
            }

            Debug.Log("--[" + this.name + "] has taken " + value + " damage!" +" Now has " + this.health);
            GameObject.Find("MainCanvas").transform.Find("InGameUI").Find("Characters").Find("Character" + id).GetComponent<CharStatsUI>().UpdateHealth(health, maxHealth);

            if(health <= 0)
            {
                Debug.Log("--[" + this.name + "] has DIED from health. L");
                GameObject.Find("GameManager").GetComponent<PartyManager>().party.Remove(this);
                MonoBehaviour.Destroy(GameObject.Find("MainCanvas").transform.Find("InGameUI").Find("Characters").Find("Character" + id).gameObject);
            }
        }
    }

    [Header("Skills")]
    public int hunterSkill;
    public int medicSkill;
    public int chiefSkill;
    public int navigatorSkill;
    public int forgerSkill;
    public int mysticSkill;





    public Survivor(string name, Sprite sprite, int id)
    {
        this.name = name;
        this.sprite = sprite;
        this.id = id;

        health = 10;
        hunger = 10;
        sanity = 10;
    }
}
