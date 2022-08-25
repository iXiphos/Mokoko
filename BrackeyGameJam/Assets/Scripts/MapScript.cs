using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    private const short mapBoundsX = 8;
    private const short mapBoundsY = 25;

    public GameObject nodePrefab;

    public void GenerateMap()
    {
        //row col
        MapNode[,] grid = new MapNode[10,4];
        int level = 0;
        //Create First Node
    }

}
