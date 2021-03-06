﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour {

    // MAIN MENU
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    // MINING
    public void LoadMining(int i)
    {
        SceneManager.LoadScene("Mine" + i);
    }

    public void LoadMiningSelection()
    {
        SceneManager.LoadScene("MineSelectionScreen");
    }

    // WOODCUTTING
    public void LoadWoodcutting(int i)
    {
        SceneManager.LoadScene("Wood" + i);
    }

    public void LoadWoodSelection()
    {
        SceneManager.LoadScene("WoodSelectionScreen");
    }

    // BATTLE
    public void LoadBattle(int i)
    {
        SceneManager.LoadScene("Battle" + i);
    }

    public void LoadBattleSelectionScreen()
    {
        SceneManager.LoadScene("BattleSelectionScreen");
    }

    // CRAFTING PICKAXES
    public void LoadPickCraftingScreen()
    {
        SceneManager.LoadScene("Pick Crafting Screen");
    }

    // CRAFTING AXES
    public void LoadAxeCraftingScreen()
    {
        SceneManager.LoadScene("Axe Crafting Screen");
    }
}
