using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    private const int PARTY_SIZE = 4;
    private string[] allNames =
    {
        "Darrin", "Tanya", "Cannon", "Yamileth", "Garret", "Cayden", "Giancarlo", "Marcellus", "Shaina", "Matthias", "Logan", "Malcolm", "Celina", "Octavia", "Serenity", "Jaime", "Tyron", "Stetson", "Menachem", "Ken", "Trayvon", "Laken", "Cristobal", "Billy", "Allissa", "Jeffrey", "Michelle", "Janie", "Parker", "Cedrick", "Jalen", "Rico", "Dawson", "Akira", "Ezequiel", "Frank", "Dallin", "Kinsey", "Aylin", "Jacques", "Aditya", "Terrence", "Raul", "Sincere", "Demond", "Thomas", "Ruben", "Luciano", "Daryl", "Zechariah", "Treyvon", "Raymond", "Julianna", "Rowan", "Roxana", "Karolina", "Kristal", "Jordon", "Todd", "Grady", "Angela", "Renae", "Gloria", "Francisco", "Donavon", "Stone", "Eddie", "Rhianna", "Brad", "Hernan", "Rylee", "Deshawn", "Karly", "Lindsey", "Melia", "Sydney", "Amiya", "Penelope", "Summer", "Viridiana", "Talon", "Jaya", "Mikel", "Melvin", "Hailie", "Kalista", "Devyn", "William", "Casey", "Rick", "Albert", "Gene", "Treyton", "Meghan", "Gregory", "Cody", "Ginger", "Jase", "Devante", "Junior"
    };

    [SerializeField]
    private Sprite[] allSprites;
    public Survivor[] party;

    void Start()
    {
        party = new Survivor[PARTY_SIZE];
        System.Random rand = new System.Random();

        for (int i = 0; i < PARTY_SIZE; i++)
        {
            party[i] = new Survivor(allNames[rand.Next(0, allNames.Length)], allSprites[rand.Next(0, allSprites.Length)]);
        }

        if(party.Length <= 0)
        {
            Debug.LogError("Party size needs to have at least a single survivor!");
        }

        #if DEBUG
            Debug.Log("Party created!");
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
