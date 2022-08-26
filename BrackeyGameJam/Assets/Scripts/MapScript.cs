using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    private const int mapDepth = 8;

    public short marginX;
    public short marginY;

    public GameObject nodePrefab;
    public RectTransform mapStart;
    private GameObject lastNodePlaced;
    public GameObject lastNodeVisited;
    public int currentLevel;

    private void Start()
    {
        GenerateMap();
    }

    public void UpdateMap()
    {
        currentLevel++;
        Debug.Log("Updating Map with level:" + currentLevel);

        foreach (Transform child in gameObject.transform)
        {
            if(child.gameObject.name != "MapStart")
            {
                MapNode theNode = child.GetComponent<MapNode>();
                child.GetComponent<Button>().enabled = false;
                child.GetComponent<Image>().color = Color.black;

                if (theNode.level == currentLevel)
                {
                    Debug.Log("Testing for" + theNode.gameObject.name);
                    foreach (GameObject parent in theNode.parents)
                    {
                        if (parent == lastNodeVisited)
                        {
                            child.GetComponent<Button>().enabled = true;
                            child.GetComponent<Image>().color = Color.white;
                            break;
                        }
                    }
                }
            }
        }
    }

    public void GenerateMap()
    {
        System.Random rand = new System.Random();
        for (int i = 0; i < mapDepth; i++)
        {
            GameObject node = Instantiate(nodePrefab, this.transform) as GameObject;
            node.GetComponent<RectTransform>().anchoredPosition = new Vector2(mapStart.anchoredPosition.x + 0, mapStart.anchoredPosition.y + i * marginY);
            node.GetComponent<MapNode>().level = i;
            node.gameObject.name = "Node " + i;

            switch (rand.Next(0, 3))
            {
                case (0):
                    node.GetComponent<MapNode>().nodeType = LevelTypes.Reststop;
                    break;

                case (1):
                    node.GetComponent<MapNode>().nodeType = LevelTypes.Pitstop;
                    break;

                case (2):
                    node.GetComponent<MapNode>().nodeType = LevelTypes.Treasurestop;
                    break;
            }

            if (i == 0)
            {
                lastNodeVisited = node;
                node.GetComponent<MapNode>().nodeType = LevelTypes.Reststop;
            }

            if (lastNodePlaced != null)
            {
                node.GetComponent<MapNode>().parents.Add(lastNodePlaced);
            }

            lastNodePlaced = node;
        }
    }

}
