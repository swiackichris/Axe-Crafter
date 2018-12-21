using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    [SerializeField] Image healthBar;                                       // Health bar to appear above the monster in battle scene
    static public float CurrentHealth;                                      // Doesn't work with Int

    [SerializeField] AxeStats axeStatsScriptPrefab;
    [SerializeField] MobStats mobStatsScriptPrefab;

    [SerializeField] private GameSession gameSessionScriptPrefab;           //   

    private void Start()
    {
        // CurrentHealth initialisation
        CurrentHealth = mobStatsScriptPrefab.GetMaxHealth();
    }

    public void UpdateHealth()
    {
        Attack();
        if (CurrentHealth <= -10)
        {
            EarnGoldAndReset();
        }
    }

    private void EarnGoldAndReset()
    {
        // Adds gold after killing a monster
        gameSessionScriptPrefab.CountGold();

        // Resets health after monster has died so a new monster is at full health
        CurrentHealth = mobStatsScriptPrefab.GetMaxHealth();

        // Updates health amount on healthbar sprite to full health
        healthBar.fillAmount = CurrentHealth / mobStatsScriptPrefab.GetMaxHealth();
    }

    private void Attack()
    {
        // Updates health
        CurrentHealth -= axeStatsScriptPrefab.GetAxeDamage();

        // Updates health amount on healthbar sprite
        healthBar.fillAmount = CurrentHealth / mobStatsScriptPrefab.GetMaxHealth();
    }
}
