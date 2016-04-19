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

    private bool DetectRightHandAbove(int bodyID) {

        Kinect.Body body = data[bodyID];
        Kinect.Joint handRight = body.Joints[Kinect.JointType.HandRight];
        Kinect.Joint elbowRight = body.Joints[Kinect.JointType.ElbowRight];
        Kinect.Joint shoulderRight = body.Joints[Kinect.JointType.ShoulderRight];

        if ((handRight.Position.Y > elbowRight.Position.Y) && (elbowRight.Position.Y > shoulderRight.Position.Y) && (handRight.Position.X - shoulderRight.Position.X) < 0.5f) {
            return true;
        }
        else {
            return false;
        }

    }

    private bool DetectGrasping(int bodyID) {
        Kinect.Body body = data[bodyID];
        Kinect.Joint handRight = body.Joints[Kinect.JointType.HandRight];
        Kinect.Joint spineMid = body.Joints[Kinect.JointType.SpineMid];

        if (Mathf.Abs(handRight.Position.Z - spineMid.Position.Z) < 0.3f) {
            if (Kinect.HandState.Closed == data[bodyID].HandRightState) {
                return true;
            }
            else {
                return false;
            }
        }
        else {
            return false;
        }
    }

    private bool DetectHandWave(int bodyID){
        int caseSwitch = 0;

        switch (caseSwitch){
            case 0:
                if (DetectHandWaveHelperPart1(bodyID)) {
                    System.Threading.Thread.Sleep(200);
                    goto case 1;
                }
                break;
            case 1:
                if (DetectHandWaveHelperPart2(bodyID)) {
                    System.Threading.Thread.Sleep(200);
                    goto case 2;
                }
                break;
            case 2:
                if (DetectHandWaveHelperPart2(bodyID)) {
                    System.Threading.Thread.Sleep(200);
                    return true;
                }
                break;
            default:
                break;
        }
        return false;
    }

     private bool DetectHandWaveHelperPart1(int bodyID){
        Kinect.Body body = data[bodyID];
        Kinect.Joint handRight = body.Joints[Kinect.JointType.HandRight];
        Kinect.Joint elbowRight = body.Joints[Kinect.JointType.ElbowRight];

        if ((handRight.Position.Y > elbowRight.Position.Y) && (handRight.Position.X > elbowRight.Position.X)) {
            return true;
        }
        else {
            return false;
        }
    }

     private bool DetectHandWaveHelperPart2(int bodyID) {
         Kinect.Body body = data[bodyID];
         Kinect.Joint handRight = body.Joints[Kinect.JointType.HandRight];
         Kinect.Joint elbowRight = body.Joints[Kinect.JointType.ElbowRight];

         if ((handRight.Position.Y > elbowRight.Position.Y) && (handRight.Position.X < elbowRight.Position.X)) {
             return true;
         }
         else {
             return false;
         }
     }

    private bool DetectHandSwipe(int bodyID){
        int caseSwitch = 0;

        switch (caseSwitch) {
            case 0:
                if (DetectHandSwipeHelperPart1(bodyID)) {
                    System.Threading.Thread.Sleep(200);
                    goto case 1;
                }
                break;
            case 1:
                if (DetectHandSwipeHelperPart2(bodyID)) {
                    System.Threading.Thread.Sleep(200);
                    return true;
                }
                break;
            default:
                break;
        }
        return false;
    }

    private bool DetectHandSwipeHelperPart1(int bodyID) {
        Kinect.Body body = data[bodyID];
        Kinect.Joint handRight = body.Joints[Kinect.JointType.HandRight];
        Kinect.Joint hipRight = body.Joints[Kinect.JointType.HipRight];
        Kinect.Joint spineMid = body.Joints[Kinect.JointType.SpineMid];

        if ((handRight.Position.Y > hipRight.Position.Y) && (handRight.Position.X > spineMid.Position.X)) {
            return true;
        }
        else {
            return false;
        }
    }

    private bool DetectHandSwipeHelperPart2(int bodyID) {
        Kinect.Body body = data[bodyID];
        Kinect.Joint handRight = body.Joints[Kinect.JointType.HandRight];
        Kinect.Joint hipRight = body.Joints[Kinect.JointType.HipRight];
        Kinect.Joint spineMid = body.Joints[Kinect.JointType.SpineMid];

        if ((handRight.Position.Y > hipRight.Position.Y) && (handRight.Position.X < spineMid.Position.X)) {
            return true;
        }
        else {
            return false;
        }
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

        ret = new Vector3(distanceX*100, distanceY*100);
        

      
        Rigidbody rbJoint = joint.GetComponent<Rigidbody>();
        rbJoint.AddForce(ret);
        Debug.Log(ret.ToString());
        //return ret;

    }
  
}
