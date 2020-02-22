using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeStats : MonoBehaviour {

    [SerializeField] int AxeDamage = 10;
    [SerializeField] AudioClip[] AxeSound = null;        
    [SerializeField] AudioClip[] MobSound = null;
    int AxeSoundArrayLength;

    public void Start()
    {
        AxeSoundArrayLength = AxeSound.Length;
    }

    public int GetAxeDamage() { return AxeDamage; }
    public AudioClip GetAxeSound(int i) { return AxeSound[i]; }
    public AudioClip GetMobSound(int i) { return MobSound[i]; }
    public int GetAxeSoundArrayLength() { return AxeSoundArrayLength; }
}
