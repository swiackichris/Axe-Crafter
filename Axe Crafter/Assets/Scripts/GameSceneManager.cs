﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    // MINING
    public void LoadMining1()
    {
        SceneManager.LoadScene("Mine1");
    }

    public void LoadMining2()
    {
        SceneManager.LoadScene("Mine2");
    }

    public void LoadMining3()
    {
        SceneManager.LoadScene("Mine3");
    }

    public void LoadMining4()
    {
        SceneManager.LoadScene("Mine4");
    }

    public void LoadMining5()
    {
        SceneManager.LoadScene("Mine5");
    }

    public void LoadMining6()
    {
        SceneManager.LoadScene("Mine6");
    }

    public void LoadMining7()
    {
        SceneManager.LoadScene("Mine7");
    }

    public void LoadMining8()
    {
        SceneManager.LoadScene("Mine8");
    }

    public void LoadMining9()
    {
        SceneManager.LoadScene("Mine9");
    }

    public void LoadMining10()
    {
        SceneManager.LoadScene("Mine10");
    }
    // MINING

    public void LoadWoodcutting()
    {
        SceneManager.LoadScene("Woodcutting Screen");
    }

    public void LoadBattle()
    {
        SceneManager.LoadScene("Battle Screen");
    }
}
