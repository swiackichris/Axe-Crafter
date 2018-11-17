using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameSession : MonoBehaviour
{
    // TODO - Change this possibly to an Array?
    [SerializeField] TextMeshProUGUI OreMinedText1;
    [SerializeField] TextMeshProUGUI OreMinedText2;
    [SerializeField] TextMeshProUGUI OreMinedText3;
    [SerializeField] TextMeshProUGUI OreMinedText4;
    [SerializeField] TextMeshProUGUI OreMinedText5;
    [SerializeField] TextMeshProUGUI OreMinedText6;
    [SerializeField] TextMeshProUGUI OreMinedText7;
    [SerializeField] TextMeshProUGUI OreMinedText8;
    [SerializeField] TextMeshProUGUI OreMinedText9;
    [SerializeField] TextMeshProUGUI OreMinedText10;

    // TODO - Change this possibly to an Array
    [SerializeField] int MinedOreCounter1 = 0;
    [SerializeField] int MinedOreCounter2 = 0;
    [SerializeField] int MinedOreCounter3 = 0;
    [SerializeField] int MinedOreCounter4 = 0;
    [SerializeField] int MinedOreCounter5 = 0;
    [SerializeField] int MinedOreCounter6 = 0;
    [SerializeField] int MinedOreCounter7 = 0;
    [SerializeField] int MinedOreCounter8 = 0;
    [SerializeField] int MinedOreCounter9 = 0;
    [SerializeField] int MinedOreCounter10 = 0;

    // Use this for initialization
    void Start()
    {
        OreMinedText1.text = MinedOreCounter1.ToString();
        OreMinedText2.text = MinedOreCounter2.ToString();
        OreMinedText3.text = MinedOreCounter3.ToString();
        OreMinedText4.text = MinedOreCounter4.ToString();
        OreMinedText5.text = MinedOreCounter5.ToString();
        OreMinedText6.text = MinedOreCounter6.ToString();
        OreMinedText7.text = MinedOreCounter7.ToString();
        OreMinedText8.text = MinedOreCounter8.ToString();
        OreMinedText9.text = MinedOreCounter9.ToString();
        OreMinedText10.text = MinedOreCounter10.ToString();
    }

    // Displays and updates the amount of ore mined
    public void CountMinedOre1()
    {
        MinedOreCounter1 += 1;
        OreMinedText1.text = MinedOreCounter1.ToString();
    }

    public void CountMinedOre2()
    {
        MinedOreCounter2 += 1;
        OreMinedText2.text = MinedOreCounter2.ToString();
    }

    public void CountMinedOre3()
    {
        MinedOreCounter3 += 1;
        OreMinedText3.text = MinedOreCounter3.ToString();
    }

    public void CountMinedOre4()
    {
        MinedOreCounter4 += 1;
        OreMinedText4.text = MinedOreCounter4.ToString();
    }

    public void CountMinedOre5()
    {
        MinedOreCounter5 += 1;
        OreMinedText5.text = MinedOreCounter5.ToString();
    }

    public void CountMinedOre6()
    {
        MinedOreCounter6 += 1;
        OreMinedText6.text = MinedOreCounter6.ToString();
    }

    public void CountMinedOre7()
    {
        MinedOreCounter7 += 1;
        OreMinedText7.text = MinedOreCounter7.ToString();
    }

    public void CountMinedOre8()
    {
        MinedOreCounter8 += 1;
        OreMinedText8.text = MinedOreCounter8.ToString();
    }

    public void CountMinedOre9()
    {
        MinedOreCounter9 += 1;
        OreMinedText9.text = MinedOreCounter9.ToString();
    }

    public void CountMinedOre10()
    {
        MinedOreCounter10 += 1;
        OreMinedText10.text = MinedOreCounter10.ToString();
    }

    // Updates the amount of ores after purchasing items in the market
    // TODO Prices added later
    public void BuyPickaxe1()
    {
        MinedOreCounter1 -= 1;
        OreMinedText1.text = MinedOreCounter1.ToString();
    }

    public void BuyPickaxe2()
    {
        MinedOreCounter2 -= 1;
        OreMinedText2.text = MinedOreCounter2.ToString();
    }

    public void BuyPickaxe3()
    {
        MinedOreCounter3 -= 1;
        OreMinedText3.text = MinedOreCounter3.ToString();
    }

    public void BuyPickaxe4()
    {
        MinedOreCounter4 -= 1;
        OreMinedText4.text = MinedOreCounter4.ToString();
    }

    public void BuyPickaxe5()
    {
        MinedOreCounter5 -= 1;
        OreMinedText5.text = MinedOreCounter5.ToString();
    }

    public void BuyPickaxe6()
    {
        MinedOreCounter6 -= 1;
        OreMinedText6.text = MinedOreCounter6.ToString();
    }

    public void BuyPickaxe7()
    {
        MinedOreCounter7 -= 1;
        OreMinedText7.text = MinedOreCounter7.ToString();
    }

    public void BuyPickaxe8()
    {
        MinedOreCounter8 -= 1;
        OreMinedText8.text = MinedOreCounter8.ToString();
    }

    public void BuyPickaxe9()
    {
        MinedOreCounter9 -= 1;
        OreMinedText9.text = MinedOreCounter9.ToString();
    }

    public void BuyPickaxe10()
    {
        MinedOreCounter10 -= 1;
        OreMinedText10.text = MinedOreCounter10.ToString();
    }

    // Data saving related below:
    void OnDisable()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat"); // Creates a data file in DataPath with name specified in "XXX"

        PlayerData data = new PlayerData();
        // Later possibly change below to array
        data.MinedOreCounter1 = MinedOreCounter1;
        data.MinedOreCounter2 = MinedOreCounter2;
        data.MinedOreCounter3 = MinedOreCounter3;
        data.MinedOreCounter4 = MinedOreCounter4;
        data.MinedOreCounter5 = MinedOreCounter5;
        data.MinedOreCounter6 = MinedOreCounter6;
        data.MinedOreCounter7 = MinedOreCounter7;
        data.MinedOreCounter8 = MinedOreCounter8;
        data.MinedOreCounter9 = MinedOreCounter9;
        data.MinedOreCounter10 = MinedOreCounter10;

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

            // Later Possibly change this to array
            MinedOreCounter1 = data.MinedOreCounter1;
            MinedOreCounter2 = data.MinedOreCounter2;
            MinedOreCounter3 = data.MinedOreCounter3;
            MinedOreCounter4 = data.MinedOreCounter4;
            MinedOreCounter5 = data.MinedOreCounter5;
            MinedOreCounter6 = data.MinedOreCounter6;
            MinedOreCounter7 = data.MinedOreCounter7;
            MinedOreCounter8 = data.MinedOreCounter8;
            MinedOreCounter9 = data.MinedOreCounter9;
            MinedOreCounter10 = data.MinedOreCounter10;
        }
    }

    [Serializable]
    class PlayerData
    {
        // Later Possibly change this to array
        public int MinedOreCounter1;
        public int MinedOreCounter2;
        public int MinedOreCounter3;
        public int MinedOreCounter4;
        public int MinedOreCounter5;
        public int MinedOreCounter6;
        public int MinedOreCounter7;
        public int MinedOreCounter8;
        public int MinedOreCounter9;
        public int MinedOreCounter10;
    }
}
