using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameSession : MonoBehaviour
{
    // [SerializeField] GameObject CurrentPickaxe;
    [SerializeField] TextMeshProUGUI[] OreMinedText;                            // Displays amount of ore currently owned
    [SerializeField] int[] MinedOreCounter;                                     // Amount of ore currently owwned
    [SerializeField] private PickaxePrices[] pickaxePricesScriptPrefab;         // Temporary Solution
    [SerializeField] int PickLevel = 0;                                         // Current pick level
    [SerializeField] int PickUpgradeCounter = 0;                                // Current upgrade level (max is +9 for each pick)
    [SerializeField] int Gold = 0;                                              // Amount of gold owned

    int PARAMETER = 0;                                                          // Required for BuyPickaxe() function

    // Use this for initialization
    void Start()
    {
        // Checks if both arrays are of equal size, it's necessary to save properly.
        if (MinedOreCounter.Length != OreMinedText.Length) { Debug.LogError("MinedOreCounter.Length should be equal to OreMinedText.Length"); }

        // Initialization of displayed text
        for(int i=0; i<OreMinedText.Length; i++)
        {
            OreMinedText[i].text = MinedOreCounter[i].ToString();
        }
    }

    // Displays and updates the amount of ore mined
    public void CountMinedOre(int i)
    {
        // Increments ore mined
        MinedOreCounter[i] += 1;

        // Updates ore mined text
        OreMinedText[i].text = MinedOreCounter[i].ToString();
    }

    public void BuyPickaxe()
    {
        // Resets PARAMETER required for "for" functions
        PARAMETER = 0;

        for (int jj = PARAMETER; jj < 10; jj++) // Possibly add .Length method instead of 10 later
        {
            // Checks if we have enough supplies to upgrade
            if (MinedOreCounter[jj] >= pickaxePricesScriptPrefab[PickLevel].GetOreRequired(jj) 
                && pickaxePricesScriptPrefab[PickLevel].GetOreRequired(jj) >0)
            {
                print("MinedOreCounter[jj]: " +MinedOreCounter[jj] +" -= " +pickaxePricesScriptPrefab[PickLevel].GetOreRequired(jj) + " jj = " +jj);
                    
                // Deducts materials
                MinedOreCounter[jj] -= pickaxePricesScriptPrefab[PickLevel].GetOreRequired(jj);

                // Updates material count as a string
                OreMinedText[jj].text = MinedOreCounter[jj].ToString();
                PARAMETER = jj + 1;
                break;
                /// TODO Add Remove Wood
            }
        }
    }

    public void PayGoldForUpgrade()
    {
        // Deducts gold required for upgrade
        Gold -= pickaxePricesScriptPrefab[PickLevel].GetGoldRequired();
    }

    // UpgradePickClass
    public int GetMinedOreCounter(int i) { return MinedOreCounter[i]; }
    public int GetPickLevel() { return PickLevel; }
    public int GetPickUpgradeCounter() { return PickUpgradeCounter; }
    public int GetCurrentGold() { return Gold; }
    public void IncreasePickLevel() { PickLevel++; }
    public void IncreasePickUpgradeCounter() { PickUpgradeCounter++; }
    public void ResetPickUpgradeCounter() { PickUpgradeCounter = 0; }
    
    // Increases gold owned by the amount dropped by a mosnter
    public void CountGold()
    {
        Gold += FindObjectOfType<MobStats>().GetGoldReward();
        print("Gold =" + Gold);
    }

    // DATA saving related below:
    // Creates a file and saves variables to it
    void OnDisable()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat"); // Creates a data file in DataPath with name specified in "XXX"
        PlayerData data = new PlayerData();

        for(int i=0; i<OreMinedText.Length; i++) { data.MinedOreCounterData[i] = MinedOreCounter[i]; }
        data.PickUpgradeCounterData = PickUpgradeCounter;
        data.PickLevelData = PickLevel;
        data.GoldData = Gold;
        bf.Serialize(file, data);
        file.Close();
    }

    // Loads variables from a file already created
    void OnEnable()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat")) // checks if the file already exists, if it doesn't, it is created.
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open); // Opens a file to be used and Loaded.
            PlayerData data = bf.Deserialize(file) as PlayerData; // You could also cast is as PlayerData by putting (PlayerData) in front like: PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            for (int i = 0; i < OreMinedText.Length; i++) { MinedOreCounter[i] = data.MinedOreCounterData[i]; }
            PickUpgradeCounter = data.PickUpgradeCounterData;
            PickLevel = data.PickLevelData;
            Gold = data.GoldData;
        }
    }

    // Variables needed to save/load
    [Serializable]
    class PlayerData
    {
        public int[] MinedOreCounterData = new int[10]; // 10 in array has to always be equal to MinedOreCounter.Length
        public int PickUpgradeCounterData;
        public int PickLevelData;
        public int GoldData;
    }
}
