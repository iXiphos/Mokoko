using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    [SerializeField]
    private Survivor[] party;
    // Start is called before the first frame update
    void Start()
    {
        if(party.Length <= 0)
        {
            Debug.LogError("Party size needs to have at least a single survivor!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
