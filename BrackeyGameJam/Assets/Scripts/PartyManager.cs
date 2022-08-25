using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PartyManager : MonoBehaviour
{
    private const int PARTY_SIZE = 4;
    private string[] allNames =
    {
        "Darrin", "Tanya", "Cannon", "Yamileth", "Garret", "Cayden", "Giancarlo", "Marcellus", "Shaina", "Matthias", "Logan", "Malcolm", "Celina", "Octavia", "Serenity", "Jaime", "Tyron", "Stetson", "Menachem", "Ken", "Trayvon", "Laken", "Cristobal", "Billy", "Allissa", "Jeffrey", "Michelle", "Janie", "Parker", "Cedrick", "Jalen", "Rico", "Dawson", "Akira", "Ezequiel", "Frank", "Dallin", "Kinsey", "Aylin", "Jacques", "Aditya", "Terrence", "Raul", "Sincere", "Demond", "Thomas", "Ruben", "Luciano", "Daryl", "Zechariah", "Treyvon", "Raymond", "Julianna", "Rowan", "Roxana", "Karolina", "Kristal", "Jordon", "Todd", "Grady", "Angela", "Renae", "Gloria", "Francisco", "Donavon", "Stone", "Eddie", "Rhianna", "Brad", "Hernan", "Rylee", "Deshawn", "Karly", "Lindsey", "Melia", "Sydney", "Amiya", "Penelope", "Summer", "Viridiana", "Talon", "Jaya", "Mikel", "Melvin", "Hailie", "Kalista", "Devyn", "William", "Casey", "Rick", "Albert", "Gene", "Treyton", "Meghan", "Gregory", "Cody", "Ginger", "Jase", "Devante", "Junior"
    };

    [SerializeField]
    private Sprite[] allSprites;
    public List<Survivor> party;
    private GameObject upgradeScreen;
    private bool firstTimeUpgrading;

    void Start()
    {
        party = new List<Survivor>(PARTY_SIZE);
        firstTimeUpgrading = true;
        upgradeScreen = GameObject.Find("MainCanvas").transform.Find("UpgradeScreen").gameObject;
        System.Random rand = new System.Random();

        for (int i = 0; i < PARTY_SIZE; i++)
        {
            party[i] = new Survivor(allNames[rand.Next(0, allNames.Length)], allSprites[rand.Next(0, allSprites.Length)]);
        }

        if(party.Count <= 0)
        {
            Debug.LogError("Party size needs to have at least a single survivor!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //0-6 {hunter, medic, chef, navigator, forager, mystic}, 0-4 Character #, Amount
    public void AddSkillPt(string input)
    {
        string[] allInput = input.Split(',');
        int characterNum = int.Parse(allInput[1]);
        int amount = int.Parse(allInput[2]);

        if(characterNum > party.Count)
        {
            return;
        }

        switch(int.Parse(allInput[0]))
        {
            case (0):
                party[characterNum].hunterSkill += amount;
                break;
            case (1):
                party[characterNum].medicSkill += amount;
                break;
            case (2):
                party[characterNum].chiefSkill += amount;
                break;
            case (3):
                party[characterNum].navigatorSkill += amount;
                break;
            case (4):
                party[characterNum].forgerSkill += amount;
                break;
            case (5):
                party[characterNum].mysticSkill += amount;
                break;
        }

        GameObject playerUpgrades = upgradeScreen.transform.Find("Player" + characterNum + " Upgrades").gameObject;

        playerUpgrades.transform.GetChild(int.Parse(allInput[0])).GetChild(0).GetComponent<TextMeshPro>().text = (int.Parse(playerUpgrades.transform.GetChild(int.Parse(allInput[0])).GetChild(0).GetComponent<TextMeshPro>().text) + 1).ToString();

        if (firstTimeUpgrading)
        {
            foreach (Transform child in playerUpgrades.transform)
            {
                child.GetComponent<Button>().enabled = false;
            }
        }
    }
}
