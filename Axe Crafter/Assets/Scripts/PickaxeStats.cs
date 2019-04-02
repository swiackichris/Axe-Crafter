using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeStats : MonoBehaviour {

    [SerializeField] int PickaxeDamage = 10;
    [SerializeField] AudioClip[] PickaxeSound;          // Audio clip to be played when used
    // [SerializeField] [Range(0, 1)] float PickSoundVolume = 0.05f;
    int PickSoundArrayLength;

    public void Start()
    {
        PickSoundArrayLength = PickaxeSound.Length;
    }

    // Returns PickaxeDamage
    public int GetPickaxeDamage() { return PickaxeDamage; }

    // Returns PickaxeSound
    public AudioClip GetPickaxeSound(int i) { return PickaxeSound[i]; }

    public int GetPickSoundArrayLength() { return PickSoundArrayLength; }
}
