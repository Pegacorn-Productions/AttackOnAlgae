using UnityEngine;
using System.Collections;

public class AcanthophoraSimulationScript : MonoBehaviour {

    private float prevTime;
    private ParticleSystem ps;
    private bool gotSuperSucked = false;

	// Use this for initialization
	void Start () {
        ps = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (gotSuperSucked && (Time.time - prevTime > 1.0f) && (ps.maxParticles > 0)) {
            ps.maxParticles = ps.maxParticles - 50;
            prevTime = Time.time;
        }
	}

    public void getSuperSucked(bool value){
        gotSuperSucked = value;
        Debug.Log("Function call");
    }
}
