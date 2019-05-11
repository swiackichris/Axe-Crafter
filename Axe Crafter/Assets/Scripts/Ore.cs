using System;
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
    [SerializeField] GameObject DamageText;
    GameObject ore;                                                         
    GameObject pickaxe;
    GameObject dmgtxt;

    [SerializeField] [Range(0, 1)] float PickSoundVolume = 1f;

    private float CurrentHealth;                                            
    private float CurrentPickaxeDamage;                                     

    bool canHit = true;                                                     
    bool canAnimate = false;
    bool canRotate = true;

    private float RotationSpeed = 200f;
    private float UpgradeToolMultiplier = 1.05f;


    private void Start()
    {
        CurrentHealth = oreStats.GetOreHealth();
        CurrentPickaxeDamage = pickStats[gameSession.GetPickLevel()].GetPickaxeDamage() * (float)Math.Pow(UpgradeToolMultiplier, gameSession.GetPickUpgradeCounter());
        OreInstantiate();
        PickaxeInstantiate();
    }

    private void Update()
    {
        MoveToolOnTouch();
        ToolAnimation();
    }

    // Moves currently used tool to touch position
    private void MoveToolOnTouch()
    {
        if (Input.touchCount > 0) { pickaxe.transform.position = new Vector3(Input.GetTouch(0).position.x / Screen.width * 18, Input.GetTouch(0).position.y / Screen.height * 32, 0); }
    }

    // Each function is attached to pickaxe in different scene, so that it is possible to count and save the amount of different types of ores mined.
    public void MineOre(int i)
    {
        canAnimate = true;

        // Checks if position close to a monster is clicked
        if (HitCheckX() <= 5 && HitCheckY() <= 5) { Hit(); }

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

            // Shows damage numbers
            ShowDamageText();
        }
    }

    private float HitCheckX() { return Mathf.Abs((Input.GetTouch(0).position.x / Screen.width * 18) - ore.transform.position.x); }
    private float HitCheckY() { return Mathf.Abs((Input.GetTouch(0).position.y / Screen.height * 32) - ore.transform.position.y); }

    // Spawn ore with a random scale and position
    public void OreInstantiate()
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
    public void PickaxeInstantiate()
    {
        pickaxe = Instantiate(
        PickaxePrefabs[CurrentPickLevel()],
        new Vector2(14, 7), // Change later the position of new ore spawned to be posibly random
        Quaternion.identity) as GameObject;
        print("pickaxe = Instantiate");
    }

    // Shows damage numbers on screen
    public void ShowDamageText()
    {
        dmgtxt = Instantiate(
        DamageText,
        new Vector2(ore.transform.position.x + RandomXOffset(), ore.transform.position.y + RandomYOffset()),
        Quaternion.identity,
        transform);

        dmgtxt.GetComponent<TextMesh>().text = Math.Round(pickStats[gameSession.GetPickLevel()].GetPickaxeDamage() * RandomDamageMultiplier() * (float)Math.Pow(UpgradeToolMultiplier, gameSession.GetPickUpgradeCounter()), 1).ToString();
    }

    // This coroutine destroys ore with 0 health, resets heaslth, and after 1 second spawns new ore. Also during 1 second period pickaxe damage is reset to 0.
    IEnumerator DestroyAndSpawn()
    {
        canHit = false;
        Destroy(ore);
        CurrentHealth = oreStats.GetOreHealth();

        // Wait time before new ore spawns, so that it can't be damaged while it hasn't spawned
        CurrentPickaxeDamage = 0;
        yield return new WaitForSeconds(RandomSpawnTime());

        // Initializes pick damage after it has been reduced to 0
        CurrentPickaxeDamage = pickStats[gameSession.GetPickLevel()].GetPickaxeDamage() * (float)Math.Pow(UpgradeToolMultiplier, gameSession.GetPickUpgradeCounter());

        OreInstantiate();
        canHit = true;
    }

    // Animates tool on touch
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

            if (!canRotate && pickaxe.transform.rotation.eulerAngles.z >= 0)
            {
                pickaxe.transform.Rotate(Vector3.back * (RotationSpeed * Time.deltaTime));
                if (pickaxe.transform.rotation.eulerAngles.z <= 0 || pickaxe.transform.rotation.eulerAngles.z >= 180)
                {
                    pickaxe.transform.eulerAngles = new Vector3(0, 0, 0);
                    canRotate = true;
                    canAnimate = false;
                }
            }
        }
    }

    public int CurrentPickLevel() { return gameSession.GetPickLevel(); }
    public int RandomPX() { return UnityEngine.Random.Range(4, 14); }
    public int RandomPY() { return UnityEngine.Random.Range(6, 20); }
    public float RandomScale() { return UnityEngine.Random.Range(-0.33f, 0.33f); }
    public int RandomRotation() { return UnityEngine.Random.Range(0, 360); }
    public float RandomSpawnTime() { return UnityEngine.Random.Range(0.25f, 0.75f); }
    public int RandomXOffset() { return UnityEngine.Random.Range(-1, 3); }
    public int RandomYOffset() { return UnityEngine.Random.Range(4, 8); }
    public float RandomDamageMultiplier() { return UnityEngine.Random.Range(0.5f, 1.5f); }
}
