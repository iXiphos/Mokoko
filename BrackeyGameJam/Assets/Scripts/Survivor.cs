using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survivor
{
    public string name;
    public Sprite sprite;

    [Header("Stats")]
    public ushort health;
    public ushort hunger;
    public ushort sanity;

    [Header("Skills")]
    public ushort medicSkill;
    public ushort hunterSkill;
    public ushort forgerSkill;
    public ushort fighterSkill;

    public Survivor(string name, Sprite sprite)
    {
        this.name = name;
        this.sprite = sprite;

        health = 3;
        hunger = 10;
        sanity = 5;
    }
}
