using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeStats : MonoBehaviour {

    [SerializeField] int AxeDamage = 10;

    public int GetAxeDamage()
    {
        return AxeDamage;
    }
}
