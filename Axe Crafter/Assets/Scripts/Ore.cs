using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour {

    [SerializeField] private OreStats oreStatsScriptPrefab;                 // Should be the same as OrePrefab
    [SerializeField] private GameSession gameSessionStatsScriptPrefab;
    [SerializeField] private PickaxeStats[] pickaxeStatsScriptPrefabs;      // Should be the same as PickaxePrefab TODO these could be deleted

    private int CurrentHealth;                                              // How much health currently mined ore has
    private int CurrentPickaxeDamage;                                       // How much damage we apply to currently mined ore

    [SerializeField] GameObject OrePrefab;                                  // Ore prefab to be instatiated and mined
    [SerializeField] GameObject [] PickaxePrefabs;                          // Pickaxe prefab to be instatiated and mined with
    GameObject ore;                                                         // Required for Button to know which object should be destroyed.
    GameObject pickaxe;

    bool isRotated = false;                                                 // Reuqired for proper pickaxe animation

    private void Start()
    {
        // You could possibly remove CurrentHealth and CurrentPickaxeDamage
        CurrentHealth = oreStatsScriptPrefab.GetOreHealth();
        CurrentPickaxeDamage = pickaxeStatsScriptPrefabs[gameSessionStatsScriptPrefab.GetPickLevel()].GetPickaxeDamage();
        OreInstatiate();
        PickaxeInstatiate();
    }

    // MINING
    // Each function is attached to pickaxe in different scene/mine, so that it is possible to count and save the amount of different types of ores mined.
    public void MineOre(int i)
    {
        // Rotates pickaxe
        RotateTool();

        // Rotates pickaxe to starting position
        StartCoroutine(ResetToolRotation());

        // Deducts ore health
        CurrentHealth -= CurrentPickaxeDamage;
        print("CurrentHealth=" +CurrentHealth);
        if (CurrentHealth <= 0)
        {
            // If ore is mined, add it
            FindObjectOfType<GameSession>().CountMinedOre(i);
            StartCoroutine(DestroyAndSpawn());
        }
    }

    // Spawn ore at a position
    public void OreInstatiate()
    {
        ore = Instantiate(
        OrePrefab,
        new Vector2(9, 9), // Change later the position of new ore spawned to be posibly random
        Quaternion.identity) as GameObject;
        print("ore = Instantiate");
    }

    // Spawn currently owned pickaxe at a position
    public void PickaxeInstatiate()
    {
        pickaxe = Instantiate(
        PickaxePrefabs[gameSessionStatsScriptPrefab.GetPickLevel()],
        new Vector2(14, 7), // Change later the position of new ore spawned to be posibly random
        Quaternion.identity) as GameObject;
        print("pickaxe = Instantiate");
    }

    // Rotates pickaxe 45 degrees
    public void RotateTool()
    {
        pickaxe.transform.Rotate(0, 0, 45);
        isRotated = true;
    }

    // This coroutine destroys ore with 0 health, resets heaslth, and after 1 second spawns new ore. Also during 1 second period pickaxe damage is reset to 0.
    IEnumerator DestroyAndSpawn()
    {
        // Destroys ore created in void Start();
        Destroy(ore);
        print("Destroy(ore)");

        // Resets health for new ore
        CurrentHealth = oreStatsScriptPrefab.GetOreHealth();
        print("CurrentHealth = FullHealth");

        // Wait time before new ore spawns, so that it can't be damaged while it hasn't spawned
        // TODO you could randomize it in the future
        CurrentPickaxeDamage = 0;
        yield return new WaitForSeconds(1);

        // Initializes pick damage after it has been reduced to 0
        CurrentPickaxeDamage = pickaxeStatsScriptPrefabs[gameSessionStatsScriptPrefab.GetPickLevel()].GetPickaxeDamage();

        // Spawns ore
        OreInstatiate();
    }

    // Rotates pickaxe back to starting position
    IEnumerator ResetToolRotation()
    {
        yield return new WaitForSeconds(0.05f);
        pickaxe.transform.Rotate(0, 0, -45);
        isRotated = false;
    }
}
