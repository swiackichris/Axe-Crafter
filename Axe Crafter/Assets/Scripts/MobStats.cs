using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobStats : MonoBehaviour {

    [SerializeField] int MaxHealth = 100;
    [SerializeField] int GoldReward = 10;
    [SerializeField] AudioClip[] MobSound;          // Audio clip to be played when used
    // [SerializeField] [Range(0, 1)] float MobSoundVolume = 0.05f;
    int MobSoundArrayLength;

    public void Start()
    {
        MobSoundArrayLength = MobSound.Length;
    }

    // Returns MaxHealth
    public int GetMaxHealth() { return MaxHealth; }

    // Returns GoldReward
    public int GetGoldReward() { return GoldReward; }

    // Returns AxeSound
    public AudioClip GetMobSound(int i) { return MobSound[i]; }

    public int GetMobSoundArrayLength() { return MobSoundArrayLength; }
}

