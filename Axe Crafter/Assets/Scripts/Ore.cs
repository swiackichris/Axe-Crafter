using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour {

    [SerializeField] private OreStats oreStats;                 // Should be the same as OrePrefab
    [SerializeField] GameObject OrePrefab;                                  // Ore prefab to be instatiated and mined
    [SerializeField] private GameSession gameSession;
    [SerializeField] private PickaxeStats[] pickStats;      // Should be the same as PickaxePrefab TODO these could be deleted
    [SerializeField] GameObject [] PickaxePrefabs;                          // Pickaxe prefab to be instatiated and mined with
    GameObject ore;                                                         // Required for Button to know which object should be destroyed.
    GameObject pickaxe;

    [SerializeField] [Range(0, 1)] float PickSoundVolume = 1f;

    private int CurrentHealth;                                              // How much health currently mined ore has
    private int CurrentPickaxeDamage;                                       // How much damage we apply to currently mined ore

    bool isRotated = false;                                                  // Reuqired for proper pickaxe animation
    bool canRotate = true;

    private void Start()
    {
        // You could possibly remove CurrentHealth and CurrentPickaxeDamage
        CurrentHealth = oreStats.GetOreHealth();
        CurrentPickaxeDamage = pickStats[CurrentPickLevel()].GetPickaxeDamage();
        OreInstatiate();
        PickaxeInstatiate();
    }

    // MINING
    // Each function is attached to pickaxe in different scene/mine, so that it is possible to count and save the amount of different types of ores mined.
    public void MineOre(int i)
    {
        if (canRotate)
        {
            // Rotates axe
            RotateTool();
            canRotate = false;
        }

        if (isRotated)
        {
            // Rotates pickaxe to starting position
            StartCoroutine(ResetToolRotation());

            // Deducts ore health
            CurrentHealth -= CurrentPickaxeDamage;
            print("CurrentHealth=" + CurrentHealth);
            if (CurrentHealth <= 0)
            {
                // If ore is mined, add it
                FindObjectOfType<GameSession>().CountMinedOre(i);
                StartCoroutine(DestroyAndSpawn());
            }
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
        PickaxePrefabs[CurrentPickLevel()],
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
        CurrentHealth = oreStats.GetOreHealth();
        print("CurrentHealth = FullHealth");

        // Wait time before new ore spawns, so that it can't be damaged while it hasn't spawned
        // TODO you could randomize it in the future
        CurrentPickaxeDamage = 0;
        yield return new WaitForSeconds(1);

        // Initializes pick damage after it has been reduced to 0
        CurrentPickaxeDamage = pickStats[CurrentPickLevel()].GetPickaxeDamage();

        // Spawns ore
        OreInstatiate();
    }

    // Rotates pickaxe back to starting position
    IEnumerator ResetToolRotation()
    {
        isRotated = false;

        // Plays random pickaxe sound
        AudioSource.PlayClipAtPoint(pickStats[CurrentPickLevel()].GetPickaxeSound
            (UnityEngine.Random.Range(0, 5)), Camera.main.transform.position, PickSoundVolume);

        yield return new WaitForSeconds(0.1f);
        pickaxe.transform.Rotate(0, 0, -45);
        canRotate = true;
    }

    public int CurrentPickLevel()
    {
        return gameSession.GetPickLevel();
    }
}
