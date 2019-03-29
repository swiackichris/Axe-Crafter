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
    [SerializeField] TextMeshProUGUI AxeUpgradeLevelText;                          // Displays current axe upgrade
    [SerializeField] TextMeshProUGUI[] AxeUpgradePriceText;                        // Displays price required to ugprade axe once upgrade hits +9
    [SerializeField] TextMeshProUGUI AxeUpgradeGoldCostText;
    [SerializeField] TextMeshProUGUI HardnessText;

    [SerializeField] TextMeshProUGUI ToolNameText;
    [SerializeField] String[] ToolName;

    [SerializeField] TextMeshProUGUI InsufficientMaterialsText;                     // Displays "Insufficient Materials"

    GameObject Axe;                                                             // Required to destroy axe sprites

    [SerializeField] GameObject[] AxeSprite;                                    // Array of axe prefabs to instatiate

    [SerializeField] private GameSession gameSessionScriptPrefab;                   // GameSession prefab required for it's attributes
    [SerializeField] private AxePrices[] axePricesScriptPrefab;             // axe prefabs required to get their attributes
    [SerializeField] private AxeStats[] axeStatsScriptPrefab;               // axe prefabs required to get their attributes

    [SerializeField] Button UpgradeButton;

    [SerializeField] Image OrePriceImage;
    [SerializeField] Sprite[] OreSprites;

    [SerializeField] Image WoodPriceImage;
    [SerializeField] Sprite[] WoodSprites;

    int PARAMETER = 0; // Needed to make a for function in DisplayUpgradePriceSprite and MaterialCost functions

    private void Start()
    {
        // Spawning First axe
        Axe = Instantiate(AxeSprite[gameSessionScriptPrefab.GetAxeLevel()], new Vector2(4, 16), Quaternion.identity) as GameObject;

        // Loading AxeUpgradeCounter from a file
        AxeUpgradeLevelText.text = gameSessionScriptPrefab.GetAxeUpgradeCounter().ToString();

        // Initialises upgrade gold cost
        AxeUpgradeGoldCostText.text = axePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel()].GetGoldRequired().ToString();

        // Initialises axe name
        ToolNameText.text = ToolName[gameSessionScriptPrefab.GetAxeLevel()];

        // Initialises Hardness text
        HardnessText.text = axeStatsScriptPrefab[gameSessionScriptPrefab.GetAxeLevel()].GetAxeDamage().ToString();

        // Removes text for +0 upgrade
        RemoveAxeUpgradeCounterText();

        // Shows Upgrade Cost in Numbers
        MaterialCost();

        // Checks if there is enough Gold for an Upgrade
        InsufficientGoldCheck();
    }

    // Upgrade Button uses this function
    public void UpgradeAxes()
    {
        IncreaseUpgradeCounter();
        RemoveAxeUpgradeCounterText();
        MaterialCost();
        UpgradeAxeSprite();
        UpdateAxeTexts();
    }

    public void UpdateAxeTexts()
    {
        ToolNameText.text = ToolName[gameSessionScriptPrefab.GetAxeLevel()];
        HardnessText.text = axeStatsScriptPrefab[gameSessionScriptPrefab.GetAxeLevel()].GetAxeDamage().ToString();
    }

    public void DestroyAndInstantiateAxe()
    {
        // Destroys old axe to make place for a new one
        Destroy(Axe);

        // axeLevel is required for the game to know which axe you currently have.
        gameSessionScriptPrefab.IncreaseAxeLevel();
        Axe = Instantiate(AxeSprite[gameSessionScriptPrefab.GetAxeLevel()], new Vector2(4, 16), Quaternion.identity) as GameObject;

        gameSessionScriptPrefab.ResetAxeUpgradeCounter();
        AxeUpgradeLevelText.text = gameSessionScriptPrefab.GetAxeUpgradeCounter().ToString();

        // Removes upgrade price
        for (int i = 0; i <= 3; i++) { AxeUpgradePriceText[i].text = null; }
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
                // If resource price of a axe is bigger than 1, displays visual sprite of ore required.
                if (axePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel()+1].GetWoodRequired(jj) > 0)
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
                // If resource price of a axe is bigger than 1, displays visual sprite of ore required.
                if (axePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel() + 1].GetOreRequired(jj) > 0)
                {
                    OrePriceImage.overrideSprite = OreSprites[jj];
                }
            }
        }
    }

    // Shows Material Cost required to Upgrade axe to a new sprite
    public void MaterialCost() // Edit Function name to be clearer
    {
        // Show Upgrade Materials
        if (gameSessionScriptPrefab.GetAxeUpgradeCounter() == 9) // There should be x instead of 9
        {
            DisplayUpgradePriceSprite();

            // For Wood Prices
            PARAMETER = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int ii = PARAMETER; ii < 10; ii++) // Possibly add .Length method instead of 10 later
                {
                    if (axePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel() + 1].GetWoodRequired(ii) > 0)
                    {
                        AxeUpgradePriceText[i].text = axePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel() + 1].GetWoodRequired(ii).ToString();
                        print("ii: " + ii + " = " + axePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel() + 1].GetWoodRequired(ii));
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
                    if (axePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel() + 1].GetOreRequired(ii) > 0)
                    {
                        AxeUpgradePriceText[i + 2].text = axePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel() + 1].GetOreRequired(ii).ToString();
                        print("ii: " + ii + " = " + axePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel() + 1].GetOreRequired(ii));
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
            if (gameSessionScriptPrefab.GetMinedOreCounter(jj) < axePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel() + 1].GetOreRequired(jj)
                || gameSessionScriptPrefab.GetChoppedWoodCounter(jj) < axePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel() + 1].GetWoodRequired(jj))
            {
                // Inssufficient Materials Text
                InsufficientMaterialsText.text = "Insufficient Materials";

                // Disables button
                UpgradeButton.interactable = false;
            }
        }
    }

    // Possibly Change Function Name
    private void UpgradeAxeSprite()
    {
        if (gameSessionScriptPrefab.GetAxeUpgradeCounter() == 10)
        {
            // Pay Axe Cost
            gameSessionScriptPrefab.BuyAxe();

            // UpgradeaxeSprite
            DestroyAndInstantiateAxe();
        }
    }

    private void RemoveAxeUpgradeCounterText()
    {
        // If Upgrade Counter = 0, Don't show +0 Upgrade Counter Text
        if (gameSessionScriptPrefab.GetAxeUpgradeCounter() == 0) { AxeUpgradeLevelText.text = "0"; }
    }

    private void IncreaseUpgradeCounter()
    {
        // Checks if there is enough Gold for an upgrade
        if (gameSessionScriptPrefab.GetCurrentGold() < axePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel()].GetGoldRequired())
        {
            // Inssufficient Materials Text
            InsufficientMaterialsText.text = "Insufficient Materials";

            // Disables button
            UpgradeButton.interactable = false;
        }
        else
        {
            // Deducts gold for upgrading
            gameSessionScriptPrefab.PayGoldForAxeUpgrade();

            // Increases Upgrade Counter (+X) and Displays it as a String
            gameSessionScriptPrefab.IncreaseAxeUpgradeCounter();
            AxeUpgradeLevelText.text = gameSessionScriptPrefab.GetAxeUpgradeCounter().ToString();
        }
    }

    private void InsufficientGoldCheck()
    {
        // Checks if there is enough Gold for an upgrade
        if (gameSessionScriptPrefab.GetCurrentGold() < axePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel()].GetGoldRequired())
        {
            // Inssufficient Materials Text
            InsufficientMaterialsText.text = "Insufficient Materials";

            // Disables button
            UpgradeButton.interactable = false;
        }
    }
}