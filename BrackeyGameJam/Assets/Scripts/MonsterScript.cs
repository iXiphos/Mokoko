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
            int random = rand.Next(0, 2);
            gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            if(random == 0)
            {
                gameObject.GetComponent<SpriteRenderer>().color = new UnityEngine.Color(thisColor.r, thisColor.g, thisColor.b, 0);
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().color = new UnityEngine.Color(thisColor.r, thisColor.g, thisColor.b, 0.05f);
            }
            gameObject.transform.position = new Vector3(ogPosition.x + rand.Next(-10, 10), gameObject.transform.position.y, gameObject.transform.position.z);
        }


        if (sanityValue < 5)
        {
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            UnityEngine.Color thisColor = gameObject.GetComponent<SpriteRenderer>().color;
            gameObject.GetComponent<SpriteRenderer>().color = new UnityEngine.Color(thisColor.r, thisColor.g, thisColor.b, 0.4f);
        }


        if (sanityValue < 3)
        {
            gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            gameObject.transform.position = new Vector3(ogPosition.x + rand.Next(-2, 2), gameObject.transform.position.y, gameObject.transform.position.z);
            UnityEngine.Color thisColor = gameObject.GetComponent<SpriteRenderer>().color;
            gameObject.GetComponent<SpriteRenderer>().color = new UnityEngine.Color(thisColor.r, thisColor.g, thisColor.b, 1.0f);
            GameObject.Find("Main Camera").GetComponent<CameraScript>().StartShake();
        }


    }
}
