using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleLevelManager : MonoBehaviour
{

    [SerializeField] Button[] BattleLevelsButton;                        
    [SerializeField] Button[] BattleLevelsResourceButton;                   
    [SerializeField] TextMeshProUGUI[] BattleResourceText;                     
    [SerializeField] TextMeshProUGUI CurrentGoldText;                     

    [SerializeField] GameSession gameSessionScriptPrefab;

    [SerializeField] int[] LevelUnlockGold;


    // Use this for initialization
    void Start()
    {
        // Displays current gold
        CurrentGoldText.text = Math.Round(gameSessionScriptPrefab.GetCurrentGold(), 1).ToString();

        // Displays resources required text for next level
        DisplayBattleResourceText();

        // Displays button to pay resources for next level
        DisplayBattleResourceButton();

        for (int i = 0; i <= gameSessionScriptPrefab.GetCurrentBattleLevel(); i++)
        {
            BattleLevelsButton[i].interactable = true;
            BattleLevelsResourceButton[i].interactable = false;
            BattleResourceText[i].text = null; // TODO delete or keep
        }
    }

    public void PayForLevel(int i)
    {
        // If resources owned > resources required
        if (gameSessionScriptPrefab.GetCurrentGold() >= LevelUnlockGold[i-1])
        {
            // Enables the button
            BattleLevelsButton[i].interactable = true;

            // Disables the button
            BattleLevelsResourceButton[i].interactable = false;

            // Disables upgrade cost text
            BattleResourceText[i].text = null;

            // Deducts resources required for unlocking the level
            gameSessionScriptPrefab.PayMaterialsForBattleUnlock(i-1);

            // Displays resources required for next level
            DisplayBattleResourceText();

            // Displays button to pay resources for next level
            DisplayBattleResourceButton();

            // Displays current gold
            CurrentGoldText.text = Math.Round(gameSessionScriptPrefab.GetCurrentGold(), 1).ToString();
        }
    }

    // Displays resources required text for next level
    void DisplayBattleResourceText()
    {
        BattleResourceText[gameSessionScriptPrefab.GetCurrentBattleLevel() + 1].text
            = LevelUnlockGold[gameSessionScriptPrefab.GetCurrentBattleLevel()].ToString();
    }

    // Displays button to pay resources for next level
    void DisplayBattleResourceButton()
    {
        BattleLevelsResourceButton[gameSessionScriptPrefab.GetCurrentBattleLevel() + 1].interactable = true;
    }

    public int GetLevelUnlockGold(int i) { return LevelUnlockGold[i]; }

}
