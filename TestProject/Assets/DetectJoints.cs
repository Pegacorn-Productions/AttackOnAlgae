using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class DetectJoints : MonoBehaviour {

    public GameObject BodySourceManager;
    public JointType TrackJoint;
    private BodySourceManager bodyManager;

	// Use this for initialization
	void Start () {
        if (BodySourceManager == null)
        {

        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
