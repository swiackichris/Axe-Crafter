using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodStats : MonoBehaviour {

    [SerializeField] int WoodHealth = 10;

    // Returns OreHealth
    public int GetWoodHealth() { return WoodHealth; }
}
