using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;

public class HandController : MonoBehaviour {

    public GameObject BodySourceManager;

    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodySourceManager _BodyManager;

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
                Kinect.Joint rightHand = body.Joints[Kinect.JointType.HandRight];
                Kinect.Joint elbowRight = body.Joints[Kinect.JointType.ElbowRight];

                //Check if there is a significant change first for hand to eblow.
                if (Mathf.Abs(rightHand.Position.X - elbowRight.Position.X) > 0.2f) {
                    if (rightHand.Position.X > elbowRight.Position.X) {
                        //Go right since right hand is right of right shoulder
                        if (rightHand.Position.Y > elbowRight.Position.Y) {
                            //Higher so go up
                            Vector2 ret = new Vector2(1, 1);
                        }
                        else if (rightHand.Position.Y < elbowRight.Position.Y) {
                            //Below so go down
                            Vector2 ret = new Vector2(1, -1);
                        }
                    }
                    else if (rightHand.Position.X < elbowRight.Position.X) {
                        //Go left since right hand is left of right shoulder
                        if (rightHand.Position.Y > elbowRight.Position.Y) {
                            //Higher so go up
                            Vector2 ret = new Vector2(-1, 1);
                        }
                        else if (rightHand.Position.Y < elbowRight.Position.Y) {
                            //Below so go down
                            Vector2 ret = new Vector2(-1, -1);
                        }
                    }
                }

            }

        }
    }
}
