using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private GameObject Canvas;

    void Start()
    {
        Canvas = GameObject.Find("MainCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTransitionUI(int into)
    {
        Canvas.transform.Find("MapView").gameObject.SetActive(!Convert.ToBoolean(into));
        Canvas.transform.Find("InGameUI").gameObject.SetActive(Convert.ToBoolean(into));
    }
}
