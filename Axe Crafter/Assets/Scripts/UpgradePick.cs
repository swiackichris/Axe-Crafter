using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using UnityEngine.UI;

public class UpgradePick : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI PickUpgradeLevelText = null;                          // Displays current pick upgrade
    [SerializeField] TextMeshProUGUI[] PickUpgradePriceText = null;                        // Displays price required to ugprade pickaxe once upgrade hits +9
    [SerializeField] TextMeshProUGUI PickUpgradeGoldCostText = null;                       // Displays current upgrade gold cost
    [SerializeField] TextMeshProUGUI HardnessText = null;                                  // Displays current tool hardness

    [SerializeField] TextMeshProUGUI ToolNameText = null;                                  // Displays currently used tool name
    [SerializeField] String[] ToolName = null;                                             // Array including names of tools

    [SerializeField] TextMeshProUGUI InsufficientMaterialsText = null;                     // Displays "Insufficient Materials"

    GameObject Pickaxe;                                                             // Required to destroy Pickaxe sprites

    [SerializeField] GameObject[] PickaxeSprite = null;                                    // Array of Pickaxe prefabs to instatiate

    [SerializeField] private GameSession gameSessionScriptPrefab = null;                   // GameSession prefab required for it's attributes
    [SerializeField] private PickaxePrices[] pickaxePricesScriptPrefab = null;             // Pickaxe prefabs required to get their attributes
    [SerializeField] private PickaxeStats[] pickaxeStatsScriptPrefab = null;               // Pickaxe prefabs required to get their attributes

    [SerializeField] Button UpgradeButton = null;                                          // UpgradeButton prefab

    [SerializeField] Image OrePriceImage = null;                                           // Image required to display ore price
    [SerializeField] Sprite[] OreSprites = null;                                           // Sprites to be displayed in price image

    [SerializeField] Image WoodPriceImage = null;                                          // Image required to display wood price
    [SerializeField] Sprite[] WoodSprites = null;                                          // Sprites to be displayed in price image

    [SerializeField] AudioClip CraftingSound = null;                                       // Crafting sound
    [SerializeField] [Range(0, 1)] float CraftingSoundVolume = 1f;                  // Crafting sound volume

    int lastResourcePos = 0;
    readonly int maxPickLevel = 10;

    private readonly int CraftToolMultiplier = 5;
    private readonly float UpgradeToolMultiplier = 1.05f;

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

    // Shows visual representation materials required to upgrade
    public void DisplayUpgradePriceSprite()
    {
        for (int i = 0; i < maxPickLevel; i++)
        {
            // If resource price of a tool is bigger than 1, displays visual sprite of materials required.
            if (gameSessionScriptPrefab.GetPickLevel() < maxPickLevel && pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel() + 1].GetWoodRequired(i) > 0)
            {
                WoodPriceImage.overrideSprite = WoodSprites[i];
            }

            // If resource price of a tool is bigger than 1, displays visual sprite of materials required.
            if (gameSessionScriptPrefab.GetPickLevel() < maxPickLevel && pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel() + 1].GetOreRequired(i) > 0)
            {
                OrePriceImage.overrideSprite = OreSprites[i];
            }
        }
    }

    // Shows Material Cost required to Upgrade tool to a new sprite
    public void MaterialCost() // Edit Function name to be clearer
    {
        // Show Upgrade Materials
        if (gameSessionScriptPrefab.GetPickUpgradeCounter() == 9)
        {
            DisplayUpgradePriceSprite();

            // For Wood Prices
            lastResourcePos = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int j = lastResourcePos; j < maxPickLevel; j++)
                {
                    if (gameSessionScriptPrefab.GetPickLevel() < maxPickLevel && pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel() + 1].GetWoodRequired(j) > 0)
                    {
                        PickUpgradePriceText[i].text = pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel() + 1].GetWoodRequired(j).ToString();
                        lastResourcePos = j + 1;
                        break;
                    }
                }
            }

            // For Ore Prices
            lastResourcePos = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int j = lastResourcePos; j < maxPickLevel; j++)
                {
                    if (gameSessionScriptPrefab.GetPickLevel() < maxPickLevel && pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel() + 1].GetOreRequired(j) > 0)
                    {
                        PickUpgradePriceText[i + 2].text = pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel() + 1].GetOreRequired(j).ToString();
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
        for (int i = 0; i < maxPickLevel; i++)
        {
            // Checks if we have enough materials to perform an upgrade
            if(gameSessionScriptPrefab.GetPickLevel() < maxPickLevel)
            {
                if (gameSessionScriptPrefab.GetMinedOreCounter(i) < pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel() + 1].GetOreRequired(i)
                    || gameSessionScriptPrefab.GetChoppedWoodCounter(i) < pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel() + 1].GetWoodRequired(i))
                {
                    DisableButtonDisplayText();
                }
            }
        }
    }

    private void UpgradePickaxeSprite()
    {
        if (gameSessionScriptPrefab.GetPickLevel() < maxPickLevel && gameSessionScriptPrefab.GetPickUpgradeCounter() == 10)
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
        if (gameSessionScriptPrefab.GetPickLevel() < maxPickLevel && gameSessionScriptPrefab.GetPickUpgradeCounter() == 9) { PickUpgradeGoldCostText.text = (pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel()].GetGoldRequired()*CraftToolMultiplier).ToString(); }
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
