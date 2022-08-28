using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    System.Random rand = new System.Random();
    Vector3 ogPosition;
    float sanityValue;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DoStuff", 0, 4);
        ogPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DoStuff()
    {
        foreach(Survivor sur in GameObject.Find("GameManager").GetComponent<PartyManager>().party)
        {
            sanityValue += sur.Sanity;
        }

        sanityValue /= 4;

        if(sanityValue < 10)
        {
            UnityEngine.Color thisColor = gameObject.GetComponent<SpriteRenderer>().color;
            gameObject.GetComponent<SpriteRenderer>().color = new UnityEngine.Color(thisColor.r, thisColor.g, thisColor.b, 0.2f);
        }


        if (sanityValue < 5)
        {
            UnityEngine.Color thisColor = gameObject.GetComponent<SpriteRenderer>().color;
            gameObject.GetComponent<SpriteRenderer>().color = new UnityEngine.Color(thisColor.r, thisColor.g, thisColor.b, 0.5f);
        }


        if (sanityValue < 3)
        {
            gameObject.transform.position = new Vector3(ogPosition.x + rand.Next(-2, 2), gameObject.transform.position.y, gameObject.transform.position.z);
            UnityEngine.Color thisColor = gameObject.GetComponent<SpriteRenderer>().color;
            gameObject.GetComponent<SpriteRenderer>().color = new UnityEngine.Color(thisColor.r, thisColor.g, thisColor.b, 1.0f);
            GameObject.Find("Main Camera").GetComponent<CameraScript>().StartShake();
        }


    }
}
