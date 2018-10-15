using System.Collections;
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

    public void LoadMining()
    {
        SceneManager.LoadScene("Mining Screen");
    }

    public void LoadWoodcutting()
    {
        SceneManager.LoadScene("Woodcutting Screen");
    }

    public void LoadBattle()
    {
        SceneManager.LoadScene("Battle Screen");
    }
}
