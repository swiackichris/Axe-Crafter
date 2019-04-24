using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour {

    [SerializeField] private WoodStats woodStats;                           // Should be the same as WoodPrefab
    [SerializeField] GameObject WoodPrefab;                                 // Wood prefab to be instatiated and chopped
    [SerializeField] private GameSession gameSession;
    [SerializeField] private AxeStats[] axeStats;                           // Should be the same as AxePrefab TODO these could be deleted
    [SerializeField] GameObject[] AxePrefabs;                               // Axe prefab to be instatiated and chopped with
    [SerializeField] ParticleSystem ChoppingParticle;
    GameObject wood;                                                        // Required for Button to know which object should be destroyed.
    GameObject axe;

    [SerializeField] [Range(0, 1)] float AxeSoundVolume = 1f;

    private float CurrentHealth;                                              // How much health currently chopped wood has
    private float CurrentAxeDamage;                                           // How much damage we apply to currently chopped wood

    bool canHit = true;                                                    // required to stop mining when ore is depleted
    bool canAnimate = false;
    bool canRotate = true;

    private float RotationSpeed = 219f;
    private float UpgradeToolMultiplier = 1.05f;

    private void Start()
    {
        CurrentHealth = woodStats.GetWoodHealth();

        // TODO Could possibly add Math.Round to round the number
        CurrentAxeDamage = axeStats[gameSession.GetAxeLevel()].GetAxeDamage() * (float)Math.Pow(UpgradeToolMultiplier, gameSession.GetAxeUpgradeCounter());

        WoodInstantiate();
        AxeInstantiate();
    }

    private void Update()
    {
        ToolAnimation();
    }

    // Each function is attached to axe in different scene/mine, so that it is possible to count and save the amount of different types of ores mined.
    public void ChopWood(int i)
    {
        canAnimate = true;

        Hit();

        if (CurrentHealth <= 0)
        {
            // If wood is mined, add it
            gameSession.CountMinedOre(i);
            StartCoroutine(DestroyAndSpawn());
        }
    }

    private void Hit()
    {
        if (canHit)
        {
            // Deducts wood health
            CurrentHealth -= CurrentAxeDamage;

            // Plays a particle effect
            Instantiate(ChoppingParticle, wood.transform.position, Quaternion.identity);

            // Plays random axe sound
            AudioSource.PlayClipAtPoint(axeStats[CurrentAxeLevel()].GetAxeSound
                (UnityEngine.Random.Range(0, 5)), Camera.main.transform.position, AxeSoundVolume);
        }
    }

    // Spawn wood at a position
    public void WoodInstantiate()
    {
        wood = Instantiate(
        WoodPrefab,
        new Vector2(RandomPX(), RandomPY()), // Change later the position of new wood spawned to be posibly random
        Quaternion.identity) as GameObject;
        print("wood = Instantiate");

        wood.transform.localScale += new Vector3(RandomScale(), RandomScale(), 0);
    }

    // Spawn currently owned axe at a position
    public void AxeInstantiate()
    {
        axe = Instantiate(
        AxePrefabs[CurrentAxeLevel()],
        new Vector2(14, 7), // Change later the position of new wood spawned to be posibly random
        Quaternion.identity) as GameObject;
        print("axe = Instantiate");
    }

    // This coroutine destroys wood with 0 health, resets health, and after 1 second spawns new wood. Also during 1 second period axe damage is reset to 0.
    IEnumerator DestroyAndSpawn()
    {
        canHit = false;

        // Destroys wood created in void Start();
        Destroy(wood);

        // Resets health for new wood
        CurrentHealth = woodStats.GetWoodHealth();

        // Wait time before new wood spawns, so that it can't be damaged while it hasn't spawned
        CurrentAxeDamage = 0;
        yield return new WaitForSeconds(RandomSpawnTime());

        // Initializes axe damage after it has been reduced to 0
        CurrentAxeDamage = axeStats[gameSession.GetAxeLevel()].GetAxeDamage() * (float)Math.Pow(UpgradeToolMultiplier, gameSession.GetAxeUpgradeCounter());

        // Spawns wood
        WoodInstantiate();

        canHit = true;
    }

    private void ToolAnimation()
    {
        if (canAnimate)
        {
            if (canRotate && axe.transform.rotation.eulerAngles.z <= 45)
            {
                axe.transform.Rotate(Vector3.forward * (RotationSpeed * Time.deltaTime));
                if (axe.transform.rotation.eulerAngles.z >= 45)
                {
                    canRotate = false;
                }
            }

            if (!canRotate && axe.transform.rotation.eulerAngles.z >= 1)
            {
                axe.transform.Rotate(Vector3.back * (RotationSpeed * Time.deltaTime));
                if (axe.transform.rotation.eulerAngles.z <= 1)
                {
                    canRotate = true;
                    canAnimate = false;
                }
            }
        }
    }

    public int CurrentAxeLevel() { return gameSession.GetAxeLevel(); }
    public int RandomPX() { return UnityEngine.Random.Range(4, 14); }
    public int RandomPY() { return UnityEngine.Random.Range(8, 16); }
    public float RandomScale() { return UnityEngine.Random.Range(-0.25f, 0.25f); }
    public int RandomRotation() { return UnityEngine.Random.Range(0, 360); }
    public float RandomSpawnTime() { return UnityEngine.Random.Range(0.25f, 0.75f); }

}
