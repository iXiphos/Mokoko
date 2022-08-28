using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum LevelTypes
{
    Pitstop, 
    Reststop,
    Treasurestop
}

public class SceneManager : MonoBehaviour
{
    public GameObject[] pitStops;
    public GameObject[] restStops;
    public GameObject[] treasureStops;

    public LevelTypes currentType;

    private GameObject InGameUI;

    public void Awake()
    {
        currentType = LevelTypes.Reststop;
    }

    public void Start()
    {
        InGameUI = GameObject.Find("MainCanvas").transform.Find("InGameUI").gameObject;
    }

    public void EnterScene()
    {
        Camera.main.gameObject.GetComponent<Animator>().Play("Camera_In");
    }

    public void ExitScene()
    {
        Camera.main.gameObject.GetComponent<Animator>().Play("Camera_Out");
    }

    public void SpawnNextScene(LevelTypes levelType, string stopName = "")
    {
        currentType = levelType;
        GameObject newLevel = pitStops[0];
        gameObject.GetComponent<PartyManager>().upgradePoints++;
        System.Random rand = new System.Random();
        switch (levelType)
        {
            case LevelTypes.Pitstop:
                if (stopName == "")
                {
                    foreach (GameObject gm in pitStops)
                    {
                        if (gm.name == stopName)
                            newLevel = gm;
                    }
                }
                else
                {
                    newLevel = pitStops[rand.Next(0, pitStops.Length)];
                }
                InGameUI.transform.Find("UpgradeUI").gameObject.SetActive(false);
                break;

            case LevelTypes.Reststop:
                if (stopName == "")
                {
                    foreach (GameObject gm in pitStops)
                    {
                        if (gm.name == stopName)
                            newLevel = gm;
                    }
                }
                else
                {
                    newLevel = pitStops[rand.Next(0, pitStops.Length)];
                }
                InGameUI.transform.Find("UpgradeUI").gameObject.SetActive(true);
                GameObject.Find("MainCanvas").transform.Find("InGameUI").transform.Find("EventDescription").transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "ASDASD";

                break;

            case LevelTypes.Treasurestop:
                if (stopName == "")
                {
                    foreach (GameObject gm in pitStops)
                    {
                        if (gm.name == stopName)
                            newLevel = gm;
                    }
                }
                else
                {
                    newLevel = pitStops[rand.Next(0, pitStops.Length)];
                }
                InGameUI.transform.Find("UpgradeUI").gameObject.SetActive(false);
                break;
        }

        GameObject scene = Instantiate(newLevel, Vector3.zero, Quaternion.identity);
        scene.name = "Scene";
    }
}
