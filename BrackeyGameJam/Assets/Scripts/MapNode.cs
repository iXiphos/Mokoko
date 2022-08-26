using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MapNode : MonoBehaviour
{
    public LevelTypes nodeType;
    public int level;
    public List<GameObject> parents;

    public Sprite[] mapSprites;

    public void Start()
    {
        gameObject.GetComponent<Image>().sprite = mapSprites[(int)nodeType];
    }

    public void UpdateNextSceneType()
    {
        SceneManager SM = GameObject.Find("GameManager").GetComponent<SceneManager>();
        SM.EnterScene();
        SM.SpawnNextScene(this.nodeType);
    }

    public void SetAsPrevNode()
    {
        gameObject.transform.parent.GetComponent<MapScript>().lastNodeVisited = this.gameObject;
    }
}
