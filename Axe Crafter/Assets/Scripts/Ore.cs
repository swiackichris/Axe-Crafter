﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour {

    [SerializeField] private OreStats oreStats;                 
    [SerializeField] GameObject OrePrefab;                                  
    [SerializeField] private GameSession gameSession;
    [SerializeField] private PickaxeStats[] pickStats;                      
    [SerializeField] GameObject [] PickaxePrefabs;                          
    [SerializeField] ParticleSystem PickingParticle;
    GameObject ore;                                                         // Required for Button to know which object should be destroyed.
    GameObject pickaxe;

    [SerializeField] [Range(0, 1)] float PickSoundVolume = 1f;

    private int CurrentHealth;                                              // current ore health
    private int CurrentPickaxeDamage;                                       // damage applied to ore

    bool canHit = true;                                                    // required to stop mining when ore is depleted
    bool canAnimate = false;
    bool canRotate = true;

    private float RotationSpeed = 219f;


    private void Start()
    {
        CurrentHealth = oreStats.GetOreHealth();
        CurrentPickaxeDamage = pickStats[CurrentPickLevel()].GetPickaxeDamage();
        OreInstatiate();
        PickaxeInstatiate();
    }

    private void Update()
    {
        ToolAnimation();
    }

    // Each function is attached to pickaxe in different scene, so that it is possible to count and save the amount of different types of ores mined.
    public void MineOre(int i)
    {
        canAnimate = true;

        Hit();

        if (CurrentHealth <= 0)
        {
            // If ore is mined, add it
            gameSession.CountMinedOre(i);
            StartCoroutine(DestroyAndSpawn());
        }
    }

    private void Hit()
    {
        if (canHit)
        {
            // Deduct ore health
            CurrentHealth -= CurrentPickaxeDamage;

            // Plays a particle effect
            Instantiate(PickingParticle, ore.transform.position, Quaternion.identity);

            // Plays random pickaxe sound
            AudioSource.PlayClipAtPoint(pickStats[CurrentPickLevel()].GetPickaxeSound
                (UnityEngine.Random.Range(0, 5)), Camera.main.transform.position, PickSoundVolume);
        }
    }

    // Spawn ore with a random scale and position
    public void OreInstatiate()
    {
        ore = Instantiate(
        OrePrefab,
        new Vector2(RandomPX(), RandomPY()), // Change later the position of new ore spawned to be posibly random
        Quaternion.identity) as GameObject;
        print("ore = Instantiate");

        ore.transform.localScale += new Vector3(RandomScale(), RandomScale(), 0);
        ore.transform.Rotate(0, 0, RandomRotation());
    }

    // Spawn currently owned pickaxe
    public void PickaxeInstatiate()
    {
        pickaxe = Instantiate(
        PickaxePrefabs[CurrentPickLevel()],
        new Vector2(14, 7), // Change later the position of new ore spawned to be posibly random
        Quaternion.identity) as GameObject;
        print("pickaxe = Instantiate");
    }

    // This coroutine destroys ore with 0 health, resets heaslth, and after 1 second spawns new ore. Also during 1 second period pickaxe damage is reset to 0.
    IEnumerator DestroyAndSpawn()
    {
        canHit = false;

        // Destroys ore created in void Start();
        Destroy(ore);

        // Resets health for new ore
        CurrentHealth = oreStats.GetOreHealth();

        // Wait time before new ore spawns, so that it can't be damaged while it hasn't spawned
        CurrentPickaxeDamage = 0;
        yield return new WaitForSeconds(RandomSpawnTime());

        // Initializes pick damage after it has been reduced to 0
        CurrentPickaxeDamage = pickStats[CurrentPickLevel()].GetPickaxeDamage();

        // Spawns ore
        OreInstatiate();

        canHit = true;
    }

    private void ToolAnimation()
    {
        if (canAnimate)
        {
            if (canRotate && pickaxe.transform.rotation.eulerAngles.z <= 45)
            {
                pickaxe.transform.Rotate(Vector3.forward * (RotationSpeed * Time.deltaTime));
                if (pickaxe.transform.rotation.eulerAngles.z >= 45)
                {
                    canRotate = false;
                }
            }

            if (!canRotate && pickaxe.transform.rotation.eulerAngles.z >= 1)
            {
                pickaxe.transform.Rotate(Vector3.back * (RotationSpeed * Time.deltaTime));
                if (pickaxe.transform.rotation.eulerAngles.z <= 1)
                {
                    canRotate = true;
                    canAnimate = false;
                }
            }
        }
    }

    public int CurrentPickLevel() { return gameSession.GetPickLevel(); }
    public int RandomPX() { return UnityEngine.Random.Range(2, 16); }
    public int RandomPY() { return UnityEngine.Random.Range(4, 20); }
    public float RandomScale() { return UnityEngine.Random.Range(-0.75f, 0.75f); }
    public int RandomRotation() { return UnityEngine.Random.Range(0, 360); }
    public float RandomSpawnTime() { return UnityEngine.Random.Range(0.25f, 0.75f); }

}
