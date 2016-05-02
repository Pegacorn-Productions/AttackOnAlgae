using UnityEngine;
using System.Collections;

public class testingMovement : MonoBehaviour {

    public GameObject moveThis;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyUp(KeyCode.A))
        {
            moveThis.transform.position -= new Vector3 (1, 0, 0);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            moveThis.transform.position += new Vector3(0, 0, 1);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            moveThis.transform.position -= new Vector3(0, 0, 1);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            moveThis.transform.position += new Vector3(1, 0, 0);
        }


    }
}
