using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeStats : MonoBehaviour {

    [SerializeField] int AxeDamage = 10;            // Damage that each click of axe does to a monster or a tree

    // Returns AxeDamage
    public int GetAxeDamage() { return AxeDamage; }
}
