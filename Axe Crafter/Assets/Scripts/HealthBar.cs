using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    [SerializeField] Image healthBar;                                       // Health bar to appear above the monster in battle scene
    // static public float CurrentHealth;                                   // Doesn't work with Int
    [SerializeField] public float CurrentHealth;
    [SerializeField] int CurrentAxe;
    [SerializeField] AxeStats[] axeStats;
    [SerializeField] MobStats mobStats;
    [SerializeField] GameObject MobPrefab;                                  // Mob prefab to be instatiated and mined
    GameObject mob;                                                         // Required for to Respawn a mob after It has been killed

    [SerializeField] private GameSession gameSession;
    [SerializeField] [Range(0, 1)] float AxeSoundVolume = 0.05f;

    bool CanAttack = true;

    private void Start()
    {
        // CurrentHealth initialisation
        CurrentHealth = mobStats.GetMaxHealth();
        MobInstatiate();
        CurrentAxe = CurrentAxeLevel();
    }

    public void UpdateHealth()
    {
        // Plays random mob sound
        AudioSource.PlayClipAtPoint(axeStats[CurrentAxeLevel()].GetMobSound
            (UnityEngine.Random.Range(0, 5)), Camera.main.transform.position, AxeSoundVolume);

        Attack();
        if (CurrentHealth <= -10)
        {
            print("CurrentHealth <= -10"); // TODO Delete this
            EarnGoldAndReset();
        }
    }

    private void EarnGoldAndReset()
    {
        // Adds gold after killing a monster
        gameSession.CountGold();

        // Resets health after monster has died so a new monster is at full health
        CurrentHealth = mobStats.GetMaxHealth();

        // Updates health amount on healthbar sprite to full health
        healthBar.fillAmount = CurrentHealth / mobStats.GetMaxHealth();

        StartCoroutine(DestroyAndSpawn());
    }

    private void Attack()
    {
        if(CanAttack)
        {
            // Updates health
            CurrentHealth -= axeStats[CurrentAxe].GetAxeDamage();

            // Updates health amount on healthbar sprite
            healthBar.fillAmount = CurrentHealth / mobStats.GetMaxHealth();
        }
    }

    // Spawn ore at a position
    public void MobInstatiate()
    {
        mob = Instantiate(
        MobPrefab,
        new Vector2(9, 9), // Change later the position of new ore spawned to be posibly random
        Quaternion.identity) as GameObject;
        print("ore = Instantiate");
    }

    IEnumerator DestroyAndSpawn()
    {
        print("DestroyAndSpawn() Started"); // TODO Delete this

        // Destroys ore created in void Start();
        Destroy(mob);
        print("Destroy(mob)");

        // Code so you can't attack before new monster has spawned
        CanAttack = false;

        // TODO you could randomize it in the future
        yield return new WaitForSeconds(1);

        CanAttack = true;

        // Spawns ore
        MobInstatiate();
    }

    public int CurrentAxeLevel()
    {
        return gameSession.GetAxeLevel();
    }
}
