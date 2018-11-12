using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class UpgradePick : MonoBehaviour {

    [SerializeField] TextMeshProUGUI PickUpgradeLevelText;

    [SerializeField] TextMeshProUGUI PickUpgradePriceText;
    int UpgradePrice = 1;

    [SerializeField] int PickUpgradeCounter = 0;
    int PickaxeLevel = 0;
    GameObject Pickaxe;
    GameObject SmallOre;

    [SerializeField] GameObject[] PickaxeSprite;
    [SerializeField] GameObject[] SmallOreSprite;

    private void Awake()
    {
        // Spawning First Pickaxe
        Pickaxe = Instantiate(
            PickaxeSprite[PickaxeLevel],
            new Vector2(9, 9),
            Quaternion.identity) as GameObject;
    }

    public void UpgradePickaxe()
    {
        PickUpgradeCounter += 1;
        PickUpgradeLevelText.text = PickUpgradeCounter.ToString();

        if(PickUpgradeCounter == 9)
        {
            DisplayUpgradePrice();

            PickUpgradePriceText.text = UpgradePrice.ToString();
        }

        if(PickUpgradeCounter >= 10)
        {
            Destroy(Pickaxe);
            print("Pickaxe Destroyed");
            Destroy(SmallOre);
            print("SmallOre Destroyed");

            // PickaxeLevel is required for the game to know which pickaxe you currently have.
            PickaxeLevel += 1;
            Pickaxe = Instantiate(
                PickaxeSprite[PickaxeLevel],
                new Vector2(9, 9),
                Quaternion.identity) as GameObject;
            print("Pickaxe Spawned");

            PickUpgradeCounter = 0;
            PickUpgradeLevelText.text = PickUpgradeCounter.ToString();

            PickUpgradePriceText.text = null;
        }
    }

    public void DisplayUpgradePrice()
    {
        SmallOre = Instantiate(
                SmallOreSprite[0], // number in brackets should be changed to a variable
                new Vector2(14, 12),
                Quaternion.identity) as GameObject;
    }
}
