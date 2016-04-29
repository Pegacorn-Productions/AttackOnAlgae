using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;

public class PlayerGesture : MonoBehaviour {

    public GameObject BodySourceManager;
    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodySourceManager _BodyManager;
    public GameObject superSucker;

    private Transform joint;
    private Kinect.Body[] data;

	// Use this for initialization
	void Start () {
        joint = superSucker.transform.Find("joint1");
        for (int i = 2; i < 32; i++) {
            joint = joint.transform.Find("joint" + i);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (BodySourceManager == null) {
            return;
        }

        _BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
        if (_BodyManager == null) {
            return;
        }

        data = _BodyManager.GetData();
        if (data == null) {
            return;
        }

        List<ulong> trackedIds = new List<ulong>();
        foreach (var body in data) {
            if (body == null) {
                continue;
            }

            if (body.IsTracked) {
                trackedIds.Add(body.TrackingId);
            }
        }

        List<ulong> knownIds = new List<ulong>(_Bodies.Keys);

        // First delete untracked bodies
        foreach (ulong trackingId in knownIds) {
            if (!trackedIds.Contains(trackingId)) {
                Destroy(_Bodies[trackingId]);
                _Bodies.Remove(trackingId);
            }
        }

        int nearestBodyID = -1;
        for (int i = 0; i < data.Length; i++) {
            Kinect.Body body = data[i];
            if (body.IsTracked) {
                if (nearestBodyID == -1) {
                    nearestBodyID = i;
                }
                else {
                    Kinect.Joint head = body.Joints[Kinect.JointType.Head];
                    if (head.Position.Z < data[nearestBodyID].Joints[Kinect.JointType.Head].Position.Z) {
                        nearestBodyID = i;
                    }
                }
            }
        }

        //Debug.Log(nearestBodyID);

        if (nearestBodyID == -1) {
            return;
        }

        forcePush(nearestBodyID);

	}

    private void forcePush(int bodyID) {
        Kinect.Body body = data[bodyID];
        Kinect.Joint handRight = body.Joints[Kinect.JointType.HandRight];
        Kinect.Joint handLeft = body.Joints[Kinect.JointType.HandLeft];
        Kinect.Joint spineMid = body.Joints[Kinect.JointType.SpineMid];

        float handMidNormalizedX = (handLeft.Position.X + handRight.Position.X + 2) / 2.0f;
        float handMixNormalizedY = (handLeft.Position.Y + handRight.Position.Y + 2) / 2.0f;

        float spineMidNormalizedX = spineMid.Position.X + 1;
        float spineMidNormalizedY = spineMid.Position.Y + 1;

        float distanceX = handMidNormalizedX - spineMidNormalizedX;
        float distanceY = handMixNormalizedY - spineMidNormalizedY;

        Vector3 ret;
        Debug.Log(distanceX.ToString() + distanceY.ToString());
        if (Mathf.Abs(distanceX) < 0.1f && Mathf.Abs(distanceY) < 0.1f) {
            ret = new Vector3(0, 0, 0);
        }
        else {
            ret = new Vector3(distanceX, 0, distanceY);
        }
        

      
        Rigidbody rbJoint = joint.GetComponent<Rigidbody>();
        rbJoint.AddForce(ret);
       // Debug.Log(ret.ToString());
        //return ret;

    }
  
}
