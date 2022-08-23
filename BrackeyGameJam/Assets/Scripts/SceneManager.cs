using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private GameObject Canvas;

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
}
