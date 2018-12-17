using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreStats : MonoBehaviour {

    [SerializeField] int OreHealth = 10;

    public int GetOreHealth()
    {
        return OreHealth;
    }
}
