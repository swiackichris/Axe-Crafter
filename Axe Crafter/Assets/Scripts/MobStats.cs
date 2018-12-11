using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobStats : MonoBehaviour {

    // Health
    [SerializeField] float MaxHealth = 100f;

    // Loot
    [SerializeField] float GoldReward = 10f;

    public float GetMaxHealth()
    {
        return MaxHealth;
    }
}

