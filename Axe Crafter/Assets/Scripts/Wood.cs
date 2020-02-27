using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wood : MonoBehaviour {

    [SerializeField] private WoodStats woodStats = null;                           
    [SerializeField] GameObject WoodPrefab = null;                                 
    [SerializeField] private GameSession gameSession = null;
    [SerializeField] private AxeStats[] axeStats = null;                           
    [SerializeField] GameObject[] AxePrefabs = null;                               
    [SerializeField] ParticleSystem ChoppingParticle = null;
    [SerializeField] TextMeshPro DamageText = null;
    GameObject wood;                                                        
    GameObject axe;

    [SerializeField] [Range(0, 1)] float AxeSoundVolume = 1f;

    private float CurrentHealth;                                                                               

    bool canHit = true;                                                     
    bool canAnimate = false;
    bool canRotate = true;

    private readonly float RotationSpeed = 200f;
    private readonly float UpgradeToolMultiplier = 1.05f;
    private float DamageAmount = 0;

    private void Start()
    {
        CurrentHealth = woodStats.GetWoodHealth();
        WoodInstantiate();
        AxeInstantiate();
    }

    private void Update()
    {
        MoveToolOnTouch();
        ToolAnimation();
    }

    // Moves currently used tool to touch position
    private void MoveToolOnTouch()
    {
        if (Input.touchCount > 0) { axe.transform.position = new Vector3(Input.GetTouch(0).position.x / Screen.width * 18, Input.GetTouch(0).position.y / Screen.height * 32, 0); }
    }

    // Each function is attached to axe in different scene/mine, so that it is possible to count and save the amount of different types of ores mined.
    public void ChopWood(int i)
    {
        canAnimate = true;

        // Checks if position close to a monster is clicked
        if (HitCheckX() <= 5 && HitCheckY() <= 5) { Hit(); }

        if (CurrentHealth <= -1)
        {
            // If wood is mined, add it
            gameSession.CountChoppedWood(i);
            StartCoroutine(DestroyAndSpawn());
            CurrentHealth = woodStats.GetWoodHealth();
        }
    }

    private void Hit()
    {
        if (canHit)
        {
            // Deducts wood health
            DamageAmount = axeStats[gameSession.GetAxeLevel()].GetAxeDamage() * (float)Math.Pow(UpgradeToolMultiplier, gameSession.GetAxeUpgradeCounter()) * RandomDamageMultiplier();
            CurrentHealth -= DamageAmount;

            // Plays a particle effect
            Instantiate(ChoppingParticle, wood.transform.position, Quaternion.identity);

            // Plays random axe sound
            AudioSource.PlayClipAtPoint(axeStats[CurrentAxeLevel()].GetAxeSound
                (UnityEngine.Random.Range(0, 5)), Camera.main.transform.position, AxeSoundVolume);

            // Shows damage numbers
            ShowDamageText();
        }
    }

    private float HitCheckX() { return Mathf.Abs((Input.GetTouch(0).position.x / Screen.width * 18) - wood.transform.position.x); }
    private float HitCheckY() { return Mathf.Abs((Input.GetTouch(0).position.y / Screen.height * 32) - wood.transform.position.y); }

    // Spawn wood at a position
    public void WoodInstantiate()
    {
        wood = Instantiate(
        WoodPrefab,
        new Vector2(RandomPX(), RandomPY()),
        Quaternion.identity) as GameObject;
        wood.transform.localScale += new Vector3(RandomScale(), RandomScale(), 0);
    }

    // Spawn currently owned axe at a position
    public void AxeInstantiate()
    {
        axe = Instantiate(
        AxePrefabs[CurrentAxeLevel()],
        new Vector2(14, 7),
        Quaternion.identity) as GameObject;
    }

    // Shows damage numbers on screen
    public void ShowDamageText()
    {
        DamageText.text = Math.Round(DamageAmount, 1).ToString();

        Instantiate(
        DamageText,
        new Vector2(wood.transform.position.x + RandomXOffset(), wood.transform.position.y + RandomYOffset()),
        Quaternion.identity,
        GameObject.FindGameObjectWithTag("Canvas").transform);
    }

    // This coroutine destroys wood with 0 health, resets health, and after 1 second spawns new wood. Also during 1 second period axe damage is reset to 0.
    IEnumerator DestroyAndSpawn()
    {
        canHit = false;
        Destroy(wood);
        yield return new WaitForSeconds(RandomSpawnTime());
        WoodInstantiate();
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

    public int CurrentAxeLevel() { return gameSession.GetAxeLevel(); }
    public int RandomPX() { return UnityEngine.Random.Range(6, 14); }
    public int RandomPY() { return UnityEngine.Random.Range(8, 16); }
    public float RandomScale() { return UnityEngine.Random.Range(-0.25f, 0.25f); }
    public int RandomRotation() { return UnityEngine.Random.Range(0, 360); }
    public float RandomSpawnTime() { return UnityEngine.Random.Range(0.25f, 0.75f); }
    public int RandomXOffset() { return UnityEngine.Random.Range(-1, 3); }
    public int RandomYOffset() { return UnityEngine.Random.Range(4, 8); }
    public float RandomDamageMultiplier() { return UnityEngine.Random.Range(0.5f, 1.5f); }

}
