using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	//check every frame for player input
	//apply that input to every frame as movement

	//will show up in the inspector as an editable property, and since public, can make changes in editor
	public float speed = 10;
    public Vector3 movement;
    //public bool[] clearCoral = {false,false,false,false,false};
    public bool clearCoral1 = false;
	public bool clearCoral2 = false;
	public bool clearCoral3 = false;
	public bool clearCoral4 = false;
	//holds reference for rigidbody
	private Rigidbody rb;
	public GameObject urchin_01;
	GameObject urchinClone1;
	GameObject urchinClone2;
	GameObject urchinClone3;
	GameObject urchinClone4;

	//Variables for supersucker
  public GameObject superSucker;
  private bool superSuckerOn;

	//called on the first frame the script is active
	void Start()
	{
		rb = GetComponent<Rigidbody> ();
        superSuckerOn = false;

	}

	// Update is called once per frame, before rendering a frame, where most of game will go
	void Update()
	{
				//For debugging purposes when Kinect is not availaible.
        if (Input.GetKey(KeyCode.R)) {
            GetSuperSucker();
        }
    }

	//called just before performing physics calculations
	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		//leave the force mode at default by omitting it from code
		movement = new Vector3(moveHorizontal,0.0f,moveVertical);

		rb.AddForce (movement * speed);

	}

	void OnTriggerEnter(Collider other)
	{
		//int tag = 0;
		if (other.CompareTag ("0,1")) {
			clearCoral1 = true;
			urchinClone1 = Instantiate (urchin_01, new Vector3(0.5f,0.55f,4.5f), Quaternion.identity) as GameObject;
		}
		else if (other.CompareTag ("1,0")) {
			clearCoral2 = true;
			urchinClone2 = Instantiate (urchin_01, new Vector3(4.95f,0.55f,0.05f), Quaternion.identity) as GameObject;
		}
		if (other.CompareTag ("0,-1")) {
			clearCoral3 = true;
			urchinClone3 = Instantiate (urchin_01, new Vector3(0.17f,0.55f,-4.27f), Quaternion.identity) as GameObject;
		}
		if (other.CompareTag ("-1,0")) {
			clearCoral4 = true;
			urchinClone4 = Instantiate (urchin_01, new Vector3(-5.23f,0.55f,0.74f), Quaternion.identity) as GameObject;
		}
    if (other.CompareTag("Algae")) { //Kill all the algae!
        if (superSuckerOn) { //Super sucker is on, destroy algae on player contact.
            Destroy(other.gameObject);
        }
    }

		//if(Int32.TryParse (other.tag, out tag))//converts tag To int
		//{clearCoral[tag] == true;}
	}

		/**
		* Instantiates a super sucker game object and have it folow the player.
		* Usually called by a Kinect Gesture Frame update.
		* Can be called by a hotkey for debugging purposes.
		*/

    public void GetSuperSucker() {
        if (superSuckerOn == false) {
            Debug.Log("Getting super sucker");
            GameObject newSuperSucker = Instantiate(superSucker, rb.position, Quaternion.identity) as GameObject;
            newSuperSucker.transform.parent = gameObject.transform;
            superSuckerOn = true;
        }
        //else { //put away super sucker
        //    Debug.Log("Put away super sucker");
        //    superSuckerOn = false;
        //}
    }
}
