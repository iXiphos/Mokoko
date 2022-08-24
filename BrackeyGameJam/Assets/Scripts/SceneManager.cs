using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelTypes
{
    Pitstop, 
    Reststop,
    Treasurestop
}

public class SceneManager : MonoBehaviour
{
    private GameObject Canvas;

    public LevelTypes nextLevelType;
    public GameObject[] pitStops;
    public GameObject[] restStops;
    public GameObject[] treasureStops;


    public void Start()
    {
        Canvas = GameObject.Find("MainCanvas");
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
        GameObject newLevel = pitStops[0];
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
                    System.Random rand = new System.Random();
                    newLevel = pitStops[rand.Next(0, pitStops.Length)];
                }
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
                    System.Random rand = new System.Random();
                    newLevel = pitStops[rand.Next(0, pitStops.Length)];
                }
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
                    System.Random rand = new System.Random();
                    newLevel = pitStops[rand.Next(0, pitStops.Length)];
                }
                break;
        }

        GameObject scene = Instantiate(newLevel, Vector3.zero, Quaternion.identity);
        scene.name = "Scene";
    }
}
