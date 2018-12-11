﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    [SerializeField] Image healthBar;
    static public float health;

    [SerializeField] AxeStats axeStatsScriptPrefab;
    [SerializeField] MobStats mobStatsScriptPrefab;

    private void Start()
    {
        health = mobStatsScriptPrefab.GetMaxHealth();
    }

    public void UpdateHealth()
    {
        health -= axeStatsScriptPrefab.GetAxeDamage();
        healthBar.fillAmount = health / mobStatsScriptPrefab.GetMaxHealth();
    }
}
