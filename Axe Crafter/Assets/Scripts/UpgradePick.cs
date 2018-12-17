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
    [SerializeField] TextMeshProUGUI PickUpgradeLevelText;
    [SerializeField] TextMeshProUGUI[] PickUpgradePriceText;

    [SerializeField] TextMeshProUGUI InsufficientMaterialsText;

    [SerializeField] GameObject UpgradePickaxeButton;

    GameObject Pickaxe;
    GameObject[] SmallOre;

    [SerializeField] GameObject[] PickaxeSprite;
    [SerializeField] GameObject[] SmallOreSprite;

    [SerializeField] private GameSession gameSessionScriptPrefab;
    // [SerializeField] private PickaxePrices pickaxePricesScriptPrefab; // Possibly Delete, We need either an Array of Function or Array of Scripts?

    [SerializeField] Button UpgradeButton;

    int P = 0; // Needed to make a for function with PickUpgradePriceText

    private void Start()
    {
        // Spawning First Pickaxe
        Pickaxe = Instantiate(PickaxeSprite[gameSessionScriptPrefab.GetPickLevel()], new Vector2(9, 9), Quaternion.identity) as GameObject;
        PickUpgradeLevelText.text = gameSessionScriptPrefab.GetPickUpgradeCounter().ToString();
        if(gameSessionScriptPrefab.GetPickUpgradeCounter() == 0) { PickUpgradeLevelText.text = null; }
        SmallOre = new GameObject[4];

        if (gameSessionScriptPrefab.GetMinedOreCounter(gameSessionScriptPrefab.GetPickLevel()) < 1) // TODO 1 Should be Replaced with some ugprade cost
        {
            InsufficientMaterialsText.text = "Insufficient Materials";
            UpgradeButton.interactable = false;
        }
    }

    public void UpgradePickaxe()
    {
        gameSessionScriptPrefab.IncreasePickUpgradeCounter();
        PickUpgradeLevelText.text = gameSessionScriptPrefab.GetPickUpgradeCounter().ToString();
        if (gameSessionScriptPrefab.GetPickUpgradeCounter() == 0) { PickUpgradeLevelText.text = null; }

        if (gameSessionScriptPrefab.GetPickUpgradeCounter() == 9) // There should be x instead of 9
        {
            DisplayUpgradePriceSprite(gameSessionScriptPrefab.GetPickLevel());
            PickUpgradePriceText[0].text = 1.ToString();
            PickUpgradePriceText[1].text = 1.ToString();
            PickUpgradePriceText[2].text = 1.ToString();
            PickUpgradePriceText[3].text = 1.ToString();
            for(int i = 0; i<4; i++)
            {
                for(int ii = P; ii<10; ii++) // Possibly add .Length method instead of 10 later
                {
                    if (FindObjectOfType<PickaxePrices>().GetOreRequired(ii) > 0)
                    {
                        PickUpgradePriceText[i].text = FindObjectOfType<PickaxePrices>().GetOreRequired(ii).ToString();
                        P = ii + 1;
                        break;
                    }
                }
            }

            // PickUpgradePriceText[0-4] = PickaxePrices[0-10].ToString(); Each different pickaxe prefab has to be included
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

        if (gameSessionScriptPrefab.GetPickUpgradeCounter() == 10)
        {
            DestroyAndInstantiatePickaxe();
            gameSessionScriptPrefab.BuyPickaxe(gameSessionScriptPrefab.GetPickLevel()); // Deducts the cost of a pickaxe
        }
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

    public void CheckA()
    {
        /*if x[1] > 0
        {
            A = x[1]
            if A > 0; break;
        }
        else if x[2] > 0
        {
            A = x[2]
        }
        */
    }

    public void CheckB()
    {
        //
    }

    public void CheckC()
    {
        //
    }

    public void CheckD()
    {
        //
    }
}
