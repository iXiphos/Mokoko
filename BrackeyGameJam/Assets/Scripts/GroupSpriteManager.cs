using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GroupSpriteManager : MonoBehaviour
{
    private GameObject mainCamera;
    public bool isMoving;
    public Sprite[] allStationarySprites;
    public Sprite[] allMovingSprites;
    public float jumpAmount;
    public float jumpSpeed;
    private PartyManager PM;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        isMoving = false;
        PM = GameObject.Find("GameManager").GetComponent<PartyManager>();
        InvokeRepeating("UpdatePos", 0, 0.1f);
    }

    private void Update()
    {
        float newY = 0;
        float baseOffset = 0.625f;
        if (isMoving)
        {
            foreach (Transform child in this.transform)
            {
                if(child.name != "Shadow")
                {
                    newY = baseOffset + Mathf.Abs(Mathf.Sin((Time.timeSinceLevelLoad * jumpSpeed + (child.transform.localPosition.x))) * jumpAmount);
                    child.transform.position = new Vector3(child.transform.position.x, newY, 0);
                }
            }
        }
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(mainCamera.transform.position.x, 0, 0), 0.1f);
    }

    public void SetIsMoving(bool value)
    {
        isMoving = value;
        for (int i = 0; i < PM.party.Count; i++)
        {
            if (value)
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = allMovingSprites[PM.party[i].id];
            else
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = allStationarySprites[PM.party[i].id];
        }
    }
}