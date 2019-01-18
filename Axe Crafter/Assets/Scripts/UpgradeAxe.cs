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
    [SerializeField] TextMeshProUGUI AxeUpgradeLevelText;                          // Displays current Axe upgrade
    [SerializeField] TextMeshProUGUI[] AxeUpgradePriceText;                        // Displays price required to ugprade Axe once upgrade hits +9

    [SerializeField] TextMeshProUGUI InsufficientMaterialsText;                     // Displays "Insufficient Materials"

    GameObject Axe;                                                             // Required to destroy Axe sprites
    GameObject[] SmallWood;                                                          // Required to destroy SmallWood sprites
    GameObject[] SmallOre;

    [SerializeField] GameObject[] AxeSprite;                                    // Array of Axe prefabs to instatiate
    [SerializeField] GameObject[] SmallWoodSprite;                                   // Array of SmallWood prefabs to instatiate
    [SerializeField] GameObject[] SmallOreSprite;

    [SerializeField] private GameSession gameSessionScriptPrefab;                   // GameSession prefab required for it's attributes
    [SerializeField] private AxePrices[] AxePricesScriptPrefab;            // Axe prefabs required to get their attributes

    [SerializeField] Button UpgradeButton;

    int PARAMETER = 0; // Needed to make a for function in DisplayUpgradePriceSprite and MaterialCost functions

    private void Start()
    {
        // Spawning First Axe
        Axe = Instantiate(AxeSprite[gameSessionScriptPrefab.GetAxeLevel()], new Vector2(9, 9), Quaternion.identity) as GameObject;

        // Loading AxeUpgradeCounter from a file
        AxeUpgradeLevelText.text = gameSessionScriptPrefab.GetAxeUpgradeCounter().ToString();

        // Removes text for +0 upgrade
        RemoveAxeUpgradeCounterText();

        // Initialise SmallWood Array Size
        SmallWood = new GameObject[2];

        // Initialise SmallOre Array Size
        SmallOre = new GameObject[2];

        // Shows Upgrade Cost in Numbers
        MaterialCost();
    }

    public void UpgradeAxes()
    {
        IncreaseUpgradeCounter();
        RemoveAxeUpgradeCounterText();
        MaterialCost();
        UpgradeAxeSprite();
    }

    public void DestroyAndInstantiateAxe()
    {
        // Destroys old Axe to make place for a new one
        Destroy(Axe);
        for (int i = 0; i <= 1; i++) { Destroy(SmallWood[i]); }
        for (int i = 0; i <= 1; i++) { Destroy(SmallOre[i]); }

        // AxeLevel is required for the game to know which Axe you currently have.
        gameSessionScriptPrefab.IncreaseAxeLevel();
        Axe = Instantiate(AxeSprite[gameSessionScriptPrefab.GetAxeLevel()], new Vector2(9, 9), Quaternion.identity) as GameObject;

        gameSessionScriptPrefab.ResetAxeUpgradeCounter();
        AxeUpgradeLevelText.text = gameSessionScriptPrefab.GetAxeUpgradeCounter().ToString();

        // Removes upgrade price
        for (int i = 0; i <= 3; i++) { AxeUpgradePriceText[i].text = null; }
    }

    // Shows visual representation of types of Wood required to upgrade
    public void DisplayUpgradePriceSprite() // TODO improve this function
    {
        // For Wood Prices
        PARAMETER = 0;
        for (int j = 0; j < 2; j++)
        {
            for (int jj = PARAMETER; jj < 10; jj++) // Possibly add .Length method instead of 10 later
            {
                // If resource price of a Axe is bigger than 1, displays visual sprite of Wood required.
                if (AxePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel()].GetWoodRequired(jj) > 0)
                {
                    SmallWood[j] = Instantiate(SmallWoodSprite[jj], new Vector2(12 + j, 12), Quaternion.identity) as GameObject;
                    PARAMETER = jj + 1;
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
                // If resource price of a Axe is bigger than 1, displays visual sprite of Mine required.
                if (AxePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel()].GetOreRequired(jj) > 0)
                {
                    SmallOre[j] = Instantiate(SmallOreSprite[jj], new Vector2(12 + j + 2, 12), Quaternion.identity) as GameObject;
                    PARAMETER = jj + 1;
                    break;
                }
            }
        }
    }

    // Shows Material Cost required to Upgrade Axe to a new sprite
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
                    if (AxePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel()].GetWoodRequired(ii) > 0)
                    {
                        AxeUpgradePriceText[i].text = AxePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel()].GetWoodRequired(ii).ToString();
                        print("ii: " + ii + " = " + AxePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel()].GetWoodRequired(ii));
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
                    if (AxePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel()].GetOreRequired(ii) > 0)
                    {
                        AxeUpgradePriceText[i+2].text = AxePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel()].GetOreRequired(ii).ToString();
                        print("ii: " + ii + " = " + AxePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel()].GetOreRequired(ii));
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
            if (gameSessionScriptPrefab.GetChoppedWoodCounter(jj) < AxePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel()].GetWoodRequired(jj) 
                || gameSessionScriptPrefab.GetMinedOreCounter(jj) < AxePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel()].GetOreRequired(jj))
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

            // UpgradeAxeSprite
            DestroyAndInstantiateAxe();
        }
    }

    private void RemoveAxeUpgradeCounterText()
    {
        // If Upgrade Counter = 0, Don't show +0 Upgrade Counter Text
        if (gameSessionScriptPrefab.GetAxeUpgradeCounter() == 0) { AxeUpgradeLevelText.text = null; }
    }

    private void IncreaseUpgradeCounter()
    {
        // Checks if there is enough Gold for an upgrade
        if (gameSessionScriptPrefab.GetCurrentGold() < AxePricesScriptPrefab[gameSessionScriptPrefab.GetAxeLevel()].GetGoldRequired())
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
}
