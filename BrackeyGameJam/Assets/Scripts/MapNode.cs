using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode : MonoBehaviour
{
    public LevelTypes nodeType;

    public void UpdateNextSceneType()
    {
        GameObject.Find("GameManager").GetComponent<SceneManager>().nextLevelType = this.nodeType;
    }
}
