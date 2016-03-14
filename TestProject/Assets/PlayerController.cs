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
	//public bool[] clearCoral = {false,false,false,false,false};
	public bool clearCoral1 = false;
	public bool clearCoral2 = false;
	public bool clearCoral3 = false;
	public bool clearCoral4 = false;
	//holds reference for rigidbody
	private Rigidbody rb;


	//called on the first frame the script is active
	void Start()
	{
		rb = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame, before rendering a frame, where most of game will go
	void Update() 
	{

	}
	//called just before performing physics calculations
	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		//leave the force mode at default by omitting it from code
		Vector3 movement = new Vector3(moveHorizontal,0.0f,moveVertical);

		rb.AddForce (movement * speed);
	}

	void OnTriggerEnter(Collider other)
	{
		//int tag = 0;
		if (other.CompareTag ("0,1")) {
			clearCoral1 = true;
		}
		else if (other.CompareTag ("1,0")) {
			clearCoral2 = true;
		}
		if (other.CompareTag ("0,-1")) {
			clearCoral3 = true;
		}
		if (other.CompareTag ("-1,0")) {
			clearCoral4 = true;
		}
		
			
		//if(Int32.TryParse (other.tag, out tag))//converts tag To int
		//{clearCoral[tag] == true;}
	}
}
