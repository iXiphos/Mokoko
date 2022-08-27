using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    private const int mapDepth = 12;

    public short marginX;
    public short marginY;

    public int min_rooms = 2;
    public int max_rooms = 5;
    public float randomRangeOffsetX;
    public float randomRangeOffsetY;

    public GameObject nodePrefab;
    public RectTransform mapStart;
    private GameObject lastNodePlaced;
    public GameObject lastNodeVisited;
    public int currentLevel;

    // Variables for handling weighted numbers
    public int wieghtPrecentIncrease = 10;
    public List<int> baseWeights;
    List<int> weights;

    private void Start()
    {
        weights = new List<int>();
        currentLevel = 0;
        resetWeights();
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
                        child.GetComponent<Button>().enabled = true;
                        child.GetComponent<Image>().color = Color.white;
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
            int random_size = i == 0 ? 1 : Random.Range(1, max_rooms + 1);
            for (int j = 0; j < random_size; j++)
            {
                GameObject node = Instantiate(nodePrefab, this.transform) as GameObject;
                float xPos = (mapStart.anchoredPosition.x - (marginX * random_size/2) + marginX*j) + Random.Range(-randomRangeOffsetY, randomRangeOffsetY); ;
                float yPos = (mapStart.anchoredPosition.y + (gameObject.GetComponent<RectTransform>().rect.height / mapDepth) * i) + Random.Range(-randomRangeOffsetY, randomRangeOffsetY);
                node.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, yPos);
                node.GetComponent<MapNode>().level = i;
                node.gameObject.name = "Node " + i + "_" + j;

                // Calcuted random weighted number
                int sum_of_weight = 0;
                int size = weights.Count;
                int randomRoom = 0;
                for (int k = 0; k < size; k++)
                {
                    sum_of_weight += weights[k];
                }
                int rnd = rand.Next(0, sum_of_weight);
                for (int k = 0; k < size; k++)
                {
                    if (rnd < weights[k])
                    {
                        randomRoom = k;
                        break;
                    }
                    rnd -= weights[k];
                }

                switch (randomRoom)
                {
                    case (0):
                        node.GetComponent<MapNode>().nodeType = LevelTypes.Pitstop;
                        break;

                    case (1):
                        node.GetComponent<MapNode>().nodeType = LevelTypes.Reststop;
                        resetWeights();
                        break;

                    case (2):
                        resetWeights();
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
            updateWeights();
        }
    }

    void updateWeights()
    {
        // Fixme: not hard code this
        for (int i = 0; i < weights.Count; i++)
        {
            switch (i)
            {
                case (0):
                    weights[i] -= wieghtPrecentIncrease;
                    break;
                case (1):
                    weights[i] += wieghtPrecentIncrease;
                    break;
            }
        }
    }

    void resetWeights()
    {
        if (weights.Count != 0) weights.Clear();
        for (int i = 0; i < baseWeights.Count; i++)
            weights.Add(baseWeights[i]);
    }

}