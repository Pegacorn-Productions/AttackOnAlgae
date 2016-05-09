using UnityEngine;
using System.Collections;

public class getPath : MonoBehaviour {

    public GameObject[] thePaths;

	// Use this for initialization
	void Start ()
    {
        transform.position = thePaths[0].transform.position;
        moveOnPath myPath = GetComponent<moveOnPath>();
        myPath.pathName = thePaths[0].name;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
