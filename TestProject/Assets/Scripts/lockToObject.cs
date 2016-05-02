using UnityEngine;
using System.Collections;

public class lockToObject : MonoBehaviour {

    //the thing we want to attach too
    public GameObject target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = target.transform.position;
	
	}
}
