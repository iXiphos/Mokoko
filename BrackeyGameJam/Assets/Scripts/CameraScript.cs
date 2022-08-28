using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private GameObject Canvas;
    private CardManager CM;
    private SceneManager SM;

    const int amount_to_lose_on_round_end = -2;

    private bool startLock = false;

    void Start()
    {
        Canvas = GameObject.Find("MainCanvas");
        CM = GameObject.Find("MainCanvas").transform.Find("InGameUI").transform.Find("CardManager").GetComponent<CardManager>();
        SM = GameObject.Find("GameManager").GetComponent<SceneManager>(); 
    }

    void Update()
    {
        
    }

    public void SetMovingOnSprites(int value)
    {
        GameObject.Find("PlayerSprites").GetComponent<GroupSpriteManager>().SetIsMoving(value == 0 ? true : false);
    }

    public void TransitioningScene(int into)
    {
        Canvas.transform.Find("MapView").gameObject.SetActive(!Convert.ToBoolean(into));
        Canvas.transform.Find("InGameUI").gameObject.SetActive(Convert.ToBoolean(into));

        if(into != 0)
        {
            //Into scene, camera is just entering the map
            GameObject.Find("CardManager").GetComponent<CardManager>().generateDeck();
            List<GameObject> hand = GameObject.Find("CardManager").GetComponent<CardManager>().hand;
            for (int i = hand.Count; i < 6; i++) GameObject.Find("CardManager").GetComponent<CardManager>().DrawCard(1);
            List<Survivor> survivors = GameObject.Find("GameManager").GetComponent<PartyManager>().party;

            if(GameObject.Find("GameManager").GetComponent<SceneManager>().currentType != LevelTypes.Reststop && startLock)
            {
                for (int i = 0; i < survivors.Count; i++)
                {
                    survivors[i].Sanity = amount_to_lose_on_round_end;
                    survivors[i].Hunger = amount_to_lose_on_round_end;
                }

                //Change to print in text box
                Debug.Log("All survivors take " + amount_to_lose_on_round_end + "'Hunger' and 'Sanity' damage");
                GameObject.Find("MainCanvas").transform.Find("InGameUI").transform.Find("EventDescription").transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "All survivors take: " + amount_to_lose_on_round_end + "'Hunger' and 'Sanity' damage";
            }

            startLock = true;

            if (hand.Count > 0)
            {
                CM.CheckForIllness();
            }
        }
        else
        {
            //Out of the scene, when camera is fully out of the map
            Canvas.transform.Find("MapView").GetChild(0).GetChild(0).GetComponent<MapScript>().UpdateMap();
        }
    }

    public void StartShake()
    {
        StartCoroutine(ShakeCamera());
    }

    [SerializeField] private float cameraShakeDuration = 0.5f;
    [SerializeField] private float cameraShakeDecreaseFactor = 1f;
    [SerializeField] private float cameraShakeAmount = 1f;
    // coroutine
    IEnumerator ShakeCamera()
    {
        var originalPos = this.transform.localPosition;
        var duration = cameraShakeDuration;
        while (duration > 0)
        {
            this.transform.localPosition = originalPos + UnityEngine.Random.insideUnitSphere * cameraShakeAmount;
            duration -= Time.deltaTime * cameraShakeDecreaseFactor;
            yield return null;
        }
        this.transform.localPosition = originalPos;
    }
}
