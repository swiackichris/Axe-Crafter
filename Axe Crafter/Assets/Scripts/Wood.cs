using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour {

    [SerializeField] private WoodStats woodStatsScriptPrefab;               // Should be the same as WoodPrefab
    [SerializeField] private GameSession gameSessionStatsScriptPrefab;
    [SerializeField] private AxeStats[] axeStatsScriptPrefabs;              // Should be the same as AxePrefab TODO these could be deleted

    private int CurrentHealth;                                              // How much health currently chopped wood has
    private int CurrentAxeDamage;                                           // How much damage we apply to currently chopped wood

    [SerializeField] GameObject WoodPrefab;                                 // Wood prefab to be instatiated and chopped
    [SerializeField] GameObject[] AxePrefabs;                               // Axe prefab to be instatiated and chopped with
    GameObject wood;                                                        // Required for Button to know which object should be destroyed.
    GameObject axe;

    bool isRotated = false;                                                 // Required for proper Axe animation

    private void Start()
    {
        CurrentHealth = woodStatsScriptPrefab.GetWoodHealth();
        CurrentAxeDamage = axeStatsScriptPrefabs[gameSessionStatsScriptPrefab.GetAxeLevel()].GetAxeDamage();
        WoodInstantiate();
        AxeInstantiate();
    }

    // MINING
    // Each function is attached to axe in different scene/mine, so that it is possible to count and save the amount of different types of ores mined.
    public void ChopWood(int i)
    {
        // Rotates axe
        RotateTool();

        // Rotates axe to starting position
        StartCoroutine(ResetToolRotation());

        // Deducts wood health
        CurrentHealth -= CurrentAxeDamage;
        print("CurrentHealth=" + CurrentHealth);
        if (CurrentHealth <= 0)
        {
            // If wood is mined, add it
            FindObjectOfType<GameSession>().CountChoppedWood(i);
            StartCoroutine(DestroyAndSpawn());
        }
    }

    // Spawn wood at a position
    public void WoodInstantiate()
    {
        wood = Instantiate(
        WoodPrefab,
        new Vector2(9, 13), // Change later the position of new wood spawned to be posibly random
        Quaternion.identity) as GameObject;
        print("wood = Instantiate");
    }

    // Spawn currently owned axe at a position
    public void AxeInstantiate()
    {
        axe = Instantiate(
        AxePrefabs[gameSessionStatsScriptPrefab.GetAxeLevel()],
        new Vector2(14, 7), // Change later the position of new wood spawned to be posibly random
        Quaternion.identity) as GameObject;
        print("axe = Instantiate");
    }

    // Rotates axe 45 degrees
    public void RotateTool()
    {
        axe.transform.Rotate(0, 0, 45);
        isRotated = true;
    }

    // This coroutine destroys wood with 0 health, resets health, and after 1 second spawns new wood. Also during 1 second period axe damage is reset to 0.
    IEnumerator DestroyAndSpawn()
    {
        // Destroys wood created in void Start();
        Destroy(wood);
        print("Destroy(wood)");

        // Resets health for new wood
        CurrentHealth = woodStatsScriptPrefab.GetWoodHealth();
        print("CurrentHealth = FullHealth");

        // Wait time before new wood spawns, so that it can't be damaged while it hasn't spawned
        // TODO you could randomize it in the future
        CurrentAxeDamage = 0;
        yield return new WaitForSeconds(1);

        // Initializes axe damage after it has been reduced to 0
        CurrentAxeDamage = axeStatsScriptPrefabs[gameSessionStatsScriptPrefab.GetAxeLevel()].GetAxeDamage();

        // Spawns wood
        WoodInstantiate();
    }

    // Rotates axe back to starting position
    IEnumerator ResetToolRotation()
    {
        yield return new WaitForSeconds(0.05f);
        axe.transform.Rotate(0, 0, -45);
        isRotated = false;
    }
}
