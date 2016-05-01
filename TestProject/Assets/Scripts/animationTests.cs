using UnityEngine;
using System.Collections;

public class animationTests : MonoBehaviour {

    public Animator anim;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKeyUp(KeyCode.I))
        {
            anim.SetBool("wave", false);
            anim.SetBool("move", false);
        }
        if(Input.GetKeyUp(KeyCode.M))
        {
            anim.SetBool("move", true);
        }

        if(Input.GetKeyUp(KeyCode.W))
        {
            anim.SetBool("wave", true);
        }

        if(Input.GetKeyUp(KeyCode.K))
        {
            anim.SetBool("kneel", true);
            anim.SetBool("stand", false);
        }

        if(Input.GetKeyUp(KeyCode.S))
        {
            anim.SetBool("stand", true);
            anim.SetBool("kneel", false);
        }
	}
}
