using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobStats : MonoBehaviour {

    [SerializeField] int MaxHealth = 100;
    [SerializeField] int GoldReward = 10;
    [SerializeField] AudioClip[] MobSound = null;       
    int MobSoundArrayLength;

    public void Start()
    {
        MobSoundArrayLength = MobSound.Length;
    }

    public int GetMaxHealth() { return MaxHealth; }
    public int GetGoldReward() { return GoldReward; }
    public AudioClip GetMobSound(int i) { return MobSound[i]; }
    public int GetMobSoundArrayLength() { return MobSoundArrayLength; }
}

