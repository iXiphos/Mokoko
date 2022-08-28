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
        isMoving = true;
        PM = GameObject.Find("GameManager").GetComponent<PartyManager>();
    }

    private void Update()
    {
        float newY = 0;
        float baseOffset = 0.838f;
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
        //gameObject.transform.position = new Vector3(mainCamera.transform.position.x, 0, 0);
    }

    public void SetIsMoving(bool value)
    {
        isMoving = value;
        for (int i = 0; i < PM.party.Count; i++)
        {
            if (value)
                gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = allMovingSprites[PM.party[i].id];
            else
            {
                gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = allStationarySprites[PM.party[i].id];
                foreach (Transform child in this.transform)
                {
                    if (child.name != "Shadow")
                    {
                        child.transform.position = new Vector3(child.transform.position.x, 0.838f, 0);
                    }

                }
            }

        }
    }
}
