using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour {

    [SerializeField] private WoodStats woodStatsScriptPrefab;                 // Should be the same as OrePrefab
    [SerializeField] private GameSession gameSessionStatsScriptPrefab;
    [SerializeField] private AxeStats[] axeStatsScriptPrefabs;         // Should be the same as AxePrefab TODO these could be deleted

    private int CurrentHealth;                                              // How much health currently mined ore has
    private int CurrentAxeDamage;                                       // How much damage we apply to currently mined ore

    [SerializeField] GameObject WoodPrefab;                                  // Ore prefab to be instatiated and mined
    [SerializeField] GameObject[] AxePrefabs;                          // Axe prefab to be instatiated and mined with
    GameObject wood;                                                         // Required for Button to know which object should be destroyed.
    GameObject axe;

    bool isRotated = false;                                                 // Reuqired for proper Axe animation

    private void Start()
    {
        // You could possibly remove CurrentHealth and CurrentAxeDamage
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

        // Deducts ore health
        CurrentHealth -= CurrentAxeDamage;
        print("CurrentHealth=" + CurrentHealth);
        if (CurrentHealth <= 0)
        {
            // If ore is mined, add it
            FindObjectOfType<GameSession>().CountChoppedWood(i);
            StartCoroutine(DestroyAndSpawn());
        }
    }

    // Spawn ore at a position
    public void WoodInstantiate()
    {
        wood = Instantiate(
        WoodPrefab,
        new Vector2(9, 9), // Change later the position of new ore spawned to be posibly random
        Quaternion.identity) as GameObject;
        print("wood = Instantiate");
    }

    // Spawn currently owned axe at a position
    public void AxeInstantiate()
    {
        axe = Instantiate(
        AxePrefabs[gameSessionStatsScriptPrefab.GetAxeLevel()],
        new Vector2(14, 7), // Change later the position of new ore spawned to be posibly random
        Quaternion.identity) as GameObject;
        print("axe = Instantiate");
    }

    // Rotates axe 45 degrees
    public void RotateTool()
    {
        axe.transform.Rotate(0, 0, 45);
        isRotated = true;
    }

    // This coroutine destroys ore with 0 health, resets heaslth, and after 1 second spawns new ore. Also during 1 second period pickaxe damage is reset to 0.
    IEnumerator DestroyAndSpawn()
    {
        // Destroys ore created in void Start();
        Destroy(wood);
        print("Destroy(ore)");

        // Resets health for new ore
        CurrentHealth = woodStatsScriptPrefab.GetWoodHealth();
        print("CurrentHealth = FullHealth");

        // Wait time before new ore spawns, so that it can't be damaged while it hasn't spawned
        // TODO you could randomize it in the future
        CurrentAxeDamage = 0;
        yield return new WaitForSeconds(1);

        // Initializes axe damage after it has been reduced to 0
        CurrentAxeDamage = axeStatsScriptPrefabs[gameSessionStatsScriptPrefab.GetAxeLevel()].GetAxeDamage();

        // Spawns ore
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
