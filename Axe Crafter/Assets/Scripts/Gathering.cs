﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gathering : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void MovePickaxe()
    {
        transform.Rotate(Vector3.forward * -90);
    }

    void MoveAxe()
    {

    }
}
