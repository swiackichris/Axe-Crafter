using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    [SerializeField] Image healthBar;
    static public float health; // Doesn't work with Int

    [SerializeField] AxeStats axeStatsScriptPrefab;
    [SerializeField] MobStats mobStatsScriptPrefab;

    [SerializeField] private GameSession gameSessionScriptPrefab;

    private void Start()
    {
        health = mobStatsScriptPrefab.GetMaxHealth();
    }

    public void UpdateHealth()
    {
        Attack();
        if (health <= -10)
        {
            EarnGoldAndReset();
        }
    }

    private void EarnGoldAndReset()
    {
        gameSessionScriptPrefab.CountGold();
        health = mobStatsScriptPrefab.GetMaxHealth();
        healthBar.fillAmount = health / mobStatsScriptPrefab.GetMaxHealth();
    }

    private void Attack()
    {
        health -= axeStatsScriptPrefab.GetAxeDamage();
        healthBar.fillAmount = health / mobStatsScriptPrefab.GetMaxHealth();
    }
}
