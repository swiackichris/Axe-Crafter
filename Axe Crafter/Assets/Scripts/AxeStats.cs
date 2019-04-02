using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeStats : MonoBehaviour {

    [SerializeField] int AxeDamage = 10;            // Damage that each click of axe does to a monster or a tree
    [SerializeField] AudioClip[] AxeSound;          // Audio clip to be played when used
    [SerializeField] AudioClip[] MobSound;
    // [SerializeField] [Range(0, 1)] float AxeSoundVolume = 0.05f;
    int AxeSoundArrayLength;

    public void Start()
    {
        AxeSoundArrayLength = AxeSound.Length;
    }

    // Returns AxeDamage
    public int GetAxeDamage() { return AxeDamage; }

    // Returns AxeSound
    public AudioClip GetAxeSound(int i) { return AxeSound[i]; }

    public AudioClip GetMobSound(int i) { return MobSound[i]; }

    public int GetAxeSoundArrayLength() { return AxeSoundArrayLength; }
}
