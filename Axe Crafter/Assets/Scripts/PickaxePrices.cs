using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxePrices : MonoBehaviour {

    [SerializeField] int[] OreRequired;
    [SerializeField] int[] WoodRequired;
    [SerializeField] int GoldRequired = 10;

    public int GetOreRequired(int i) { return OreRequired[i]; }
    public int GetWoodRequired(int i) { return WoodRequired[i]; }
    public int GetGoldRequired() { return GoldRequired; }
}
