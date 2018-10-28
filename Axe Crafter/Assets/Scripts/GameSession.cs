using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI OreMinedText;
    [SerializeField] int MinedOreCounter = 0;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        OreMinedText.text = MinedOreCounter.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // Time.timeScale = gameSpeed; -> TO BE DELETED LATER
        // You can gradually increase the gameSpeed as the time passes.
    }

    public void CountMinedOre()
    {
        MinedOreCounter += 1;
        OreMinedText.text = MinedOreCounter.ToString();
    }
}
