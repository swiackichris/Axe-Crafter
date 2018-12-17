using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    [SerializeField] Image healthBar;
    static public float health;

    [SerializeField] AxeStats axeStatsScriptPrefab;
    [SerializeField] MobStats mobStatsScriptPrefab;

    [SerializeField] private GameSession gameSessionScriptPrefab;

    private void Start(MobStats mobStats)
    {
        health = mobStats.GetMaxHealth();
        // health = mobStatsScriptPrefab.GetMaxHealth();
    }

    public void UpdateHealth()
    {
        health -= axeStatsScriptPrefab.GetAxeDamage();
        healthBar.fillAmount = health / mobStatsScriptPrefab.GetMaxHealth();
        if(health <= 0)
        {
            if(health <= -10)
            {
                gameSessionScriptPrefab.CountGold();
                health = mobStatsScriptPrefab.GetMaxHealth();
                healthBar.fillAmount = health / mobStatsScriptPrefab.GetMaxHealth();
            }
        }
    }
}
