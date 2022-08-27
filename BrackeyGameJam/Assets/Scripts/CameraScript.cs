using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private GameObject Canvas;
    private SceneManager SM;

    void Start()
    {
        Canvas = GameObject.Find("MainCanvas");
        SM = GameObject.Find("GameManager").GetComponent<SceneManager>(); 
    }

    void Update()
    {
        
    }

    public void TransitioningScene(int into)
    {
        Canvas.transform.Find("MapView").gameObject.SetActive(!Convert.ToBoolean(into));
        Canvas.transform.Find("InGameUI").gameObject.SetActive(Convert.ToBoolean(into));

        if(into != 0)
        {
            //Out of scene
            GameObject.Find("CardManager").GetComponent<CardManager>().generateDeck();
            GameObject.Find("CardManager").GetComponent<CardManager>().DrawCard(6);
        }
        else
        {
            //Into Scene
            Canvas.transform.Find("MapView").GetChild(0).GetChild(0).GetComponent<MapScript>().UpdateMap();
        }
    }
}
