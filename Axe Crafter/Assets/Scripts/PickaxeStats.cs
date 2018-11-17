using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeStats : MonoBehaviour {

    [SerializeField] float PickaxeDamage = 10f;

    public float GetPickaxeDamage()
    {
        return PickaxeDamage;
    }
}
