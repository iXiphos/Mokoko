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
    public int upgradePoints;

    [Header("Effects")]
    public List<Card> hunterDeck;
    public List<Card> medicDeck;
    public List<Card> chefDeck;
    public List<Card> navigatorDeck;
    public List<Card> foragerDeck;
    public List<Card> mysticDeck;
    public List<Card> eventDeck;


    void Awake()
    {
        party = new List<Survivor>(PARTY_SIZE);
        firstTimeUpgrading = true;
        upgradePoints = 4;
        upgradeScreen = GameObject.Find("MainCanvas").transform.Find("UpgradeScreen").gameObject;
        System.Random rand = new System.Random();

        for (int i = 0; i < PARTY_SIZE; i++)
        {
            party.Add(new Survivor(allNames[rand.Next(0, allNames.Length)], allSprites[rand.Next(0, allSprites.Length)], i));
        }

        if(party.Count <= 0)
        {
            Debug.LogError("Party size needs to have at least a single survivor!");
        }

        Debug.Log("Party size is:" + party.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenUpgradeMenu(bool open)
    {
        //upgradePoints += 2;
        if(open)
        {
            if(upgradePoints <= 0)
            {
                return;
            }
        }

        upgradeScreen.SetActive(open);
    }

    public void Rest()
    {
        foreach(Survivor sur in party)
        {
            sur.Health = 2;
        }

        GameObject.Find("MainCanvas").transform.Find("InGameUI").transform.Find("UpgradeUI").gameObject.SetActive(false);
    }

    public void Death(Survivor victim)
    {
        upgradeScreen.transform.Find("Player" + victim.id + " Upgrades").gameObject.SetActive(false);
        //Destroy(GameObject.Find("MainCamera").transform.GetChild(0).Find("Player" + victim.id).gameObject);
        party.Remove(victim);
    }

    //0-6 {hunter, medic, chef, navigator, forager, mystic}, 0-4 Character #, Amount
    public void AddSkillPt(string input)
    {

        string[] allInput = input.Split(',');
        int characterNum = int.Parse(allInput[1]);
        int amount = int.Parse(allInput[2]);

        GameObject playerUpgrades = upgradeScreen.transform.Find("Player" + characterNum + " Upgrades").gameObject;

        if (!firstTimeUpgrading)
        {
            foreach (Transform child in playerUpgrades.transform)
            {
                child.GetComponent<Button>().enabled = true;
                child.GetComponent<Image>().color = Color.white;
            }
        }

        upgradePoints--;

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
        
        playerUpgrades.transform.GetChild(int.Parse(allInput[0])).GetChild(0).GetComponent<TextMeshProUGUI>().text = (int.Parse(playerUpgrades.transform.GetChild(int.Parse(allInput[0])).GetChild(0).GetComponent<TextMeshProUGUI>().text) + 1).ToString();
        
        if (firstTimeUpgrading)
        {
            foreach (Transform child in playerUpgrades.transform)
            {
                child.GetComponent<Button>().enabled = false;
                child.GetComponent<Image>().color = Color.grey;
            }
        }

        Debug.Log("Upgrade points: " + upgradePoints);

        if(upgradePoints <= 0)
        {
            OpenUpgradeMenu(false);
        }

        
    }
}
