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

    int PickUpgradeCounter = 0; // You can upgrade an item to +9 before you can buy a new one
    int PickUpgradeClicks = 0; // Possibly to be deleted later
    int PickaxeLevel = 0;
    int UpgradePriceParameter = 0; // Parameter needed to know which function number to call

    GameObject Pickaxe;
    GameObject SmallOre;

    [SerializeField] GameObject[] PickaxeSprite;
    [SerializeField] GameObject[] SmallOreSprite;

    [SerializeField] private GameSession gameSessionScript;

    [SerializeField] Button UpgradeButton;

    private void Start()
    {
        // Spawning First Pickaxe
        Pickaxe = Instantiate(PickaxeSprite[PickaxeLevel], new Vector2(9, 9), Quaternion.identity) as GameObject;
    }

    public void UpgradePickaxe()
    {
        PickUpgradeCounter += 1;
        PickUpgradeClicks += 1;
        PickUpgradeLevelText.text = PickUpgradeCounter.ToString();

        // NewPickaxe 1
        if (PickUpgradeClicks % 10 == 9) // There should be x instead of 9
        {
            DisplayUpgradePriceSprite(UpgradePriceParameter);
            PickUpgradePriceText[0].text = 1.ToString();
            PickUpgradePriceText[1].text = 1.ToString();
            PickUpgradePriceText[2].text = 1.ToString();
            PickUpgradePriceText[3].text = 1.ToString();            
            if (gameSessionScript.GetMinedOreCounter(UpgradePriceParameter) < 1) // TODO 1 Should be Replaced with some ugprade cost
            {
                InsufficientMaterialsText.text = "Insufficient Materials"; // This has to be somewhere else
                UpgradeButton.interactable = false;
            }
            else
            {
                InsufficientMaterialsText.text = null;               
            } 
        }

        if (PickUpgradeClicks > 1 && PickUpgradeClicks % 10 == 0)
        {
            DestroyAndInstantiatePickaxe();
            FindObjectOfType<GameSession>().BuyPickaxe(UpgradePriceParameter); // Deducts the cost of a pickaxe
            UpgradePriceParameter++;
        }
    }

    public void DestroyAndInstantiatePickaxe()
    {
        Destroy(Pickaxe);
        Destroy(SmallOre);

        // PickaxeLevel is required for the game to know which pickaxe you currently have.
        PickaxeLevel += 1;
        Pickaxe = Instantiate(PickaxeSprite[PickaxeLevel], new Vector2(9, 9), Quaternion.identity) as GameObject;

        PickUpgradeCounter = 0;
        PickUpgradeLevelText.text = PickUpgradeCounter.ToString();

        // Removes upgrade price
        PickUpgradePriceText[0].text = null;
        PickUpgradePriceText[1].text = null;
        PickUpgradePriceText[2].text = null;
        PickUpgradePriceText[3].text = null;
    }

    public void DisplayUpgradePriceSprite(int i)
    {
        SmallOre = Instantiate(SmallOreSprite[i], new Vector2(12, 12), Quaternion.identity) as GameObject;
        SmallOre = Instantiate(SmallOreSprite[i], new Vector2(13, 12), Quaternion.identity) as GameObject;
        SmallOre = Instantiate(SmallOreSprite[i], new Vector2(14, 12), Quaternion.identity) as GameObject;
        SmallOre = Instantiate(SmallOreSprite[i], new Vector2(15, 12), Quaternion.identity) as GameObject;
    }
}
