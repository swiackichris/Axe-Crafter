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

    [SerializeField] TextMeshProUGUI InsufficientMaterialsText;                     // Displays "Insufficient Materials"

    [SerializeField] GameObject UpgradePickaxeButton;                               // Required to disable a button if we don't have enough materials to ugprade

    GameObject Pickaxe;                                                             // Required to destroy Pickaxe sprites
    GameObject[] SmallOre;                                                          // Required to destroy SmallOre sprites

    [SerializeField] GameObject[] PickaxeSprite;                                    // Array of Pickaxe prefabs to instatiate
    [SerializeField] GameObject[] SmallOreSprite;                                   // Array of SmallOre prefabs to instatiate

    [SerializeField] private GameSession gameSessionScriptPrefab;                   // GameSession prefab required for it's attributes
    [SerializeField] private PickaxePrices [] pickaxePricesScriptPrefab;            // Pickaxe prefabs required to get their attributes

    [SerializeField] Button UpgradeButton;

    int P = 0; // Needed to make a for function with PickUpgradePriceText

    private void Start()
    {
        // Spawning First Pickaxe
        Pickaxe = Instantiate(PickaxeSprite[gameSessionScriptPrefab.GetPickLevel()], new Vector2(9, 9), Quaternion.identity) as GameObject;

        // Loading PickUpgradeCounter from a file
        PickUpgradeLevelText.text = gameSessionScriptPrefab.GetPickUpgradeCounter().ToString();

        // Removes text for +0 upgrade
        RemovePickUpgradeCounterText();

        // Initialise SmallOre Array Size
        SmallOre = new GameObject[4];

        // Shows Upgrade Cost in Materials
        MaterialCost();
    }

    public void UpgradePickaxe()
    {
        IncreaseUpgradeCounter();
        RemovePickUpgradeCounterText();
        MaterialCost();
        UpgradePickaxeSprite();
    }

    public void DestroyAndInstantiatePickaxe()
    {
        Destroy(Pickaxe);
        for(int i = 0; i<=3; i++) { Destroy(SmallOre[i]); }

        // PickaxeLevel is required for the game to know which pickaxe you currently have.
        gameSessionScriptPrefab.IncreasePickLevel();
        Pickaxe = Instantiate(PickaxeSprite[gameSessionScriptPrefab.GetPickLevel()], new Vector2(9, 9), Quaternion.identity) as GameObject;

        gameSessionScriptPrefab.ResetPickUpgradeCounter();
        PickUpgradeLevelText.text = gameSessionScriptPrefab.GetPickUpgradeCounter().ToString();

        // Removes upgrade price
        for (int i = 0; i <= 3; i++) { PickUpgradePriceText[i].text = null; }
    }

    public void DisplayUpgradePriceSprite(int i) // TODO improve this function
    {
        SmallOre[0] = Instantiate(SmallOreSprite[i], new Vector2(12, 12), Quaternion.identity) as GameObject;
        SmallOre[1] = Instantiate(SmallOreSprite[i], new Vector2(13, 12), Quaternion.identity) as GameObject;
        SmallOre[2] = Instantiate(SmallOreSprite[i], new Vector2(14, 12), Quaternion.identity) as GameObject;
        SmallOre[3] = Instantiate(SmallOreSprite[i], new Vector2(15, 12), Quaternion.identity) as GameObject;
    }

    // Shows Material Cost required to Upgrade pickaxe to a new sprite
    private void MaterialCost() // Edit Function name to be clearer
    {
        // Show Upgrade Materials
        if (gameSessionScriptPrefab.GetPickUpgradeCounter() == 9) // There should be x instead of 9
        {
            DisplayUpgradePriceSprite(gameSessionScriptPrefab.GetPickLevel());
            for (int i = 0; i < 4; i++)
            {
                for (int ii = P; ii < 10; ii++) // Possibly add .Length method instead of 10 later
                {
                    if (FindObjectOfType<PickaxePrices>().GetOreRequired(ii) > 0)
                    {
                        PickUpgradePriceText[i].text = pickaxePricesScriptPrefab[gameSessionScriptPrefab.GetPickLevel()].GetOreRequired(ii).ToString();
                        P = ii + 1;
                        break;
                    }
                }
            }

            // Check if there are enough materials to Pay for An Upgrade
            if (gameSessionScriptPrefab.GetMinedOreCounter(gameSessionScriptPrefab.GetPickLevel()) < 1) // TODO 1 Should be Replaced with some ugprade cost
            {
                InsufficientMaterialsText.text = "Insufficient Materials";
                UpgradeButton.interactable = false;
            }
            else
            {
                InsufficientMaterialsText.text = null;
            }
        }
    }

    // Possibly Change Function Name
    private void UpgradePickaxeSprite()
    {
        if (gameSessionScriptPrefab.GetPickUpgradeCounter() == 10)
        {
            // UpgradePickaxeSprite
            DestroyAndInstantiatePickaxe();

            // Pay Pickaxe Cost
            gameSessionScriptPrefab.BuyPickaxe(gameSessionScriptPrefab.GetPickLevel());
        }
    }

    private void RemovePickUpgradeCounterText()
    {
        // If Upgrade Counter = 0, Don't show +0 Upgrade Counter Text
        if (gameSessionScriptPrefab.GetPickUpgradeCounter() == 0) { PickUpgradeLevelText.text = null; }
    }

    private void IncreaseUpgradeCounter()
    {
        // Increases Upgrade Counter (+X) and Displays it as a String
        gameSessionScriptPrefab.IncreasePickUpgradeCounter();
        PickUpgradeLevelText.text = gameSessionScriptPrefab.GetPickUpgradeCounter().ToString();
    }
}
