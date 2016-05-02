using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;

/// <summary>
/// Script that gives normalized joint position for certain parts of the skeleton.
/// Attach this script to a gameobject and set the BodySourceManager game sobject to this game object.
/// Main use is to simply call getJointPositionArray() to get all the positions of the joints.
/// </summary>
public class DiverKinect : MonoBehaviour {
    public GameObject BodySourceManager;

    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodySourceManager _BodyManager;

    private float[,] jointPositionArray = new float[11, 2];
    private enum MyJoint { neck, spineMid, spineBase, wristRight, wristLeft, elbowRight, elbowLeft, shoulderRight, shoulderLeft, kneeRight, kneeLeft };

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (BodySourceManager == null) {
            return;
        }

        _BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
        if (_BodyManager == null) {
            return;
        }

        Kinect.Body[] data = _BodyManager.GetData();
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

        foreach (var body in data) {
            if (body == null) {
                continue;
            }

            if (body.IsTracked) {
                Kinect.Joint[] myJoints = { body.Joints[Kinect.JointType.Neck], 
                body.Joints[Kinect.JointType.Neck],
                body.Joints[Kinect.JointType.SpineMid],
                body.Joints[Kinect.JointType.SpineBase],
                body.Joints[Kinect.JointType.WristRight],
                body.Joints[Kinect.JointType.WristLeft],
                body.Joints[Kinect.JointType.ElbowRight],
                body.Joints[Kinect.JointType.ElbowLeft],
                body.Joints[Kinect.JointType.ShoulderRight],
                body.Joints[Kinect.JointType.ShoulderLeft],
                body.Joints[Kinect.JointType.KneeRight],
                body.Joints[Kinect.JointType.KneeLeft]};

                for (int i = 0; i < myJoints.Length; i++) {
                    float xNormal = (myJoints[i].Position.X - (-1f)) / (1f - (-1f));
                    float yNormal = (myJoints[i].Position.Y - (-1f)) / (1f - (-1f));

                    jointPositionArray[i, 0] = xNormal;
                    jointPositionArray[i, 1] = yNormal;
                }
            }
        }
    }

    /// <summary>
    /// Returns the array for the normalized joint positions.
    /// First index indiciates the  joint and second index indicates x and y.
    /// 
    /// Order for first index: neck, middle spine, base spine, right wrist, left wrist,
    /// right elbow, left elbow, right shoulder, left shoulder, right knee, left knee.
    /// 
    /// Order for second index: x, y
    /// </summary>
    /// <returns>The positions of all the joints in a 2D array.</returns>
    public float[,] getJointPositionArray() {
        return jointPositionArray;
    }

}
