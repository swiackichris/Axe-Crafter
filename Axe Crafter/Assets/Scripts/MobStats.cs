using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobStats : MonoBehaviour {

    // Health
    [SerializeField] int MaxHealth = 100;

    // Loot
    [SerializeField] int GoldReward = 10;

    public int GetMaxHealth()
    {
        return MaxHealth;
    }

    public int GetGoldReward()
    {
        return GoldReward;
    }
}

