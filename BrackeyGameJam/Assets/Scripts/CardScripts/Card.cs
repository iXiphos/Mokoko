using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    NULL,
    Food,
    Water
}

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string name;
    public string description;

    public Sprite sprite;

    [Header("Effects")]
    public short deltaResource;
    public ResourceType resourceType;

    public string startingFunction;
    public string ExternalFunction;

}
