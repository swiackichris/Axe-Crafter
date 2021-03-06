﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameSession : MonoBehaviour
{
    // Pickaxe variables
    [SerializeField] TextMeshProUGUI[] OreMinedText = null;                            // Displays amount of ore currently owned
    [SerializeField] int[] MinedOreCounter = null;                                     // Amount of ore currently owned
    [SerializeField] private PickaxePrices[] pickaxePricesScriptPrefab = null;         // Temporary Solution
    [SerializeField] int PickLevel = 0;                                         // Current pick level
    [SerializeField] int PickUpgradeCounter = 0;                                // Current pick upgrade level (max is +9 for each pick)

    // Axe variables
    [SerializeField] TextMeshProUGUI[] WoodChoppedText = null;                         // Displays amount of wood currently owned
    [SerializeField] int[] ChoppedWoodCounter = null;                                  // Amount of wood currently owned
    [SerializeField] private AxePrices[] axePricesScriptPrefab = null;                 // Temporary Solution
    [SerializeField] int AxeLevel = 0;                                          // Current axe level
    [SerializeField] int AxeUpgradeCounter = 0;                                 // Cuyrrent axe upgrade level

    [SerializeField] private MobStats[] mobStats = null;
    [SerializeField] float Gold = 0;                                            // Amount of gold owned
    [SerializeField] TextMeshProUGUI GoldText = null;                                  // Text to display amount of gold owned

    // Level variables
    [SerializeField] MineLevelManager mineLevelManagerScriptPrefab = null;
    [SerializeField] ForestLevelManager forestLevelManagerScriptPrefab = null;
    [SerializeField] BattleLevelManager battleLevelManagerScriptPrefab = null;
    [SerializeField] int CurrentMineLevel = 0;
    [SerializeField] int CurrentForestLevel = 0;
    [SerializeField] int CurrentBattleLevel = 0;
    [SerializeField] int DragonsKilled = 0;

    private readonly int CraftToolMultiplier = 5;
    private readonly float UpgradeToolMultiplier = 1.05f;


    // Use this for initialization
    void Start()
    {
        // Initialization of displayed text
        GoldText.text = Math.Round(Gold, 1).ToString();

        // Checks if both arrays are of equal size, it's necessary to save properly.
        if (MinedOreCounter.Length != OreMinedText.Length) { Debug.LogError("MinedOreCounter.Length should be equal to OreMinedText.Length"); }

        // Initialization of displayed text
        for (int i=0; i<OreMinedText.Length; i++)
        {
            OreMinedText[i].text = MinedOreCounter[i].ToString();
        }

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
        // For Ore Prices
        for (int i = 0; i < 10; i++)
        {
            // Checks if we have enough supplies to upgrade
            if (MinedOreCounter[i] >= pickaxePricesScriptPrefab[PickLevel+1].GetOreRequired(i) 
                && pickaxePricesScriptPrefab[PickLevel+1].GetOreRequired(i) >0)
            {                    
                // Deducts materials
                MinedOreCounter[i] -= pickaxePricesScriptPrefab[PickLevel+1].GetOreRequired(i);

                // Updates material count as a string
                OreMinedText[i].text = MinedOreCounter[i].ToString();
            }
        }

        // For Wood Prices
        for (int i = 0; i < 10; i++)
        {
            // Checks if we have enough supplies to upgrade
            if (ChoppedWoodCounter[i] >= pickaxePricesScriptPrefab[PickLevel+1].GetWoodRequired(i)
                && pickaxePricesScriptPrefab[PickLevel+1].GetWoodRequired(i) > 0)
            {
                // Deducts materials
                ChoppedWoodCounter[i] -= pickaxePricesScriptPrefab[PickLevel+1].GetWoodRequired(i);

                // Updates material count as a string
                WoodChoppedText[i].text = ChoppedWoodCounter[i].ToString();
            }
        }
    }

    public void BuyAxe()
    {
        for (int i = 0; i < 10; i++)
        {
            // Checks if we have enough supplies to upgrade
            if (MinedOreCounter[i] >= axePricesScriptPrefab[AxeLevel + 1].GetOreRequired(i)
                && axePricesScriptPrefab[AxeLevel + 1].GetOreRequired(i) > 0)
            {
                // Deducts materials
                MinedOreCounter[i] -= axePricesScriptPrefab[AxeLevel + 1].GetOreRequired(i);

                // Updates material count as a string
                OreMinedText[i].text = MinedOreCounter[i].ToString();
            }
        }

        for (int i = 0; i < 10; i++)
        {
            // Checks if we have enough supplies to upgrade
            if (ChoppedWoodCounter[i] >= axePricesScriptPrefab[AxeLevel+1].GetWoodRequired(i)
                && axePricesScriptPrefab[AxeLevel+1].GetWoodRequired(i) > 0)
            {
                // Deducts materials
                ChoppedWoodCounter[i] -= axePricesScriptPrefab[AxeLevel+1].GetWoodRequired(i);

                // Updates material count as a string
                WoodChoppedText[i].text = ChoppedWoodCounter[i].ToString();
            }
        }
    }

    // Deducts gold required for upgrade
    public void PayGoldForPickUpgrade()
    {
        if (PickUpgradeCounter == 9) { Gold -= pickaxePricesScriptPrefab[PickLevel].GetGoldRequired() * CraftToolMultiplier; }   
        else { Gold -= pickaxePricesScriptPrefab[PickLevel].GetGoldRequired()* (float)Math.Pow(UpgradeToolMultiplier, PickUpgradeCounter); }  
        GoldText.text = Math.Round(Gold, 1).ToString();
    }

    // Deducts gold required for upgrade
    public void PayGoldForAxeUpgrade()
    {
        if (AxeUpgradeCounter == 9) { Gold -= axePricesScriptPrefab[AxeLevel].GetGoldRequired() * CraftToolMultiplier; }
        else { Gold -= axePricesScriptPrefab[AxeLevel].GetGoldRequired() * (float)Math.Pow(UpgradeToolMultiplier, AxeUpgradeCounter); }
        GoldText.text = Math.Round(Gold, 1).ToString();
    }

    // Deducts ore for unlocking mine levels
    public void PayMaterialsForMineUnlock(int i)
    {
        MinedOreCounter[i] -= mineLevelManagerScriptPrefab.GetLevelUnlockOre(i);
        CurrentMineLevel += 1;
    }

    // Deducts wood for unlocking forest levels
    public void PayMaterialsForForestUnlock(int i)
    {
        ChoppedWoodCounter[i] -= forestLevelManagerScriptPrefab.GetLevelUnlockWood(i);
        CurrentForestLevel += 1;
    }   

    // Deducts gold for unlocking battle levels
    public void PayMaterialsForBattleUnlock(int i)
    {
        Gold -= battleLevelManagerScriptPrefab.GetLevelUnlockGold(i);
        CurrentBattleLevel += 1;
    }

    // Gold
    public float GetCurrentGold() { return Gold; }

    // UpgradePick Class
    public int GetMinedOreCounter(int i) { return MinedOreCounter[i]; }
    public int GetPickLevel() { return PickLevel; }
    public int GetPickUpgradeCounter() { return PickUpgradeCounter; }
    public void IncreasePickLevel() { PickLevel++; }
    public void IncreasePickUpgradeCounter() { PickUpgradeCounter++; }
    public void ResetPickUpgradeCounter() { PickUpgradeCounter = 0; }

    // UpgradeAxe Class
    public int GetChoppedWoodCounter(int i) { return ChoppedWoodCounter[i]; }
    public int GetAxeLevel() { return AxeLevel; }
    public int GetAxeUpgradeCounter() { return AxeUpgradeCounter; }
    public void IncreaseAxeLevel() { AxeLevel++; }
    public void IncreaseAxeUpgradeCounter() { AxeUpgradeCounter++; }
    public void ResetAxeUpgradeCounter() { AxeUpgradeCounter = 0; }

    // LevelManager Classes
    public int GetCurrentMineLevel() { return CurrentMineLevel; }
    public int GetCurrentForestLevel() { return CurrentForestLevel; }
    public int GetCurrentBattleLevel() { return CurrentBattleLevel; }

    public int GetDragonsKilled() { return DragonsKilled; }
    public void AddDragonsKilled() { DragonsKilled++; }

    // Increases gold owned by the amount dropped by a monster
    public void CountGold(int i)
    {
        Gold += mobStats[i].GetGoldReward();
        GoldText.text = Math.Round(Gold, 1).ToString();
    }

    // DATA saving related below:
    // Creates a file and saves variables to it
    void OnDisable()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat"); 
        PlayerData data = new PlayerData();

        for(int i=0; i<OreMinedText.Length; i++) { data.MinedOreCounterData[i] = MinedOreCounter[i]; }
        data.PickUpgradeCounterData = PickUpgradeCounter;
        data.PickLevelData = PickLevel;

        for (int i = 0; i < WoodChoppedText.Length; i++) { data.ChoppedWoodCounterData[i] = ChoppedWoodCounter[i]; }
        data.AxeUpgradeCounterData = AxeUpgradeCounter;
        data.AxeLevelData = AxeLevel;

        data.GoldData = Gold;

        data.CurrentMineLevelData = CurrentMineLevel;
        data.CurrentForestLevelData = CurrentForestLevel;
        data.CurrentBattleLevelData = CurrentBattleLevel;

        data.DragonsKilledData = DragonsKilled;

        bf.Serialize(file, data);
        file.Close();
    }

    // Loads variables from a file already created
    void OnEnable()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat")) // checks if the file already exists, if it doesn't, it is created.
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = bf.Deserialize(file) as PlayerData;
            file.Close();

            for (int i = 0; i < OreMinedText.Length; i++) { MinedOreCounter[i] = data.MinedOreCounterData[i]; }
            PickUpgradeCounter = data.PickUpgradeCounterData;
            PickLevel = data.PickLevelData;

            for (int i = 0; i < WoodChoppedText.Length; i++) { ChoppedWoodCounter[i] = data.ChoppedWoodCounterData[i]; }
            AxeUpgradeCounter = data.AxeUpgradeCounterData;
            AxeLevel = data.AxeLevelData;

            Gold = data.GoldData;

            CurrentMineLevel = data.CurrentMineLevelData;
            CurrentForestLevel = data.CurrentForestLevelData;
            CurrentBattleLevel = data.CurrentBattleLevelData;

            DragonsKilled = data.DragonsKilledData;
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

        public float GoldData;

        public int CurrentMineLevelData;
        public int CurrentForestLevelData;
        public int CurrentBattleLevelData;

        public int DragonsKilledData;
    }
}
