using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreStats : MonoBehaviour {

    [SerializeField] float OreHealth = 10;

    public float GetOreHealth()
    {
        return OreHealth;
    }
}
