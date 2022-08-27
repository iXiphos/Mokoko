using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    NULL,
    Health,
    Sanity,
    Hunger
}

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string cardName;
    public string description;

    public Sprite sprite;

    [Header("Effects")]
    public List<short> deltaResource;
    public List<ResourceType> resourceType;

    public string startingFunction;
    public string ExternalFunction;

}
