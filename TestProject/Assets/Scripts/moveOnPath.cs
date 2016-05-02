using UnityEngine;
using System.Collections;

public class moveOnPath : MonoBehaviour {

    public Paths PathToFollow;
    public int CurrentPointID = 0;
    public float speed;
    public float reachDist = 1f;
    public float rotateSpeed = 5f;
    public string pathName;
    public GameObject msgTarget;
    public string funcName = "This Is the Function We want to Call";

    private bool doPath;
    private int pathSet;

    Vector3 lastPos, currentPos;
	// Use this for initialization
	void Start ()
    {
        doPath = true;
        //PathToFollow = GameObject.Find(pathName).GetComponent<Paths>();
        //doPath = false;
        lastPos = transform.position;
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (doPath)
            pathing();
	}


    void pathing()
    {
        float distance = Vector3.Distance(PathToFollow.path_objs[CurrentPointID].position, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, PathToFollow.path_objs[CurrentPointID].position, Time.deltaTime * speed);

        var rotation = Quaternion.LookRotation(PathToFollow.path_objs[CurrentPointID].position - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateSpeed);

        if (distance <= reachDist)
        {
            CurrentPointID++;
        }

        if (CurrentPointID >= PathToFollow.path_objs.Count)
        {
            CurrentPointID = 0;
            doPath = false;
            msgTarget.BroadcastMessage(funcName);
            Debug.Log("Sent message " + funcName);
        }
    }

    void setPathing(bool go, string wayPointSet)
    {
        doPath = go;
        PathToFollow = GameObject.Find(wayPointSet).GetComponent<Paths>();
    }
}
