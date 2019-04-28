﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using UnityEngine.UI;

public class UpgradePick : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI PickUpgradeLevelText;                          // Displays current pick upgrade
    [SerializeField] TextMeshProUGUI[] PickUpgradePriceText;                        // Displays price required to ugprade pickaxe once upgrade hits +9
    [SerializeField] TextMeshProUGUI PickUpgradeGoldCostText;
    [SerializeField] TextMeshProUGUI HardnessText;

    [SerializeField] TextMeshProUGUI ToolNameText;
    [SerializeField] String[] ToolName;

    [SerializeField] TextMeshProUGUI InsufficientMaterialsText;                     // Displays "Insufficient Materials"

    GameObject Pickaxe;                                                             // Required to destroy Pickaxe sprites

    [SerializeField] GameObject[] PickaxeSprite;                                    // Array of Pickaxe prefabs to instatiate

    [SerializeField] private GameSession gameSessionScriptPrefab;                   // GameSession prefab required for it's attributes
    [SerializeField] private PickaxePrices[] pickaxePricesScriptPrefab;             // Pickaxe prefabs required to get their attributes
    [SerializeField] private PickaxeStats[] pickaxeStatsScriptPrefab;               // Pickaxe prefabs required to get their attributes

    [SerializeField] Button UpgradeButton;

    [SerializeField] Image OrePriceImage;
    [SerializeField] Sprite[] OreSprites;

    [SerializeField] Image WoodPriceImage;
    [SerializeField] Sprite[] WoodSprites;

    [SerializeField] AudioClip CraftingSound;
    [SerializeField] [Range(0, 1)] float CraftingSoundVolume = 1f;

    private int CraftToolMultiplier = 5;
    private float UpgradeToolMultiplier = 1.05f;

    int PARAMETER = 0; // Needed to make a for function in DisplayUpgradePriceSprite and MaterialCost functions

    private void Start()
    {
        // Spawning First Tool
        Pickaxe = Instantiate(PickaxeSprite[gameSessionScriptPrefab.GetPickLevel()], new Vector2(4, 16), Quaternion.identity) as GameObject;

        // Loading ToolUpgradeCounter from a file
        PickUpgradeLevelText.text = gameSessionScriptPrefab.GetPickUpgradeCounter().ToString();

        DisplayUpgradeGoldCost();

        UpdatePickaxeTexts();

        // Removes text for +0 upgrade
        RemovePickUpgradeCounterText();

        // Shows Upgrade Cost in Numbers
        MaterialCost();

        // Checks if there is enough Gold for an Upgrade
        InsufficientGoldCheck();
    }

    // Upgrade Button uses this function
    public void UpgradeTool()
    {
        PlayUpgradeSound();
        IncreaseUpgradeCounter();
        RemovePickUpgradeCounterText();
        MaterialCost();
        UpgradePickaxeSprite();
        UpdatePickaxeTexts();
        DisplayUpgradeGoldCost();
    }

    public void UpdatePickaxeTexts()
    {
        ToolNameText.text = ToolName[gameSessionScriptPrefab.GetPickLevel()];
        HardnessText.text = Math.Round(pickaxeStatsScriptPrefab[gameSessionScriptPrefab.GetPickLevel()].GetPickaxeDamage() * (float)Math.Pow(UpgradeToolMultiplier, gameSessionScriptPrefab.GetPickUpgradeCounter()), 1).ToString();
    }

    public void DestroyAndInstantiatePickaxe()
    {
        // Destroys old tool to make place for a new one
        Destroy(Pickaxe);

        // ToolLevel is required for the game to know which tool you currently have.
        gameSessionScriptPrefab.IncreasePickLevel();
        Pickaxe = Instantiate(PickaxeSprite[gameSessionScriptPrefab.GetPickLevel()], new Vector2(4, 16), Quaternion.identity) as GameObject;

        gameSessionScriptPrefab.ResetPickUpgradeCounter();
        PickUpgradeLevelText.text = gameSessionScriptPrefab.GetPickUpgradeCounter().ToString();

        // Removes upgrade price
        for (int i = 0; i <= 3; i++) { PickUpgradePriceText[i].text = "0"; }
    }

    // Shows visual representation of types of ores required to upgrade
    public void DisplayUpgradePriceSprite() // TODO improve this function
    {
        // For Wood Prices
        PARAMETER = 0;
        for (int j = 0; j < 1; j++)
        {
            for (int jj = PARAMETER; jj < 10; jj++) // Possibly add .Length method instead of 10 later
            {
                // If resource price of a tool is bigger than 1, displays visual sprite of ore required.
                if (pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel() + 1].GetWoodRequired(jj) > 0)
                {
                    WoodPriceImage.overrideSprite = WoodSprites[jj];
                }
            }
        }

        // For Ore Prices
        PARAMETER = 0;
        for (int j = 0; j < 1; j++)
        {
            for (int jj = PARAMETER; jj < 10; jj++) // Possibly add .Length method instead of 10 later
            {
                // If resource price of a tool is bigger than 1, displays visual sprite of ore required.
                if (pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel() + 1].GetOreRequired(jj) > 0)
                {
                    OrePriceImage.overrideSprite = OreSprites[jj];
                }
            }
        }
    }

    // Shows Material Cost required to Upgrade tool to a new sprite
    public void MaterialCost() // Edit Function name to be clearer
    {
        // Show Upgrade Materials
        if (gameSessionScriptPrefab.GetPickUpgradeCounter() == 9) // There should be x instead of 9
        {
            DisplayUpgradePriceSprite();

            // For Wood Prices
            PARAMETER = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int ii = PARAMETER; ii < 10; ii++) // Possibly add .Length method instead of 10 later
                {
                    if (pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel() + 1].GetWoodRequired(ii) > 0)
                    {
                        PickUpgradePriceText[i].text = pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel() + 1].GetWoodRequired(ii).ToString();
                        print("ii: " + ii + " = " + pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel() + 1].GetWoodRequired(ii));
                        PARAMETER = ii + 1;
                        break;
                    }
                }
            }

            // For Ore Prices
            PARAMETER = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int ii = PARAMETER; ii < 10; ii++) // Possibly add .Length method instead of 10 later
                {
                    if (pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel() + 1].GetOreRequired(ii) > 0)
                    {
                        PickUpgradePriceText[i + 2].text = pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel() + 1].GetOreRequired(ii).ToString();
                        print("ii: " + ii + " = " + pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel() + 1].GetOreRequired(ii));
                        PARAMETER = ii + 1;
                        break;
                    }
                }
            }
            InsufficientMaterials();
        }
    }

    // Checks if there are enough materials to upgrade
    private void InsufficientMaterials()
    {
        for (int jj = 0; jj < 10; jj++) // Possibly add .Length method instead of 10 later
        {
            // Checks if we have enough materials to perform an upgrade
            if (gameSessionScriptPrefab.GetMinedOreCounter(jj) < pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel() + 1].GetOreRequired(jj)
                || gameSessionScriptPrefab.GetChoppedWoodCounter(jj) < pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel() + 1].GetWoodRequired(jj))
            {
                DisableButtonDisplayText();
            }
        }
    }

    // Possibly Change Function Name
    private void UpgradePickaxeSprite()
    {
        if (gameSessionScriptPrefab.GetPickUpgradeCounter() == 10)
        {
            // Pay Pickaxe Cost
            gameSessionScriptPrefab.BuyPickaxe();

            // UpgradePickaxeSprite
            DestroyAndInstantiatePickaxe();
        }
    }

    private void RemovePickUpgradeCounterText()
    {
        // If Upgrade Counter = 0, Don't show +0 Upgrade Counter Text
        if (gameSessionScriptPrefab.GetPickUpgradeCounter() == 0) { PickUpgradeLevelText.text = null; }
    }

    // Checks if there is enough gold for an upgrade
    private void IncreaseUpgradeCounter()
    {
        if (gameSessionScriptPrefab.GetPickUpgradeCounter() == 9 && gameSessionScriptPrefab.GetCurrentGold() < pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel()].GetGoldRequired() * CraftToolMultiplier)
        { DisableButtonDisplayText(); }
        else if (gameSessionScriptPrefab.GetCurrentGold() < pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel()].GetGoldRequired() * (float)Math.Pow(UpgradeToolMultiplier, gameSessionScriptPrefab.GetPickUpgradeCounter()))
        { DisableButtonDisplayText(); }
        else { PayGoldAndUpgrade(); }
    }

    // Checks if there is enough Gold for an Upgrade
    private void InsufficientGoldCheck()
    {
        if (gameSessionScriptPrefab.GetPickUpgradeCounter() == 9 && gameSessionScriptPrefab.GetCurrentGold() < pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel()].GetGoldRequired() * CraftToolMultiplier)
        { DisableButtonDisplayText(); }
        else if (gameSessionScriptPrefab.GetCurrentGold() < pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel()].GetGoldRequired() * (float)Math.Pow(UpgradeToolMultiplier, gameSessionScriptPrefab.GetPickUpgradeCounter()))
        { DisableButtonDisplayText(); }
    }

    private void PayGoldAndUpgrade()
    {
        // Deducts gold for upgrading
        gameSessionScriptPrefab.PayGoldForPickUpgrade();

        // Increases Upgrade Counter (+X) and Displays it as a String
        gameSessionScriptPrefab.IncreasePickUpgradeCounter();
        PickUpgradeLevelText.text = gameSessionScriptPrefab.GetPickUpgradeCounter().ToString();
    }

    private void PlayUpgradeSound()
    {
        AudioSource.PlayClipAtPoint(CraftingSound, Camera.main.transform.position, CraftingSoundVolume);
    }

    // Initialises upgrade gold cost
    private void DisplayUpgradeGoldCost()
    {
        if (gameSessionScriptPrefab.GetPickUpgradeCounter() == 9) { PickUpgradeGoldCostText.text = (pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel()].GetGoldRequired()*CraftToolMultiplier).ToString(); }
        else { PickUpgradeGoldCostText.text = Math.Round(pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel()].GetGoldRequired() * (float)Math.Pow(UpgradeToolMultiplier, gameSessionScriptPrefab.GetPickUpgradeCounter()), 0).ToString();  }
    }

    private void DisableButtonDisplayText()
    {
        // Inssufficient Materials Text
        InsufficientMaterialsText.text = "Insufficient Materials";

        // Disables button
        UpgradeButton.interactable = false;
    }
}
