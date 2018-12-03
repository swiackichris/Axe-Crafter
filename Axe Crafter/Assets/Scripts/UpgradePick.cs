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

    [SerializeField] private GameSession gameSessionScript;

    [SerializeField] Button UpgradeButton;

    private void Start()
    {
        // Spawning First Pickaxe
        Pickaxe = Instantiate(PickaxeSprite[gameSessionScript.GetPickLevel()], new Vector2(9, 9), Quaternion.identity) as GameObject;
        PickUpgradeLevelText.text = gameSessionScript.GetPickUpgradeCounter().ToString();
        if(gameSessionScript.GetPickUpgradeCounter() == 0) { PickUpgradeLevelText.text = null; }
        SmallOre = new GameObject[4];
    }

    public void UpgradePickaxe()
    {
        gameSessionScript.IncreasePickUpgradeCounter();
        PickUpgradeLevelText.text = gameSessionScript.GetPickUpgradeCounter().ToString();
        if (gameSessionScript.GetPickUpgradeCounter() == 0) { PickUpgradeLevelText.text = null; }

        if (gameSessionScript.GetPickUpgradeCounter() == 9) // There should be x instead of 9
        {
            DisplayUpgradePriceSprite(gameSessionScript.GetPickLevel());
            PickUpgradePriceText[0].text = 1.ToString();
            PickUpgradePriceText[1].text = 1.ToString();
            PickUpgradePriceText[2].text = 1.ToString();
            PickUpgradePriceText[3].text = 1.ToString();
            if (gameSessionScript.GetMinedOreCounter(gameSessionScript.GetPickLevel()) < 1) // TODO 1 Should be Replaced with some ugprade cost
            {
                InsufficientMaterialsText.text = "Insufficient Materials";
                UpgradeButton.interactable = false;
            }
            else
            {
                InsufficientMaterialsText.text = null;
            }
        }

        if (gameSessionScript.GetPickUpgradeCounter() == 10)
        {
            DestroyAndInstantiatePickaxe();
            gameSessionScript.BuyPickaxe(gameSessionScript.GetPickLevel()); // Deducts the cost of a pickaxe
        }
    }

    public void DestroyAndInstantiatePickaxe()
    {
        Destroy(Pickaxe);
        for(int i = 0; i<=3; i++) { Destroy(SmallOre[i]); }

        // PickaxeLevel is required for the game to know which pickaxe you currently have.
        gameSessionScript.IncreasePickLevel();
        Pickaxe = Instantiate(PickaxeSprite[gameSessionScript.GetPickLevel()], new Vector2(9, 9), Quaternion.identity) as GameObject;

        gameSessionScript.ResetPickUpgradeCounter();
        PickUpgradeLevelText.text = gameSessionScript.GetPickUpgradeCounter().ToString();

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
}
