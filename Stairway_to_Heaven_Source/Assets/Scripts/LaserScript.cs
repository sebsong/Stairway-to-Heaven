﻿using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Enemy") {
			other.gameObject.SetActive (false);
		}
	}
}
