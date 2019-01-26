using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MineLevelManager : MonoBehaviour {

    [SerializeField] Button[] MineLevelsButton;
    [SerializeField] Button[] MineLevelsResourceButton;
    [SerializeField] TextMeshProUGUI[] MineResourceText;

    [SerializeField] GameSession gameSessionScriptPrefab;

    [SerializeField] int[] LevelUnlockOre;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < LevelUnlockOre.Length; i++)
        {
            MineResourceText[i].text = (gameSessionScriptPrefab.GetMinedOreCounter(i).ToString() + "/" + LevelUnlockOre[i].ToString());
        }

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
        if (gameSessionScriptPrefab.GetMinedOreCounter(i) > LevelUnlockOre[i])
        {
            // Deducts resources required for unlocking the level
            gameSessionScriptPrefab.PayMaterialsForMineUnlock(i);

            // Enables the button
            MineLevelsButton[i].interactable = true;

            // Disables the button
            MineLevelsResourceButton[i].interactable = false;

            // Disables upgrade cost text
            MineResourceText[i].text = null;
        }
    }

    public int GetLevelUnlockOre(int i) { return LevelUnlockOre[i]; }
}
