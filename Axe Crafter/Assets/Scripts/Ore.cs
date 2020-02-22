using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ore : MonoBehaviour {

    [SerializeField] private OreStats oreStats = null;                 
    [SerializeField] GameObject OrePrefab = null;                                  
    [SerializeField] private GameSession gameSession = null;
    [SerializeField] private PickaxeStats[] pickStats = null;                      
    [SerializeField] GameObject [] PickaxePrefabs = null;                          
    [SerializeField] ParticleSystem PickingParticle = null;
    [SerializeField] Transform DamageText = null;
    GameObject ore;                                                         
    GameObject pickaxe;

    [SerializeField] [Range(0, 1)] float PickSoundVolume = 1f;

    private float CurrentHealth;                                                                            

    bool canHit = true;                                                     
    bool canAnimate = false;
    bool canRotate = true;

    private readonly float RotationSpeed = 200f;
    private readonly float UpgradeToolMultiplier = 1.05f;
    private float DamageAmount = 0;


    private void Start()
    {
        CurrentHealth = oreStats.GetOreHealth();
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

        // Checks if position close to an object is clicked
        if (HitCheckX() <= 5 && HitCheckY() <= 5) { Hit(); }

        if (CurrentHealth <= 0)
        {
            // If ore is mined, add it
            gameSession.CountMinedOre(i);
            StartCoroutine(DestroyAndSpawn());
            CurrentHealth = oreStats.GetOreHealth();
        }
    }

    private void Hit()
    {
        if (canHit)
        {
            // Deduct ore health
            DamageAmount = pickStats[gameSession.GetPickLevel()].GetPickaxeDamage() * (float)Math.Pow(UpgradeToolMultiplier, gameSession.GetPickUpgradeCounter()) * RandomDamageMultiplier();
            CurrentHealth -= DamageAmount;

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
        new Vector2(RandomPX(), RandomPY()),
        Quaternion.identity) as GameObject;
        ore.transform.localScale += new Vector3(RandomScale(), RandomScale(), 0);
        ore.transform.Rotate(0, 0, RandomRotation());
    }

    // Spawn currently owned pickaxe
    public void PickaxeInstantiate()
    {
        pickaxe = Instantiate(
        PickaxePrefabs[CurrentPickLevel()],
        new Vector2(14, 7),
        Quaternion.identity) as GameObject;
    }

    // Shows damage numbers on screen
    public void ShowDamageText()
    {
        DamageText.GetComponent<TextMeshPro>().SetText(Math.Round(DamageAmount, 1).ToString());

        Instantiate(
        DamageText,
        new Vector2(ore.transform.position.x + RandomXOffset(), ore.transform.position.y + RandomYOffset()),
        Quaternion.identity,
        transform);
    }

    // This coroutine destroys ore with 0 health, resets heaslth, and after 1 second spawns new ore. Also during 1 second period pickaxe damage is reset to 0.
    IEnumerator DestroyAndSpawn()
    {
        canHit = false;
        Destroy(ore);
        yield return new WaitForSeconds(RandomSpawnTime());
        OreInstantiate();
        canHit = true;
    }

    // Animates tool on touch
    private void ToolAnimation()
    {
        if (canAnimate)
        {
            if (canRotate)
            {
                GameObject.FindGameObjectWithTag("ToolRotator").transform.Rotate(Vector3.forward * (RotationSpeed * Time.deltaTime));
                if (GameObject.FindGameObjectWithTag("ToolRotator").transform.rotation.eulerAngles.z > 45) { canRotate = false; }
            }
            else
            {
                GameObject.FindGameObjectWithTag("ToolRotator").transform.Rotate(Vector3.back * (RotationSpeed * Time.deltaTime));
                if (GameObject.FindGameObjectWithTag("ToolRotator").transform.rotation.eulerAngles.z < 15)
                {
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
