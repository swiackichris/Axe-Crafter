using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using UnityEngine.UI;

public class UpgradeAxe : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI AxeUpgradeLevelText = null;                           // Displays current axe upgrade
    [SerializeField] TextMeshProUGUI[] AxeUpgradePriceText = null;                         // Displays price required to ugprade axe once upgrade hits +9
    [SerializeField] TextMeshProUGUI AxeUpgradeGoldCostText = null;                        // Displays current upgrade gold cost
    [SerializeField] TextMeshProUGUI HardnessText = null;                                  // Displays current tool hardness

    [SerializeField] TextMeshProUGUI ToolNameText = null;                                  // Displays currently used tool name
    [SerializeField] String[] ToolName = null;                                             // Array including names of tools

    [SerializeField] TextMeshProUGUI InsufficientMaterialsText = null;                     // Displays "Insufficient Materials"

    GameObject Axe;                                                                 // Required to destroy axe sprites

    [SerializeField] GameObject[] AxeSprite = null;                                        // Array of axe prefabs to instatiate

    [SerializeField] private GameSession gameSessionScriptPrefab = null;                   // GameSession prefab required for it's attributes
    [SerializeField] private AxePrices[] axePricesScriptPrefab = null;                     // Axe prefabs required to get their attributes
    [SerializeField] private AxeStats[] axeStatsScriptPrefab = null;                       // Axe prefabs required to get their attributes

    [SerializeField] Button UpgradeButton = null;                                          // UpgradeButton prefab

    [SerializeField] Image OrePriceImage = null;                                           // Image required to display ore price
    [SerializeField] Sprite[] OreSprites = null;                                           // Sprites to be displayed in price image

    [SerializeField] Image WoodPriceImage = null;                                          // Image required to display wood price
    [SerializeField] Sprite[] WoodSprites = null;                                          // Sprites to be displayed in price image

    [SerializeField] AudioClip CraftingSound = null;                                       // Crafting sound
    [SerializeField] [Range(0, 1)] float CraftingSoundVolume = 1f;                  // Crafting sound volume

    int lastResourcePos = 0;
    readonly int maxAxeLevel = 9;

    private readonly int CraftToolMultiplier = 5;
    private readonly float UpgradeToolMultiplier = 1.05f;

    private void Start()
    {
        // Spawning First Tool
        Axe = Instantiate(AxeSprite[gameSessionScriptPrefab.GetAxeLevel()], new Vector2(4, 16), Quaternion.identity) as GameObject;

        // Loading ToolUpgradeCounter from a file
        AxeUpgradeLevelText.text = gameSessionScriptPrefab.GetAxeUpgradeCounter().ToString();

        DisplayUpgradeGoldCost();

        UpdateAxeTexts();

        // Removes text for +0 upgrade
        RemoveAxeUpgradeCounterText();

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
        RemoveAxeUpgradeCounterText();
        MaterialCost();
        UpgradeAxeSprite();
        UpdateAxeTexts();
        DisplayUpgradeGoldCost();
    }

    public void UpdateAxeTexts()
    {
        ToolNameText.text = ToolName[gameSessionScriptPrefab.GetAxeLevel()];
        HardnessText.text = Math.Round(axeStatsScriptPrefab[gameSessionScriptPrefab.GetAxeLevel()].GetAxeDamage() * (float)Math.Pow(UpgradeToolMultiplier, gameSessionScriptPrefab.GetAxeUpgradeCounter()), 1).ToString();
    }

    public void DestroyAndInstantiateAxe()
    {
        // Destroys old tool to make place for a new one
        Destroy(Axe);

        // ToolLevel is required for the game to know which tool you currently have.
        gameSessionScriptPrefab.IncreaseAxeLevel();
        Axe = Instantiate(AxeSprite[gameSessionScriptPrefab.GetAxeLevel()], new Vector2(4, 16), Quaternion.identity) as GameObject;

        gameSessionScriptPrefab.ResetAxeUpgradeCounter();
        AxeUpgradeLevelText.text = gameSessionScriptPrefab.GetAxeUpgradeCounter().ToString();

        // Removes upgrade price
        for (int i = 0; i <= 3; i++) { AxeUpgradePriceText[i].text = "0"; }
    }

    // Shows visual representation of materials required to upgrade
    public void DisplayUpgradePriceSprite()
    {
        // For Wood Prices
        for (int i = 0; i < maxAxeLevel; i++)
        {
            // If resource price of a tool is bigger than 1, displays visual sprite of materials required.
            if (gameSessionScriptPrefab.GetAxeLevel() < maxAxeLevel && axePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel() + 1].GetWoodRequired(i) > 0)
            {
                WoodPriceImage.overrideSprite = WoodSprites[i];
            }

            // If resource price of a tool is bigger than 1, displays visual sprite of materials required.
            if (gameSessionScriptPrefab.GetAxeLevel() < maxAxeLevel && axePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel() + 1].GetOreRequired(i) > 0)
            {
                OrePriceImage.overrideSprite = OreSprites[i];
            }
        }
    }

    // Shows Material Cost required to Upgrade tool to a new sprite
    public void MaterialCost()
    {
        // Show Upgrade Materials
        if (gameSessionScriptPrefab.GetAxeUpgradeCounter() == 9)
        {
            DisplayUpgradePriceSprite();

            // For Wood Prices
            lastResourcePos = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int j = lastResourcePos; j < maxAxeLevel; j++)
                {
                    if (gameSessionScriptPrefab.GetAxeLevel() < maxAxeLevel && axePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel() + 1].GetWoodRequired(j) > 0)
                    {
                        AxeUpgradePriceText[i].text = axePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel() + 1].GetWoodRequired(j).ToString();
                        lastResourcePos = j + 1;
                        break;
                    }
                }
            }

            // For Ore Prices
            lastResourcePos = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int j = lastResourcePos; j < maxAxeLevel; j++)
                {
                    if (gameSessionScriptPrefab.GetAxeLevel() < maxAxeLevel && axePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel() + 1].GetOreRequired(j) > 0)
                    {
                        AxeUpgradePriceText[i + 2].text = axePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel() + 1].GetOreRequired(j).ToString();
                        lastResourcePos = j + 1;
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
        for (int i = 0; i < maxAxeLevel; i++)
        {
            // Checks if we have enough materials to perform an upgrade
            if (gameSessionScriptPrefab.GetAxeLevel() < maxAxeLevel)
            {
                if (gameSessionScriptPrefab.GetMinedOreCounter(i) < axePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel() + 1].GetOreRequired(i)
                    || gameSessionScriptPrefab.GetChoppedWoodCounter(i) < axePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel() + 1].GetWoodRequired(i))
                {
                    DisableButtonDisplayText();
                }
            }
        }
    }

    private void UpgradeAxeSprite()
    {
        if (gameSessionScriptPrefab.GetAxeLevel() < maxAxeLevel && gameSessionScriptPrefab.GetAxeUpgradeCounter() == 10)
        {
            // Pay Axe Cost
            gameSessionScriptPrefab.BuyAxe();

            // UpgradeAxeSprite
            DestroyAndInstantiateAxe();
        }
    }

    private void RemoveAxeUpgradeCounterText()
    {
        // If Upgrade Counter = 0, Don't show +0 Upgrade Counter Text
        if (gameSessionScriptPrefab.GetAxeUpgradeCounter() == 0) { AxeUpgradeLevelText.text = null; }
    }

    // Checks if there is enough gold for an upgrade
    private void IncreaseUpgradeCounter()
    {
        if (gameSessionScriptPrefab.GetAxeUpgradeCounter() == 9 && gameSessionScriptPrefab.GetCurrentGold() < axePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel()].GetGoldRequired() * CraftToolMultiplier)
        { DisableButtonDisplayText(); }
        else if (gameSessionScriptPrefab.GetCurrentGold() < axePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel()].GetGoldRequired() * (float)Math.Pow(UpgradeToolMultiplier, gameSessionScriptPrefab.GetAxeUpgradeCounter()))
        { DisableButtonDisplayText(); }
        else { PayGoldAndUpgrade(); }
    }

    // Checks if there is enough Gold for an Upgrade
    private void InsufficientGoldCheck()
    {
        if (gameSessionScriptPrefab.GetAxeUpgradeCounter() == 9 && gameSessionScriptPrefab.GetCurrentGold() < axePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel()].GetGoldRequired() * CraftToolMultiplier)
        { DisableButtonDisplayText(); }
        else if (gameSessionScriptPrefab.GetCurrentGold() < axePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel()].GetGoldRequired() * (float)Math.Pow(UpgradeToolMultiplier, gameSessionScriptPrefab.GetAxeUpgradeCounter()))
        { DisableButtonDisplayText(); }
    }

    private void PayGoldAndUpgrade()
    {
        // Deducts gold for upgrading
        gameSessionScriptPrefab.PayGoldForAxeUpgrade();

        // Increases Upgrade Counter (+X) and Displays it as a String
        gameSessionScriptPrefab.IncreaseAxeUpgradeCounter();
        AxeUpgradeLevelText.text = gameSessionScriptPrefab.GetAxeUpgradeCounter().ToString();
    }

    private void PlayUpgradeSound()
    {
        AudioSource.PlayClipAtPoint(CraftingSound, Camera.main.transform.position, CraftingSoundVolume);
    }

    // Initialises upgrade gold cost
    private void DisplayUpgradeGoldCost()
    {
        if (gameSessionScriptPrefab.GetAxeLevel() < maxAxeLevel && gameSessionScriptPrefab.GetAxeUpgradeCounter() == 9) { AxeUpgradeGoldCostText.text = (axePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel()].GetGoldRequired() * CraftToolMultiplier).ToString(); }
        else { AxeUpgradeGoldCostText.text = Math.Round(axePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel()].GetGoldRequired() * (float)Math.Pow(UpgradeToolMultiplier, gameSessionScriptPrefab.GetAxeUpgradeCounter()), 0).ToString(); }
    }

    private void DisableButtonDisplayText()
    {
        // Inssufficient Materials Text
        InsufficientMaterialsText.text = "Insufficient Materials";

        // Disables button
        UpgradeButton.interactable = false;
    }
}