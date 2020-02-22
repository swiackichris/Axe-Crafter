using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ForestLevelManager : MonoBehaviour
{

    [SerializeField] Button[] ForestLevelsButton = null;
    [SerializeField] Button[] ForestLevelsResourceButton = null;
    [SerializeField] TextMeshProUGUI[] ForestResourceText = null;

    [SerializeField] GameSession gameSessionScriptPrefab = null;

    [SerializeField] int[] LevelUnlockWood = null;

    // Use this for initialization
    void Start()
    {
        // Displays resources required text for next level
        DisplayForestResourceText();

        // Displays button to pay resources for next level
        DisplayForestResourceButton();

        for (int i = 0; i <= gameSessionScriptPrefab.GetCurrentForestLevel(); i++)
        {
            ForestLevelsButton[i].interactable = true;
            ForestLevelsResourceButton[i].interactable = false;
            ForestResourceText[i].text = null;
        }
    }

    public void PayForLevel(int i)
    {
        // If resources owned > resources required
        if (gameSessionScriptPrefab.GetChoppedWoodCounter(i-1) >= LevelUnlockWood[i-1])
        {
            // Enables the button
            ForestLevelsButton[i].interactable = true;

            // Disables the button
            ForestLevelsResourceButton[i].interactable = false;

            // Disables upgrade cost text
            ForestResourceText[i].text = null;

            // Deducts resources required for unlocking the level
            gameSessionScriptPrefab.PayMaterialsForForestUnlock(i-1);

            // Displays resources required text for next level
            DisplayForestResourceText();

            // Displays button to pay resources for next level
            DisplayForestResourceButton();
        }
    }

    // Displays resources required text for next level
    void DisplayForestResourceText()
    {
        ForestResourceText[gameSessionScriptPrefab.GetCurrentForestLevel() + 1].text
            = (gameSessionScriptPrefab.GetChoppedWoodCounter(gameSessionScriptPrefab.GetCurrentForestLevel()).ToString()
            + "/" + LevelUnlockWood[gameSessionScriptPrefab.GetCurrentForestLevel()].ToString());
    }

    // Displays button to pay resources for next level
    void DisplayForestResourceButton()
    {
        ForestLevelsResourceButton[gameSessionScriptPrefab.GetCurrentForestLevel() + 1].interactable = true;
    }

    public int GetLevelUnlockWood(int i) { return LevelUnlockWood[i]; }
}
