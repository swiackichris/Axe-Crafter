using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class UpgradePick : MonoBehaviour {

    [SerializeField] TextMeshProUGUI PickUpgradeText;
    [SerializeField] int PickUpgradeCounter;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpgradePickaxe()
    {
        PickUpgradeCounter += 1;
        PickUpgradeText.text = PickUpgradeCounter.ToString();
        if(PickUpgradeCounter >= 10)
        {
            PickUpgradeCounter = 0;
        }
    }
}
