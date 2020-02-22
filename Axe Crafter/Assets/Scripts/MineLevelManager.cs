using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MineLevelManager : MonoBehaviour {

    [SerializeField] Button[] MineLevelsButton = null;
    [SerializeField] Button[] MineLevelsResourceButton = null;
    [SerializeField] TextMeshProUGUI[] MineResourceText = null;

    [SerializeField] GameSession gameSessionScriptPrefab = null;

    [SerializeField] int[] LevelUnlockOre = null;

    // Use this for initialization
    void Start()
    {
        // Displays resources required text for next level
        DisplayMineResourceText();

        // Displays button to pay resources for next level
        DisplayMineResourceButton();

        for (int i = 0; i <= gameSessionScriptPrefab.GetCurrentMineLevel(); i++)
        {
            MineLevelsButton[i].interactable = true;
            MineLevelsResourceButton[i].interactable = false;
            MineResourceText[i].text = null;
        }
    }

    public void PayForLevel(int i)
    {
        // If resources owned > resources required
        if (gameSessionScriptPrefab.GetMinedOreCounter(i-1) >= LevelUnlockOre[i-1])
        {
            // Enables the button
            MineLevelsButton[i].interactable = true;

            // Disables the button
            MineLevelsResourceButton[i].interactable = false;

            // Disables upgrade cost text
            MineResourceText[i].text = null;

            // Deducts resources required for unlocking the level
            gameSessionScriptPrefab.PayMaterialsForMineUnlock(i-1);

            // Displays resources required text for next level
            DisplayMineResourceText();

            // Displays button to pay resources for next level
            DisplayMineResourceButton();
        }
    }

    // Displays resources required text for next level
    void DisplayMineResourceText()
    {
        MineResourceText[gameSessionScriptPrefab.GetCurrentMineLevel() + 1].text 
            = (gameSessionScriptPrefab.GetMinedOreCounter(gameSessionScriptPrefab.GetCurrentMineLevel()).ToString() 
            + "/" + LevelUnlockOre[gameSessionScriptPrefab.GetCurrentMineLevel()].ToString());
    }

    // Displays button to pay resources for next level
    void DisplayMineResourceButton()
    {
        MineLevelsResourceButton[gameSessionScriptPrefab.GetCurrentMineLevel() + 1].interactable = true;
    }

    public int GetLevelUnlockOre(int i) { return LevelUnlockOre[i]; }

}
