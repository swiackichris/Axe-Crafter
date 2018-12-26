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
    [SerializeField] int Gold = 0;                                                               // Amount of gold owned

    int P = 0;                                                                  // Required for BuyPickaxe() function

    // Use this for initialization
    void Start()
    {
        // Checks if both arrays are of equal size, it's necessary to save properly.
        if (MinedOreCounter.Length != OreMinedText.Length) { Debug.LogError("MinedOreCounter.Length should be equal to OreMinedText.Length"); }
        for(int i=0; i<OreMinedText.Length; i++)
        {
            OreMinedText[i].text = MinedOreCounter[i].ToString();
        }
    }

    // Displays and updates the amount of ore mined
    public void CountMinedOre(int i)
    {
        MinedOreCounter[i] += 1;
        OreMinedText[i].text = MinedOreCounter[i].ToString();
    }

    /*public void BuyPickaxe(int i)
    {
        /// TODO Add Remove Gold
        if (MinedOreCounter[i] >= 1)
        {
            for (int j = 0; j < 4; j++) // You might not need it
            {
                for (int jj = P; jj < 10; jj++) // Possibly add .Length method instead of 10 later
                {
                    if (FindObjectOfType<PickaxePrices>().GetOreRequired(jj) > 0)
                    {
                        MinedOreCounter[jj] -= pickaxePricesScriptPrefab[PickLevel].GetOreRequired(jj);
                        /// TODO Add Remove Wood
                        OreMinedText[jj].text = MinedOreCounter[jj].ToString();
                        P = jj + 1;
                        break;
                    }
                }
            }
        }
    }*/

    public void BuyPickaxe()
    {
        /// TODO Add Remove Gold
        for (int j = 0; j < 4; j++) // You might not need it
        {
            for (int jj = P; jj < 10; jj++) // Possibly add .Length method instead of 10 later
            {
                if (MinedOreCounter[jj] < pickaxePricesScriptPrefab[PickLevel].GetOreRequired(jj))
                {
                    // Insufficient Materials
                    // Button Disable?
                    // Move The Insufficient Materials Check to UpgradePick
                }
                else
                {
                    MinedOreCounter[jj] -= pickaxePricesScriptPrefab[PickLevel].GetOreRequired(jj);
                    /// TODO Add Remove Wood
                    OreMinedText[jj].text = MinedOreCounter[jj].ToString();
                    P = jj + 1;
                    break;
                }
            }
        }
    }

    // UpgradePickClass
    public int GetMinedOreCounter(int i) { return MinedOreCounter[i]; }
    public int GetPickLevel() { return PickLevel; }
    public int GetPickUpgradeCounter() { return PickUpgradeCounter; }
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

    [Serializable]
    class PlayerData
    {
        public int[] MinedOreCounterData = new int[10]; // 10 in array has to always be equal to MinedOreCounter.Length
        public int PickUpgradeCounterData;
        public int PickLevelData;
        public int GoldData;
    }
}
