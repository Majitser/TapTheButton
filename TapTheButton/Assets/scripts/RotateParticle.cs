﻿using UnityEngine;
using System.Collections;

public class RotateParticle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate (Vector3.forward, Time.deltaTime * 50);
	}
}
