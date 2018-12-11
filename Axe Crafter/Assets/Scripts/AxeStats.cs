using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeStats : MonoBehaviour {

    [SerializeField] float AxeDamage = 10f;

    public float GetAxeDamage()
    {
        return AxeDamage;
    }
}
