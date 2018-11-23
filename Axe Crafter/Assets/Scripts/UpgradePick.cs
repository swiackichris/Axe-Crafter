using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using UnityEngine.UI;

public class UpgradePick : MonoBehaviour {

    [SerializeField] TextMeshProUGUI PickUpgradeLevelText;
    [SerializeField] TextMeshProUGUI PickUpgradePriceText1;
    [SerializeField] TextMeshProUGUI PickUpgradePriceText2;
    [SerializeField] TextMeshProUGUI PickUpgradePriceText3;
    [SerializeField] TextMeshProUGUI PickUpgradePriceText4;

    [SerializeField] GameObject UpgradePickaxeButton;

    int PickUpgradeCounter = 0;
    int PickUpgradeClicks = 0;
    int PickaxeLevel = 0;

    GameObject Pickaxe;
    GameObject SmallOre;

    [SerializeField] GameObject[] PickaxeSprite;
    [SerializeField] GameObject[] SmallOreSprite;

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
        if(PickUpgradeClicks == 9)
        {
            if (FindObjectOfType<GameSession>().GetMinedOreCounter1() < 1)
            {
                // UpgradePickaxeButton.GetComponent<Button>();
                // UpgradePickaxeButton.button.interactable = false;
            }
            DisplayUpgradePriceSprite1();
            PickUpgradePriceText1.text = 1.ToString();
            PickUpgradePriceText2.text = 1.ToString();
            PickUpgradePriceText3.text = 1.ToString();
            PickUpgradePriceText4.text = 1.ToString();
        }

        if(PickUpgradeClicks == 10)
        {
            DestroyAndInstantiatePickaxe();
            FindObjectOfType<GameSession>().BuyPickaxe1(); // Deducts the cost of a pickaxe
        }

        // NewPickaxe 2
        if (PickUpgradeClicks == 19)
        {
            DisplayUpgradePriceSprite2();
            PickUpgradePriceText1.text = 2.ToString();
            PickUpgradePriceText2.text = 2.ToString();
            PickUpgradePriceText3.text = 2.ToString();
            PickUpgradePriceText4.text = 2.ToString();
        }

        if (PickUpgradeClicks == 20)
        {
            DestroyAndInstantiatePickaxe();
            FindObjectOfType<GameSession>().BuyPickaxe2(); // Deducts the cost of a pickaxe
        }

        // NewPickaxe 3
        if (PickUpgradeClicks == 29)
        {
            DisplayUpgradePriceSprite3();
            PickUpgradePriceText1.text = 3.ToString();
            PickUpgradePriceText2.text = 3.ToString();
            PickUpgradePriceText3.text = 3.ToString();
            PickUpgradePriceText4.text = 3.ToString();
        }

        if (PickUpgradeClicks == 30)
        {
            DestroyAndInstantiatePickaxe();
            FindObjectOfType<GameSession>().BuyPickaxe3(); // Deducts the cost of a pickaxe
        }

        // NewPickaxe 4
        if (PickUpgradeClicks == 39)
        {
            DisplayUpgradePriceSprite4();
            PickUpgradePriceText1.text = 4.ToString();
            PickUpgradePriceText2.text = 4.ToString();
            PickUpgradePriceText3.text = 4.ToString();
            PickUpgradePriceText4.text = 4.ToString();
        }

        if (PickUpgradeClicks == 40)
        {
            DestroyAndInstantiatePickaxe();
            FindObjectOfType<GameSession>().BuyPickaxe4(); // Deducts the cost of a pickaxe
        }

        // NewPickaxe 5
        if (PickUpgradeClicks == 49)
        {
            DisplayUpgradePriceSprite5();
            PickUpgradePriceText1.text = 5.ToString();
            PickUpgradePriceText2.text = 5.ToString();
            PickUpgradePriceText3.text = 5.ToString();
            PickUpgradePriceText4.text = 5.ToString();
        }

        if (PickUpgradeClicks == 50)
        {
            DestroyAndInstantiatePickaxe();
            FindObjectOfType<GameSession>().BuyPickaxe5(); // Deducts the cost of a pickaxe
        }

        // NewPickaxe 6
        if (PickUpgradeClicks == 59)
        {
            DisplayUpgradePriceSprite6();
            PickUpgradePriceText1.text = 6.ToString();
            PickUpgradePriceText2.text = 6.ToString();
            PickUpgradePriceText3.text = 6.ToString();
            PickUpgradePriceText4.text = 6.ToString();
        }

        if (PickUpgradeClicks == 60)
        {
            DestroyAndInstantiatePickaxe();
            FindObjectOfType<GameSession>().BuyPickaxe6(); // Deducts the cost of a pickaxe
        }

        // NewPickaxe 7
        if (PickUpgradeClicks == 69)
        {
            DisplayUpgradePriceSprite7();
            PickUpgradePriceText1.text = 7.ToString();
            PickUpgradePriceText2.text = 7.ToString();
            PickUpgradePriceText3.text = 7.ToString();
            PickUpgradePriceText4.text = 7.ToString();
        }

        if (PickUpgradeClicks == 70)
        {
            DestroyAndInstantiatePickaxe();
            FindObjectOfType<GameSession>().BuyPickaxe7(); // Deducts the cost of a pickaxe
        }

        // NewPickaxe 8
        if (PickUpgradeClicks == 79)
        {
            DisplayUpgradePriceSprite8();
            PickUpgradePriceText1.text = 8.ToString();
            PickUpgradePriceText2.text = 8.ToString();
            PickUpgradePriceText3.text = 8.ToString();
            PickUpgradePriceText4.text = 8.ToString();
        }

        if (PickUpgradeClicks == 80)
        {
            DestroyAndInstantiatePickaxe();
            FindObjectOfType<GameSession>().BuyPickaxe8(); // Deducts the cost of a pickaxe
        }

        // NewPickaxe 9
        if (PickUpgradeClicks == 89)
        {
            DisplayUpgradePriceSprite9();
            PickUpgradePriceText1.text = 9.ToString();
            PickUpgradePriceText2.text = 9.ToString();
            PickUpgradePriceText3.text = 9.ToString();
            PickUpgradePriceText4.text = 9.ToString();
        }

        if (PickUpgradeClicks == 90)
        {
            DestroyAndInstantiatePickaxe();
            FindObjectOfType<GameSession>().BuyPickaxe9(); // Deducts the cost of a pickaxe
        }

        // NewPickaxe 10
        if (PickUpgradeClicks == 99)
        {
            DisplayUpgradePriceSprite10();
            PickUpgradePriceText1.text = 10.ToString();
            PickUpgradePriceText2.text = 10.ToString();
            PickUpgradePriceText3.text = 10.ToString();
            PickUpgradePriceText4.text = 10.ToString();
        }

        if (PickUpgradeClicks == 100)
        {
            DestroyAndInstantiatePickaxe();
            FindObjectOfType<GameSession>().BuyPickaxe10(); // Deducts the cost of a pickaxe
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
        PickUpgradePriceText1.text = null;
        PickUpgradePriceText2.text = null;
        PickUpgradePriceText3.text = null;
        PickUpgradePriceText4.text = null;
    }

    public void DisplayUpgradePriceSprite1()
    {
        SmallOre = Instantiate(SmallOreSprite[0], new Vector2(14, 12), Quaternion.identity) as GameObject;
    }

    public void DisplayUpgradePriceSprite2()
    {
        SmallOre = Instantiate(SmallOreSprite[1], new Vector2(14, 12), Quaternion.identity) as GameObject;
    }

    public void DisplayUpgradePriceSprite3()
    {
        SmallOre = Instantiate(SmallOreSprite[2], new Vector2(14, 12), Quaternion.identity) as GameObject;
    }

    public void DisplayUpgradePriceSprite4()
    {
        SmallOre = Instantiate(SmallOreSprite[3], new Vector2(14, 12), Quaternion.identity) as GameObject;
    }

    public void DisplayUpgradePriceSprite5()
    {
        SmallOre = Instantiate(SmallOreSprite[4], new Vector2(14, 12), Quaternion.identity) as GameObject;
    }

    public void DisplayUpgradePriceSprite6()
    {
        SmallOre = Instantiate(SmallOreSprite[5], new Vector2(14, 12), Quaternion.identity) as GameObject;
    }

    public void DisplayUpgradePriceSprite7()
    {
        SmallOre = Instantiate(SmallOreSprite[6], new Vector2(14, 12), Quaternion.identity) as GameObject;
    }

    public void DisplayUpgradePriceSprite8()
    {
        SmallOre = Instantiate(SmallOreSprite[7], new Vector2(14, 12), Quaternion.identity) as GameObject;
    }

    public void DisplayUpgradePriceSprite9()
    {
        SmallOre = Instantiate(SmallOreSprite[8], new Vector2(14, 12), Quaternion.identity) as GameObject;
    }

    public void DisplayUpgradePriceSprite10()
    {
        SmallOre = Instantiate(SmallOreSprite[9], new Vector2(14, 12), Quaternion.identity) as GameObject;
    }
}
