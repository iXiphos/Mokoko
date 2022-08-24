using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survivor
{
    public string name;
    public Sprite sprite;

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
            sanity = Mathf.Clamp(value, 0, maxSanity);
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
            hunger = Mathf.Clamp(value, 0, maxHunger);
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
            health = Mathf.Clamp(value, 0, maxHealth);
        }
    }

    [Header("Skills")]
    public int hunterSkill;
    public int medicSkill;
    public int chiefSkill;
    public int navigatorSkill;
    public int forgerSkill;
    public int mysticSkill;





    public Survivor(string name, Sprite sprite)
    {
        this.name = name;
        this.sprite = sprite;

        health = 10;
        hunger = 10;
        sanity = 10;
    }
}
