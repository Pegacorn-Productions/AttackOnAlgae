using UnityEngine;
using System.Collections;

public class receiverScript : MonoBehaviour {

    public bool fire;
    private bool called;

	// Use this for initialization
	void Start () {
        fire = false;
        called = false;
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(fire == true && called == false)
        {
            Debug.Log("Broadcasting!");
           // BroadcastMessage("getSuperSucked");
            called = true;
        }
	}
}
