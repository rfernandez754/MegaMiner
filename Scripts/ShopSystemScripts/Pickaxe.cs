using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pickaxe
{
    public string pickaxeName;
    public int pickaxeID;
    public int pickaxePrice;
    public int pickaxePower;

    public string unboughtImageName;
    public string boughtImageName;

    public bool bought;
    public bool equipped;
}
