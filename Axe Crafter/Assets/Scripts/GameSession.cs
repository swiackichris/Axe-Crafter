using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameSession : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI InsufficientMaterialsText;

    [SerializeField] TextMeshProUGUI[] OreMinedText;
    [SerializeField] int[] MinedOreCounter;

    // Use this for initialization
    void Start()
    {
        // Checks if both arrays are of equal size, it's necessary to save properly.
        if (MinedOreCounter.Length != OreMinedText.Length) { Debug.LogError("MinedOreCounter.Length should be equal to OreMinedText.Length"); }
        for(int i=0; i<OreMinedText.Length; i++)
        {
            OreMinedText[i].text = MinedOreCounter[i].ToString();
        }
    }

    // Displays and updates the amount of ore mined
    public void CountMinedOre1()
    {
        MinedOreCounter[0] += 1;
        OreMinedText[0].text = MinedOreCounter[0].ToString();
    }

    public void CountMinedOre2()
    {
        MinedOreCounter[1] += 1;
        OreMinedText[1].text = MinedOreCounter[1].ToString();
    }

    public void CountMinedOre3()
    {
        MinedOreCounter[2] += 1;
        OreMinedText[2].text = MinedOreCounter[2].ToString();
    }

    public void CountMinedOre4()
    {
        MinedOreCounter[3] += 1;
        OreMinedText[3].text = MinedOreCounter[3].ToString();
    }

    public void CountMinedOre5()
    {
        MinedOreCounter[4] += 1;
        OreMinedText[4].text = MinedOreCounter[4].ToString();
    }

    public void CountMinedOre6()
    {
        MinedOreCounter[5] += 1;
        OreMinedText[5].text = MinedOreCounter[5].ToString();
    }

    public void CountMinedOre7()
    {
        MinedOreCounter[6] += 1;
        OreMinedText[6].text = MinedOreCounter[6].ToString();
    }

    public void CountMinedOre8()
    {
        MinedOreCounter[7] += 1;
        OreMinedText[7].text = MinedOreCounter[7].ToString();
    }

    public void CountMinedOre9()
    {
        MinedOreCounter[8] += 1;
        OreMinedText[8].text = MinedOreCounter[8].ToString();
    }

    public void CountMinedOre10()
    {
        MinedOreCounter[9] += 1;
        OreMinedText[9].text = MinedOreCounter[9].ToString();
    }

    // Updates the amount of ores after purchasing items in the market
    // TODO Prices added later
    public void BuyPickaxe1()
    {
        if (MinedOreCounter[1] >= 1)
        {
            InsufficientMaterialsText.text = null;
            MinedOreCounter[1] -= 1;
            OreMinedText[1].text = MinedOreCounter[1].ToString();
        }
        else
        {
            InsufficientMaterialsText.text = "Insufficient Materials";
        }
    }

    public void BuyPickaxe2()
    {
        if (MinedOreCounter[2] >= 1)
        {
            InsufficientMaterialsText.text = null;
            MinedOreCounter[2] -= 1;
            OreMinedText[2].text = MinedOreCounter[2].ToString();
        }
        else
        {
            InsufficientMaterialsText.text = "Insufficient Materials";
        }
    }

    public void BuyPickaxe3()
    {
        if (MinedOreCounter[3] >= 1)
        {
            InsufficientMaterialsText.text = null;
            MinedOreCounter[3] -= 1;
            OreMinedText[3].text = MinedOreCounter[3].ToString();
        }
        else
        {
            InsufficientMaterialsText.text = "Insufficient Materials";
        }
    }

    public void BuyPickaxe4()
    {
        if (MinedOreCounter[4] >= 1)
        {
            InsufficientMaterialsText.text = null;
            MinedOreCounter[4] -= 1;
            OreMinedText[4].text = MinedOreCounter[4].ToString();
        }
        else
        {
            InsufficientMaterialsText.text = "Insufficient Materials";
        }
    }

    public void BuyPickaxe5()
    {
        if (MinedOreCounter[5] >= 1)
        {
            InsufficientMaterialsText.text = null;
            MinedOreCounter[5] -= 1;
            OreMinedText[5].text = MinedOreCounter[5].ToString();
        }
        else
        {
            InsufficientMaterialsText.text = "Insufficient Materials";
        }
    }

    public void BuyPickaxe6()
    {
        if (MinedOreCounter[6] >= 1)
        {
            InsufficientMaterialsText.text = null;
            MinedOreCounter[6] -= 1;
            OreMinedText[6].text = MinedOreCounter[6].ToString();
        }
        else
        {
            InsufficientMaterialsText.text = "Insufficient Materials";
        }
    }

    public void BuyPickaxe7()
    {
        if (MinedOreCounter[7] >= 1)
        {
            InsufficientMaterialsText.text = null;
            MinedOreCounter[7] -= 1;
            OreMinedText[7].text = MinedOreCounter[7].ToString();
        }
        else
        {
            InsufficientMaterialsText.text = "Insufficient Materials";
        }
    }

    public void BuyPickaxe8()
    {
        if (MinedOreCounter[8] >= 1)
        {
            InsufficientMaterialsText.text = null;
            MinedOreCounter[8]-= 1;
            OreMinedText[8].text = MinedOreCounter[8].ToString();
        }
        else
        {
            InsufficientMaterialsText.text = "Insufficient Materials";
        }
    }

    public void BuyPickaxe9()
    {
        if (MinedOreCounter[9] >= 1)
        {
            InsufficientMaterialsText.text = null;
            MinedOreCounter[9] -= 1;
            OreMinedText[9].text = MinedOreCounter[9].ToString();
        }
        else
        {
            InsufficientMaterialsText.text = "Insufficient Materials";
        }
    }

    public void BuyPickaxe10()
    {
        if (MinedOreCounter[10] >= 1)
        {
            InsufficientMaterialsText.text = null;
            MinedOreCounter[10] -= 1;
            OreMinedText[10].text = MinedOreCounter[10].ToString();
        }
        else
        {
            InsufficientMaterialsText.text = "Insufficient Materials";
        }
    }

    public int GetMinedOreCounter1() { return MinedOreCounter[0]; }
    public int GetMinedOreCounter2() { return MinedOreCounter[1]; }
    public int GetMinedOreCounter3() { return MinedOreCounter[2]; }
    public int GetMinedOreCounter4() { return MinedOreCounter[3]; }
    public int GetMinedOreCounter5() { return MinedOreCounter[4]; }
    public int GetMinedOreCounter6() { return MinedOreCounter[5]; }
    public int GetMinedOreCounter7() { return MinedOreCounter[6]; }
    public int GetMinedOreCounter8() { return MinedOreCounter[7]; }
    public int GetMinedOreCounter9() { return MinedOreCounter[8]; }
    public int GetMinedOreCounter10() { return MinedOreCounter[9]; }


    // Data saving related below:
    void OnDisable()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat"); // Creates a data file in DataPath with name specified in "XXX"
        PlayerData data = new PlayerData();

        for(int i=0; i<OreMinedText.Length; i++)
        {
            data.MinedOreCounterData[i] = MinedOreCounter[i];
        }
        bf.Serialize(file, data);
        file.Close();
    }

    void OnEnable()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat")) // checks if the file already exists, if it doesn't, it is created.
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open); // Opens a file to be used and Loaded.
            PlayerData data = bf.Deserialize(file) as PlayerData; // You could also cast is as PlayerData by putting (PlayerData) in front like: PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            for (int i = 0; i < OreMinedText.Length; i++)
            {
                MinedOreCounter[i] = data.MinedOreCounterData[i];
            }
        }
    }

    [Serializable]
    class PlayerData
    {
        public int[] MinedOreCounterData = new int [10]; // 10 in array has to always be equal to MinedOreCounter.Length
    }
}
