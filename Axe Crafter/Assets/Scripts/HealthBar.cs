using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    [SerializeField] Image healthBar;                                       // Health bar to appear above the monster in battle scene
    // static public float CurrentHealth;                                   // Doesn't work with Int
    [SerializeField] public float CurrentHealth;

    [SerializeField] AxeStats axeStatsScriptPrefab;
    [SerializeField] MobStats mobStatsScriptPrefab;

    [SerializeField] private GameSession gameSessionScriptPrefab;

    [SerializeField] GameObject MobPrefab;                                  // Mob prefab to be instatiated and mined
    GameObject mob;                                                         // Required for to Respawn a mob after It has been killed

    bool CanAttack = true;

    private void Start()
    {
        // CurrentHealth initialisation
        CurrentHealth = mobStatsScriptPrefab.GetMaxHealth();
        MobInstatiate();
    }

    public void UpdateHealth()
    {
        Attack();
        if (CurrentHealth <= -10)
        {
            print("CurrentHealth <= -10"); // TODO Delete this
            EarnGoldAndReset();
        }
    }

    private void EarnGoldAndReset()
    {
        print("EarnGoldAndReset() Started"); // TODO Delete this

        // Adds gold after killing a monster
        gameSessionScriptPrefab.CountGold();

        // Resets health after monster has died so a new monster is at full health
        CurrentHealth = mobStatsScriptPrefab.GetMaxHealth();

        // Updates health amount on healthbar sprite to full health
        healthBar.fillAmount = CurrentHealth / mobStatsScriptPrefab.GetMaxHealth();

        StartCoroutine(DestroyAndSpawn());
    }

    private void Attack()
    {
        if(CanAttack)
        {
            // Updates health
            CurrentHealth -= axeStatsScriptPrefab.GetAxeDamage();

            // Updates health amount on healthbar sprite
            healthBar.fillAmount = CurrentHealth / mobStatsScriptPrefab.GetMaxHealth();
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
}
