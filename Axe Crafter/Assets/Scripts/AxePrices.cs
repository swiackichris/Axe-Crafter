using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxePrices : MonoBehaviour {

    [SerializeField] int[] OreRequired;
    [SerializeField] int[] WoodRequired;
    [SerializeField] int GoldRequired = 10;

    // Use this for initialization
    void Start()
    {
        // FindObjectOfType<GameSession>().GetMinedOreCounter
        // Checking if Size of OreRequired and Wood Required is Equal to GetMinedOreCounter.Length.
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetOreRequired(int i) { return OreRequired[i]; }
    public int GetWoodRequired(int i) { return WoodRequired[i]; }
    public int GetGoldRequired() { return GoldRequired; }
}
