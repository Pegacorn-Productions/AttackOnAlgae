﻿using UnityEngine;
using System.Collections;

public class SuperSuckerScript : MonoBehaviour {

   

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.transform.name.Equals("acanthophora simulation(Clone)")) {
            Debug.Log("Colliding with " + other.gameObject.transform.ToString());
            //Destroy(other.gameObject);
            ParticleSystem ps = other.gameObject.GetComponent<ParticleSystem>();
            var otherScript = other.GetComponent<AcanthophoraSimulationScript>();
            otherScript.getSuperSucked(true);
        }
        
    }
}