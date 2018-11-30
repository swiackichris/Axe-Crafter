using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour {

    [SerializeField] private OreStats oreStatsScript; // Should be the same as OrePrefab
    [SerializeField] private PickaxeStats pickaxeStatsScript; // Should be the same as PickaxePrefab

    [SerializeField] public float CurrentHealth = 100f;
    [SerializeField] public float CurrentPickaxeDamage = 10f;

    [SerializeField] GameObject OrePrefab;
    [SerializeField] GameObject PickaxePrefab;
    GameObject ore; // Required for Button to know which object should be destroyed.

    private void Start()
    {
        CurrentHealth = oreStatsScript.GetOreHealth();
        CurrentPickaxeDamage = pickaxeStatsScript.GetPickaxeDamage();
        OreInstatiate();
    }

    // MINING
    // Each function is attached to pickaxe in different scene/mine, so that it is possible to count and save the amount of different types of ores mined.
    public void MineOre(int i)
    {
        CurrentHealth -= CurrentPickaxeDamage;
        print("CurrentHealth=" +CurrentHealth);
        if (CurrentHealth <= 0)
        {
            FindObjectOfType<GameSession>().CountMinedOre(i);
            StartCoroutine(DestroyAndSpawn());
        }
    }

    public void OreInstatiate()
    {
        // Spawn new ore
        ore = Instantiate(
        OrePrefab,
        new Vector2(9, 9), // Change later the position of new ore spawned to be posibly random
        Quaternion.identity) as GameObject;
        print("ore = Instantiate");
    }

    // This coroutine destroys ore with 0 health, resets heaslth, and after 1 second spawns new ore. Also during 1 second period pickaxe damage is reset to 0.
    IEnumerator DestroyAndSpawn()
    {
        Destroy(ore); // Destroys ore created in void Start();
        print("Destroy(ore)");

        CurrentHealth = oreStatsScript.GetOreHealth(); // Resets health for new ore
        print("CurrentHealth = FullHealth");

        CurrentPickaxeDamage = 0;

        yield return new WaitForSeconds(1); // Wait time before new ore spawns, you could randomize it in the future

        CurrentPickaxeDamage = pickaxeStatsScript.GetPickaxeDamage();
        OreInstatiate();
    }
}
