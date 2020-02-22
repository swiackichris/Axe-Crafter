using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxePrices : MonoBehaviour {

    [SerializeField] int[] OreRequired = null;
    [SerializeField] int[] WoodRequired = null;
    [SerializeField] int GoldRequired = 10;

    public int GetOreRequired(int i) { return OreRequired[i]; }
    public int GetWoodRequired(int i) { return WoodRequired[i]; }
    public int GetGoldRequired() { return GoldRequired; }
}
