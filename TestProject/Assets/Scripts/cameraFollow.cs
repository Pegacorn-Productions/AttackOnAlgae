using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour {

    public GameObject Ball;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3(Ball.transform.position.x, Ball.transform.position.y + 3, Ball.transform.position.z - 7);


    }
}
