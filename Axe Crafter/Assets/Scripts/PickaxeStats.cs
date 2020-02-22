using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeStats : MonoBehaviour {

    [SerializeField] int PickaxeDamage = 10;
    [SerializeField] AudioClip[] PickaxeSound = null;
    int PickSoundArrayLength;

    public void Start()
    {
        PickSoundArrayLength = PickaxeSound.Length;
    }

    public int GetPickaxeDamage() { return PickaxeDamage; }
    public AudioClip GetPickaxeSound(int i) { return PickaxeSound[i]; }
    public int GetPickSoundArrayLength() { return PickSoundArrayLength; }
}
