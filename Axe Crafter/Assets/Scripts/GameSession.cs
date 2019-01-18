using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameSession : MonoBehaviour
{
    // Pickaxe variables
    [SerializeField] TextMeshProUGUI[] OreMinedText;                            // Displays amount of ore currently owned
    [SerializeField] int[] MinedOreCounter;                                     // Amount of ore currently owned
    [SerializeField] private PickaxePrices[] pickaxePricesScriptPrefab;         // Temporary Solution
    [SerializeField] int PickLevel = 0;                                         // Current pick level
    [SerializeField] int PickUpgradeCounter = 0;                                // Current pick upgrade level (max is +9 for each pick)

    // Axe variables
    [SerializeField] TextMeshProUGUI[] WoodChoppedText;
    [SerializeField] int[] ChoppedWoodCounter;                                  // Amount of wood currently owned
    [SerializeField] private AxePrices[] axePricesScriptPrefab;                 // Temporary Solution
    [SerializeField] int AxeLevel = 0;                                          // Current axe level
    [SerializeField] int AxeUpgradeCounter = 0;                                 // Cuyrrent axe upgrade level

    [SerializeField] int Gold = 0;                                              // Amount of gold owned
    [SerializeField] TextMeshProUGUI GoldText;

    int PARAMETER = 0;                                                          // Required for BuyPickaxe() function

    // Use this for initialization
    void Start()
    {
        // Initialization of displayed text
        GoldText.text = Gold.ToString();

        /// TODO if (pickaxePricesScriptPrefab.Length != MinedOreCounter.Length) { Debug.LogError("pickaxePricesScriptPrefab.Length != MinedOreCounter.Length"); }

        // Checks if both arrays are of equal size, it's necessary to save properly.
        if (MinedOreCounter.Length != OreMinedText.Length) { Debug.LogError("MinedOreCounter.Length should be equal to OreMinedText.Length"); }

        // Initialization of displayed text
        for (int i=0; i<OreMinedText.Length; i++)
        {
            OreMinedText[i].text = MinedOreCounter[i].ToString();
        }

        /// TODO if (axePricesScriptPrefab.Length != ChoppedWoodCounter.Length) { Debug.LogError("axePricesScriptPrefab.Length != ChoppedWoodCounter.Length"); }

        // Checks if both arrays are of equal size, it's necessary to save properly.
        if (ChoppedWoodCounter.Length != WoodChoppedText.Length) { Debug.LogError("ChoppedWoodCounter.Length should be equal to WoodChoppedText.Length"); }

        // Initialization of displayed text
        for (int i = 0; i < WoodChoppedText.Length; i++)
        {
            WoodChoppedText[i].text = ChoppedWoodCounter[i].ToString();
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

    // Displays and updates the amount of ore mined
    public void CountChoppedWood(int i)
    {
        // Increments ore mined
        ChoppedWoodCounter[i] += 1;

        // Updates ore mined text
        WoodChoppedText[i].text = ChoppedWoodCounter[i].ToString();
    }

    public void BuyPickaxe()
    {
        // Resets PARAMETER required for "for" functions
        PARAMETER = 0;

        // For Ore Prices
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
                // PARAMETER = jj + 1;
            }
        }

        // Resets PARAMETER required for "for" functions
        PARAMETER = 0;

        // For Wood Prices
        for (int jj = PARAMETER; jj < 10; jj++) // Possibly add .Length method instead of 10 later
        {
            // Checks if we have enough supplies to upgrade
            if (ChoppedWoodCounter[jj] >= pickaxePricesScriptPrefab[PickLevel].GetWoodRequired(jj)
                && pickaxePricesScriptPrefab[PickLevel].GetWoodRequired(jj) > 0)
            {
                print("ChoppedWoodCounter[jj]: " + ChoppedWoodCounter[jj] + " -= " + pickaxePricesScriptPrefab[PickLevel].GetWoodRequired(jj) + " jj = " + jj);

                // Deducts materials
                ChoppedWoodCounter[jj] -= pickaxePricesScriptPrefab[PickLevel].GetWoodRequired(jj);

                // Updates material count as a string
                WoodChoppedText[jj].text = ChoppedWoodCounter[jj].ToString();
                // PARAMETER = jj + 1;
            }
        }
    }

    // TODO buy axe function might need to use different parameter, but i dont think so
    public void BuyAxe()
    {
        // Resets PARAMETER required for "for" functions
        PARAMETER = 0;

        for (int jj = PARAMETER; jj < 10; jj++) // Possibly add .Length method instead of 10 later
        {
            // Checks if we have enough supplies to upgrade
            if (MinedOreCounter[jj] >= axePricesScriptPrefab[AxeLevel].GetOreRequired(jj)
                && axePricesScriptPrefab[AxeLevel].GetOreRequired(jj) > 0)
            {
                print("MinedOreCounter[jj]: " + MinedOreCounter[jj] + " -= " + axePricesScriptPrefab[AxeLevel].GetOreRequired(jj) + " jj = " + jj);

                // Deducts materials
                MinedOreCounter[jj] -= axePricesScriptPrefab[AxeLevel].GetOreRequired(jj);

                // Updates material count as a string
                OreMinedText[jj].text = MinedOreCounter[jj].ToString();
                // PARAMETER = jj + 1;
            }
        }

        // Resets PARAMETER required for "for" functions
        PARAMETER = 0;

        for (int jj = PARAMETER; jj < 10; jj++) // Possibly add .Length method instead of 10 later
        {
            // Checks if we have enough supplies to upgrade
            if (ChoppedWoodCounter[jj] >= axePricesScriptPrefab[AxeLevel].GetWoodRequired(jj)
                && axePricesScriptPrefab[AxeLevel].GetWoodRequired(jj) > 0)
            {
                print("ChoppedWoodCounter[jj]: " + ChoppedWoodCounter[jj] + " -= " + axePricesScriptPrefab[AxeLevel].GetWoodRequired(jj) + " jj = " + jj);

                // Deducts materials
                ChoppedWoodCounter[jj] -= axePricesScriptPrefab[AxeLevel].GetWoodRequired(jj);

                // Updates material count as a string
                WoodChoppedText[jj].text = ChoppedWoodCounter[jj].ToString();
                // PARAMETER = jj + 1;
            }
        }
    }

    public void PayGoldForPickUpgrade()
    {
        // Deducts gold required for upgrade
        Gold -= pickaxePricesScriptPrefab[PickLevel].GetGoldRequired();

        // Updates amount of gold text
        GoldText.text = Gold.ToString();
    }

    public void PayGoldForAxeUpgrade()
    {
        // Deducts gold required for upgrade
        Gold -= axePricesScriptPrefab[AxeLevel].GetGoldRequired();

        // Updates amount of gold text
        GoldText.text = Gold.ToString();
    }

    // Gold
    public int GetCurrentGold() { return Gold; }

    // UpgradePickClass
    public int GetMinedOreCounter(int i) { return MinedOreCounter[i]; }
    public int GetPickLevel() { return PickLevel; }
    public int GetPickUpgradeCounter() { return PickUpgradeCounter; }
    public void IncreasePickLevel() { PickLevel++; }
    public void IncreasePickUpgradeCounter() { PickUpgradeCounter++; }
    public void ResetPickUpgradeCounter() { PickUpgradeCounter = 0; }

    // UpgradeAxeClass
    public int GetChoppedWoodCounter(int i) { return ChoppedWoodCounter[i]; }
    public int GetAxeLevel() { return AxeLevel; }
    public int GetAxeUpgradeCounter() { return AxeUpgradeCounter; }
    public void IncreaseAxeLevel() { AxeLevel++; }
    public void IncreaseAxeUpgradeCounter() { AxeUpgradeCounter++; }
    public void ResetAxeUpgradeCounter() { AxeUpgradeCounter = 0; }

    // Increases gold owned by the amount dropped by a monster
    public void CountGold()
    {
        Gold += FindObjectOfType<MobStats>().GetGoldReward();
        print("Gold =" + Gold);

        // Updates amount of gold text
        GoldText.text = Gold.ToString();
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

        for (int i = 0; i < WoodChoppedText.Length; i++) { data.ChoppedWoodCounterData[i] = ChoppedWoodCounter[i]; }
        data.AxeUpgradeCounterData = AxeUpgradeCounter;
        data.AxeLevelData = AxeLevel;

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

            for (int i = 0; i < WoodChoppedText.Length; i++) { ChoppedWoodCounter[i] = data.ChoppedWoodCounterData[i]; }
            AxeUpgradeCounter = data.AxeUpgradeCounterData;
            AxeLevel = data.AxeLevelData;

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

        public int[] ChoppedWoodCounterData = new int[10]; // 10 in array has to always be equal to MinedOreCounter.Length
        public int AxeUpgradeCounterData;
        public int AxeLevelData;

        public int GoldData;
    }
}
