using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour {

    [SerializeField] Image healthBar;                                       // Health bar to appear above the monster in battle scene
    [SerializeField] public float CurrentHealth;
    [SerializeField] int CurrentAxe;
    [SerializeField] AxeStats[] axeStats;
    [SerializeField] GameObject[] AxePrefabs;                               // Axe prefab to be instatiated and chopped with
    [SerializeField] MobStats mobStats;
    [SerializeField] GameObject MobPrefab;                                  // Mob prefab to be instatiated and mined
    [SerializeField] ParticleSystem BloodParticle;
    [SerializeField] ParticleSystem DeadParticle;
    [SerializeField] GameObject DamageText;
    GameObject mob;                                                         // Required for to Respawn a mob after It has been killed
    GameObject axe;
    GameObject dmgtxt;

    [SerializeField] private GameSession gameSession;
    [SerializeField] [Range(0, 1)] float AxeSoundVolume = 1f;

    bool canHit = true;                                                     // Required to stop mining when ore is depleted
    bool canAnimate = false;
    bool canRotate = true;

    private float RotationSpeed = 200f;
    private float UpgradeToolMultiplier = 1.05f;

    private void Start()
    {
        CurrentHealth = mobStats.GetMaxHealth();
        MobInstatiate();
        CurrentAxe = CurrentAxeLevel();
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

    public void Attack(int i)
    {
        canAnimate = true;

        // Checks if position close to a monster is clicked
        if (HitCheckX() <= 5 && HitCheckY() <= 5) { Hit(); }

        if (CurrentHealth <= -1) { EarnGoldAndReset(i); }
    }

    private void EarnGoldAndReset(int i)
    {
        // Adds gold after killing a monster
        gameSession.CountGold(i);

        // Resets health after monster has died so a new monster is at full health
        CurrentHealth = mobStats.GetMaxHealth();

        // Updates health amount on healthbar sprite to full health
        healthBar.fillAmount = CurrentHealth / mobStats.GetMaxHealth();

        // Plays a particle effect
        Instantiate(DeadParticle, mob.transform.position, Quaternion.identity);

        // Destroys mob prefab, waits and instantiates it again
        StartCoroutine(DestroyAndSpawn());
    }

    private void Hit()
    {
        if (canHit)
        {
            // Updates health
            CurrentHealth -= axeStats[gameSession.GetAxeLevel()].GetAxeDamage() * (float)Math.Pow(UpgradeToolMultiplier, gameSession.GetAxeUpgradeCounter()) * RandomDamageMultiplier();

            // Updates health amount on healthbar sprite
            healthBar.fillAmount = CurrentHealth / mobStats.GetMaxHealth();

            // Plays a particle effect
            Instantiate(BloodParticle, mob.transform.position, Quaternion.identity);

            // Plays random mob sound
            AudioSource.PlayClipAtPoint(axeStats[CurrentAxeLevel()].GetMobSound
                (UnityEngine.Random.Range(0, 5)), Camera.main.transform.position, AxeSoundVolume);

            // Shows damage numbers
            ShowDamageText();
        }
    }

    private float HitCheckX() { return Mathf.Abs((Input.GetTouch(0).position.x / Screen.width * 18) - mob.transform.position.x); }
    private float HitCheckY() { return Mathf.Abs((Input.GetTouch(0).position.y / Screen.height * 32) - mob.transform.position.y); }

    // Spawn ore at a position
    public void MobInstatiate()
    {
        mob = Instantiate(
        MobPrefab,
        new Vector2(RandomPX(), RandomPY()), // Change later the position of new ore spawned to be posibly random
        Quaternion.identity) as GameObject;
        print("mob = Instantiate");

        mob.transform.localScale += new Vector3(RandomScale(), RandomScale(), 0);
    }

    IEnumerator DestroyAndSpawn()
    {
        canHit = false;
        Destroy(mob);
        yield return new WaitForSeconds(RandomSpawnTime());
        MobInstatiate();
        canHit = true;
    }

    // Spawns currently owned axe at a position
    public void AxeInstantiate()
    {
        axe = Instantiate(
        AxePrefabs[CurrentAxeLevel()],
        new Vector2(14, 7),
        Quaternion.identity) as GameObject;
        print("axe = Instantiate");
    }

    // Shows damage numbers on screen
    public void ShowDamageText()
    {
        dmgtxt = Instantiate(
        DamageText,
        new Vector2 (mob.transform.position.x + RandomXOffset(), mob.transform.position.y + RandomYOffset()),
        Quaternion.identity,
        transform);

        dmgtxt.GetComponent<TextMesh>().text = Math.Round(axeStats[gameSession.GetAxeLevel()].GetAxeDamage() * RandomDamageMultiplier() * (float)Math.Pow(UpgradeToolMultiplier, gameSession.GetAxeUpgradeCounter()), 1).ToString();
    }

    // Animates tool on touch
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

            else if (!canRotate && axe.transform.rotation.eulerAngles.z >= 0)
            {
                axe.transform.Rotate(Vector3.back * (RotationSpeed * Time.deltaTime));
                if (axe.transform.rotation.eulerAngles.z <= 0 || axe.transform.rotation.eulerAngles.z >= 180)
                {
                    axe.transform.eulerAngles = new Vector3(0, 0, 0);
                    canRotate = true;
                    canAnimate = false;
                }
            }
        }
    }

    public int CurrentAxeLevel() { return gameSession.GetAxeLevel(); }
    public float RandomSpawnTime() { return UnityEngine.Random.Range(0.25f, 0.75f); }
    public int RandomPX() { return UnityEngine.Random.Range(7, 12); }
    public int RandomPY() { return UnityEngine.Random.Range(7, 12); }
    public float RandomScale() { return UnityEngine.Random.Range(-0.2f, 0.2f); }
    public int RandomXOffset() { return UnityEngine.Random.Range(-1, 3); }
    public int RandomYOffset() { return UnityEngine.Random.Range(4, 8); }
    public float RandomDamageMultiplier() { return UnityEngine.Random.Range(0.5f, 1.5f); }
}
