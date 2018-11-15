using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeStats : MonoBehaviour {

    [SerializeField] int PickaxeDamage = 10;

    public int GetPickaxeDamage()
    {
        return PickaxeDamage;
    }
}
