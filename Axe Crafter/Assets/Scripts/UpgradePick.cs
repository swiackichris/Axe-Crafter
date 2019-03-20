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
    [SerializeField] TextMeshProUGUI PickUpgradeLevelText;                          // Displays current pick upgrade
    [SerializeField] TextMeshProUGUI[] PickUpgradePriceText;                        // Displays price required to ugprade pickaxe once upgrade hits +9
    [SerializeField] TextMeshProUGUI PickUpgradeGoldCostText;
    [SerializeField] TextMeshProUGUI HardnessText;

    [SerializeField] TextMeshProUGUI ToolNameText;
    [SerializeField] String[] ToolName;

    [SerializeField] TextMeshProUGUI InsufficientMaterialsText;                     // Displays "Insufficient Materials"

    GameObject Pickaxe;                                                             // Required to destroy Pickaxe sprites
    GameObject[] SmallWood;
    GameObject[] SmallOre;                                                          // Required to destroy SmallOre sprites

    [SerializeField] GameObject[] PickaxeSprite;                                    // Array of Pickaxe prefabs to instatiate
    [SerializeField] GameObject[] SmallWoodSprite;
    [SerializeField] GameObject[] SmallOreSprite;                                   // Array of SmallOre prefabs to instatiate

    [SerializeField] private GameSession gameSessionScriptPrefab;                   // GameSession prefab required for it's attributes
    [SerializeField] private PickaxePrices[] pickaxePricesScriptPrefab;             // Pickaxe prefabs required to get their attributes
    [SerializeField] private PickaxeStats[] pickaxeStatsScriptPrefab;              // Pickaxe prefabs required to get their attributes

    [SerializeField] Button UpgradeButton;

    int PARAMETER = 0; // Needed to make a for function in DisplayUpgradePriceSprite and MaterialCost functions

    private void Start()
    {
        // Spawning First Pickaxe
        Pickaxe = Instantiate(PickaxeSprite[gameSessionScriptPrefab.GetPickLevel()], new Vector2(4, 22), Quaternion.identity) as GameObject;

        // Loading PickUpgradeCounter from a file
        PickUpgradeLevelText.text = gameSessionScriptPrefab.GetPickUpgradeCounter().ToString();

        // Initialises upgrade gold cost
        PickUpgradeGoldCostText.text = pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel()].GetGoldRequired().ToString();

        // Initialises Pickaxe name
        ToolNameText.text = ToolName[gameSessionScriptPrefab.GetPickLevel()];

        // Initialises Hardness text
        HardnessText.text = pickaxeStatsScriptPrefab[gameSessionScriptPrefab.GetPickLevel()].GetPickaxeDamage().ToString();

        // Removes text for +0 upgrade
        RemovePickUpgradeCounterText();

        // Initialise SmallWood Array Size
        SmallWood = new GameObject[2];

        // Initialise SmallOre Array Size
        SmallOre = new GameObject[2];

        // Shows Upgrade Cost in Numbers
        MaterialCost();
    }

    public void UpgradePickaxe()
    {
        IncreaseUpgradeCounter();
        RemovePickUpgradeCounterText();
        MaterialCost();
        UpgradePickaxeSprite();
        UpdatePickaxeTexts();
    }

    public void UpdatePickaxeTexts()
    {
        ToolNameText.text = ToolName[gameSessionScriptPrefab.GetPickLevel()];
        HardnessText.text = pickaxeStatsScriptPrefab[gameSessionScriptPrefab.GetPickLevel()].GetPickaxeDamage().ToString();
    }

    public void DestroyAndInstantiatePickaxe()
    {
        // Destroys old pickaxe to make place for a new one
        Destroy(Pickaxe);
        for (int i = 0; i <= 1; i++) { Destroy(SmallWood[i]); }
        for (int i = 0; i <= 1; i++) { Destroy(SmallOre[i]); }

        // PickaxeLevel is required for the game to know which pickaxe you currently have.
        gameSessionScriptPrefab.IncreasePickLevel();
        Pickaxe = Instantiate(PickaxeSprite[gameSessionScriptPrefab.GetPickLevel()], new Vector2(4, 22), Quaternion.identity) as GameObject;

        gameSessionScriptPrefab.ResetPickUpgradeCounter();
        PickUpgradeLevelText.text = gameSessionScriptPrefab.GetPickUpgradeCounter().ToString();

        // Removes upgrade price
        for (int i = 0; i <= 3; i++) { PickUpgradePriceText[i].text = null; }
    }

    // Shows visual representation of types of ores required to upgrade
    public void DisplayUpgradePriceSprite() // TODO improve this function
    {
        // For Wood Prices
        PARAMETER = 0;
        for (int j = 0; j < 2; j++)
        {
            for (int jj = PARAMETER; jj < 10; jj++) // Possibly add .Length method instead of 10 later
            {
                // If resource price of a pickaxe is bigger than 1, displays visual sprite of ore required.
                if (pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel()].GetWoodRequired(jj) > 0)
                {
                    SmallWood[j] = Instantiate(SmallWoodSprite[jj], new Vector2(14-j+2, 12), Quaternion.identity) as GameObject;
                    PARAMETER = jj + 1; // If the loop has found a price > 0, it shows that as a price and parameter becomes higher so that we don't show the same sprite price twice
                    break;
                }
            }
        }

        // For Ore Prices
        PARAMETER = 0;
        for (int j = 0; j < 2; j++)
        {
            for (int jj = PARAMETER; jj < 10; jj++) // Possibly add .Length method instead of 10 later
            {
                // If resource price of a pickaxe is bigger than 1, displays visual sprite of ore required.
                if (pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel()].GetOreRequired(jj) > 0)
                {
                    SmallOre[j] = Instantiate(SmallOreSprite[jj], new Vector2(14 - j, 12), Quaternion.identity) as GameObject;
                    PARAMETER = jj + 1; // If the loop has found a price > 0, it shows that as a price and parameter becomes higher so that we don't show the same sprite price twice
                    break;
                }
            }
        }
    }

    // Shows Material Cost required to Upgrade pickaxe to a new sprite
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
                    if (pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel()].GetWoodRequired(ii) > 0)
                    {
                        PickUpgradePriceText[i].text = pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel()].GetWoodRequired(ii).ToString();
                        print("ii: " + ii + " = " + pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel()].GetWoodRequired(ii));
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
                    if (pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel()].GetOreRequired(ii) > 0)
                    {
                        PickUpgradePriceText[i+2].text = pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel()].GetOreRequired(ii).ToString();
                        print("ii: " + ii + " = " + pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel()].GetOreRequired(ii));
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
            if (gameSessionScriptPrefab.GetMinedOreCounter(jj) < pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel()].GetOreRequired(jj)
                || gameSessionScriptPrefab.GetChoppedWoodCounter(jj) < pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel()].GetWoodRequired(jj))
            {
                // Inssufficient Materials Text
                InsufficientMaterialsText.text = "Insufficient Materials";

                // Disables button
                UpgradeButton.interactable = false;
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

    private void IncreaseUpgradeCounter()
    {
        // Checks if there is enough Gold for an upgrade
        if (gameSessionScriptPrefab.GetCurrentGold() < pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel()].GetGoldRequired())
        {
            // Inssufficient Materials Text
            InsufficientMaterialsText.text = "Insufficient Materials";

            // Disables button
            UpgradeButton.interactable = false;
        }
        else
        {
            // Deducts gold for upgrading
            gameSessionScriptPrefab.PayGoldForPickUpgrade();

            // Increases Upgrade Counter (+X) and Displays it as a String
            gameSessionScriptPrefab.IncreasePickUpgradeCounter();
            PickUpgradeLevelText.text = gameSessionScriptPrefab.GetPickUpgradeCounter().ToString();
        }
    }
}
