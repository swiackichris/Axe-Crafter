using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    GameObject mob;                                                         // Required for to Respawn a mob after It has been killed
    GameObject axe;

    [SerializeField] private GameSession gameSession;
    [SerializeField] [Range(0, 1)] float AxeSoundVolume = 1f;

    bool canHit = true;                                                    // required to stop mining when ore is depleted
    bool canAnimate = false;
    bool canRotate = true;

    private float RotationSpeed = 219f;

    private void Start()
    {
        // CurrentHealth initialisation
        CurrentHealth = mobStats.GetMaxHealth();
        MobInstatiate();
        CurrentAxe = CurrentAxeLevel();
        AxeInstantiate();
    }

    private void Update()
    {
        ToolAnimation();
    }

    public void Attack(int i)
    {
        canAnimate = true;

        Hit();

        if (CurrentHealth <= -1)
        {
            EarnGoldAndReset(i);
        }
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

        StartCoroutine(DestroyAndSpawn());
    }

    private void Hit()
    {
        if(canHit)
        {
            // Updates health
            CurrentHealth -= axeStats[CurrentAxe].GetAxeDamage();

            // Updates health amount on healthbar sprite
            healthBar.fillAmount = CurrentHealth / mobStats.GetMaxHealth();

            // Plays a particle effect
            Instantiate(BloodParticle, mob.transform.position, Quaternion.identity);

            // Plays random mob sound
            AudioSource.PlayClipAtPoint(axeStats[CurrentAxeLevel()].GetMobSound
                (UnityEngine.Random.Range(0, 5)), Camera.main.transform.position, AxeSoundVolume);
        }
    }

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

        // Destroys prefab created;
        Destroy(mob);

        // TODO you could randomize it in the future
        yield return new WaitForSeconds(RandomSpawnTime());

        // Spawns ore
        MobInstatiate();

        canHit = true;
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
    public float RandomSpawnTime() { return UnityEngine.Random.Range(0.25f, 0.75f); }
    public int RandomPX() { return UnityEngine.Random.Range(4, 14); }
    public int RandomPY() { return UnityEngine.Random.Range(2, 10); }
    public float RandomScale() { return UnityEngine.Random.Range(-0.25f, 0.25f); }
}
