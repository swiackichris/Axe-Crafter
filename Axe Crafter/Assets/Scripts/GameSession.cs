﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameSession : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] OreMinedText;
    [SerializeField] int[] MinedOreCounter;
    int PickLevel = 0;
    int PickUpgradeCounter = 0;

    [SerializeField] MobStats mobStatsScriptPrefab;
    int Gold = 0;
    

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

    // Updates the amount of ores after purchasing items in the market
    // TODO Prices added later
    public void BuyPickaxe(int i)
    {
        if (MinedOreCounter[i] >= 1)
        {
            MinedOreCounter[i] -= 1;
            OreMinedText[i].text = MinedOreCounter[i].ToString();
        }
    }

    // UpgradePickClass
    public int GetMinedOreCounter(int i) { return MinedOreCounter[i]; }
    public int GetPickLevel() { return PickLevel; }
    public int GetPickUpgradeCounter() { return PickUpgradeCounter; }
    public void IncreasePickLevel() { PickLevel++; }
    public void IncreasePickUpgradeCounter() { PickUpgradeCounter++; }
    public void ResetPickUpgradeCounter() { PickUpgradeCounter = 0; }
    
    // BATTLE
    public void CountGold()
    {
        Gold += mobStatsScriptPrefab.GetGoldReward();
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
