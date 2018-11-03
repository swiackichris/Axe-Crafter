using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ore : MonoBehaviour {

    // TODO organize SerializeFields, and remove what shouldn't be there.
    [SerializeField] float FullHealth = 100.0f;
    [SerializeField] float CurrentHealth = 100f;

    [SerializeField] float FullPickaxeDamage = 10.0f;
    [SerializeField] float CurrentPickaxeDamage = 10.0f;
    [SerializeField] GameObject OrePrefab;
    GameObject ore; // Required for Button to know which object should be destroyed.

    // Awake is before Start to avoid possible late ore spawn.
    private void Awake()
    {
        // Spawning First Ore
        ore = Instantiate(
            OrePrefab,
            new Vector2(9, 9), // Change later the position of new ore spawned to be posibly random
            Quaternion.identity) as GameObject;
    }

    // This coroutine destroys ore with 0 health, resets heaslth, and after 1 second spawns new ore. Also during 1 second period pickaxe damage is reset to 0.
    IEnumerator DestroyAndSpawn()
    {
        Destroy(ore); // Destroys ore created in void Awake()
        print("Destroy(ore)");

        CurrentHealth = FullHealth; // Resets health for new ore
        print("CurrentHealth = FullHealth");

        CurrentPickaxeDamage = 0;

        yield return new WaitForSeconds(1); // Wait time before new ore spawns, you could randomize it in the future

        CurrentPickaxeDamage = FullPickaxeDamage;

        // Spawn new ore
        ore = Instantiate(
        OrePrefab,
        new Vector2(9, 9),
        Quaternion.identity) as GameObject;
        print("ore = Instantiate");
    }

    // MINING
    // Each function is attached to pickaxe in different scene/mine, so that it is possible to count and save the amount of different types of ores mined.
    public void MineOre1()
    {
        CurrentHealth -= CurrentPickaxeDamage;
        print(CurrentHealth);
        if (CurrentHealth <= 0)
        {
            FindObjectOfType<GameSession>().CountMinedOre1(); // Calls a function from a different class 
            StartCoroutine(DestroyAndSpawn());
        }
    }

    public void MineOre2()
    {
        CurrentHealth -= CurrentPickaxeDamage;
        print(CurrentHealth);
        if (CurrentHealth <= 0)
        {
            FindObjectOfType<GameSession>().CountMinedOre2();
            StartCoroutine(DestroyAndSpawn());
        }
    }

    public void MineOre3()
    {
        CurrentHealth -= CurrentPickaxeDamage;
        print(CurrentHealth);
        if (CurrentHealth <= 0)
        {
            FindObjectOfType<GameSession>().CountMinedOre3();
            StartCoroutine(DestroyAndSpawn());
        }
    }

    public void MineOre4()
    {
        CurrentHealth -= CurrentPickaxeDamage;
        print(CurrentHealth);
        if (CurrentHealth <= 0)
        {
            FindObjectOfType<GameSession>().CountMinedOre4();
            StartCoroutine(DestroyAndSpawn());
        }
    }

    public void MineOre5()
    {
        CurrentHealth -= CurrentPickaxeDamage;
        print(CurrentHealth);
        if (CurrentHealth <= 0)
        {
            FindObjectOfType<GameSession>().CountMinedOre5();
            StartCoroutine(DestroyAndSpawn());
        }
    }

    public void MineOre6()
    {
        CurrentHealth -= CurrentPickaxeDamage;
        print(CurrentHealth);
        if (CurrentHealth <= 0)
        {
            FindObjectOfType<GameSession>().CountMinedOre6();
            StartCoroutine(DestroyAndSpawn());
        }
    }

    public void MineOre7()
    {
        CurrentHealth -= CurrentPickaxeDamage;
        print(CurrentHealth);
        if (CurrentHealth <= 0)
        {
            FindObjectOfType<GameSession>().CountMinedOre7();
            StartCoroutine(DestroyAndSpawn());
        }
    }

    public void MineOre8()
    {
        CurrentHealth -= CurrentPickaxeDamage;
        print(CurrentHealth);
        if (CurrentHealth <= 0)
        {
            FindObjectOfType<GameSession>().CountMinedOre8();
            StartCoroutine(DestroyAndSpawn());
        }
    }

    public void MineOre9()
    {
        CurrentHealth -= CurrentPickaxeDamage;
        print(CurrentHealth);
        if (CurrentHealth <= 0)
        {
            FindObjectOfType<GameSession>().CountMinedOre9();
            StartCoroutine(DestroyAndSpawn());
        }
    }

    public void MineOre10()
    {
        CurrentHealth -= CurrentPickaxeDamage;
        print(CurrentHealth);
        if (CurrentHealth <= 0)
        {
            FindObjectOfType<GameSession>().CountMinedOre10();
            StartCoroutine(DestroyAndSpawn());
        }
    }
}
