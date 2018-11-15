using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreStats : MonoBehaviour {

    [SerializeField] int OreHealth;

    public int GetOreHealth()
    {
        return OreHealth;
    }  
}
