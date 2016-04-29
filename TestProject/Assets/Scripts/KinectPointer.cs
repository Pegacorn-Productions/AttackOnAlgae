using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;
using UnityEngine.UI;

public class KinectPointer : MonoBehaviour {
    public Material mat;
    private Vector3 startVertex;
    private Vector3 mousePos;
    private Vector3 endRightVertex;
    private Vector3 endLeftVertex;


    public GameObject BodySourceManager;

    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodySourceManager _BodyManager;

    public Image rightImage;
    public Image leftImage;

    void start() {
    }

    void Update() {

        if (Input.GetKey("escape"))
            Application.Quit();

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
                Kinect.Joint leftHand = body.Joints[Kinect.JointType.HandLeft];
                Kinect.Joint rightWrist = body.Joints[Kinect.JointType.WristRight];
                Kinect.Joint rightElbow = body.Joints[Kinect.JointType.ElbowRight];
                Kinect.Joint rightShoulder = body.Joints[Kinect.JointType.ShoulderRight];
                Kinect.Joint leftWrist = body.Joints[Kinect.JointType.WristLeft];
                Kinect.Joint leftElbow = body.Joints[Kinect.JointType.ElbowLeft];
                Kinect.Joint leftShoulder = body.Joints[Kinect.JointType.ShoulderLeft];
                Kinect.Joint spineMid = body.Joints[Kinect.JointType.SpineMid];

                float rightHandNormalizedX = (rightHand.Position.X - (-1f)) / (1f - (-1f));
                float rightHandNormalizedY = (rightHand.Position.Y - (-1f)) / (1f - (-1f));
                float rightHandNormalizedZ = ((rightHand.Position.Z - _BodyManager.getMinZ()) / (_BodyManager.getMaxZ() - _BodyManager.getMinZ()));

                float leftHandNormalizedX = (leftHand.Position.X - (-1f)) / (1f - (-1f));
                float leftHandNormalizedY = (leftHand.Position.Y - (-1f)) / (1f - (-1f));
                float leftHandNormalizedZ = ((leftHand.Position.Z - _BodyManager.getMinZ()) / (_BodyManager.getMaxZ() - _BodyManager.getMinZ()));

                float rightWristNormalizedX = (rightWrist.Position.X - (-1f)) / (1f - (-1f));
                float rightWristNormalizedY = (rightWrist.Position.Y - (-1f)) / (1f - (-1f));
                float rightWristNormalizedZ = (rightWrist.Position.Z - _BodyManager.getMinZ()) / (_BodyManager.getMaxZ() - _BodyManager.getMinZ());

                float rightElbowNormalizedX = (rightElbow.Position.X - (-1f)) / (1f - (-1f));
                float rightElbowNormalizedY = (rightElbow.Position.Y - (-1f)) / (1f - (-1f));
                float rightElbowNormalizedZ = (rightElbow.Position.Z - _BodyManager.getMinZ()) / (_BodyManager.getMaxZ() - _BodyManager.getMinZ());

                float rightShoulderNormalizedX = (rightShoulder.Position.X - (-1f)) / (1f - (-1f));
                float rightShoulderNormalizedY = (rightShoulder.Position.Y - (-1f)) / (1f - (-1f));
                float rightShoulderNormalizedZ = (rightShoulder.Position.Z - _BodyManager.getMinZ()) / (_BodyManager.getMaxZ() - _BodyManager.getMinZ());

                float leftWristNormalizedX = (leftWrist.Position.X - (-1f)) / (1f - (-1f));
                float leftWristNormalizedY = (leftWrist.Position.Y - (-1f)) / (1f - (-1f));
                float leftWristNormalizedZ = (leftWrist.Position.Z - _BodyManager.getMinZ()) / (_BodyManager.getMaxZ() - _BodyManager.getMinZ());

                float leftElbowNormalizedX = (leftElbow.Position.X - (-1f)) / (1f - (-1f));
                float leftElbowNormalizedY = (leftElbow.Position.Y - (-1f)) / (1f - (-1f));
                float leftElbowNormalizedZ = (leftElbow.Position.Z - _BodyManager.getMinZ()) / (_BodyManager.getMaxZ() - _BodyManager.getMinZ());

                float leftShoulderNormalizedX = (leftShoulder.Position.X - (-1f)) / (1f - (-1f));
                float leftShoulderNormalizedY = (leftShoulder.Position.Y - (-1f)) / (1f - (-1f));
                float leftShoulderNormalizedZ = (leftShoulder.Position.Z - _BodyManager.getMinZ()) / (_BodyManager.getMaxZ() - _BodyManager.getMinZ());

                float averageWristX = (rightWristNormalizedX + leftWristNormalizedY) / 2f;
                float averageWristY = (rightWristNormalizedY + leftWristNormalizedY) / 2f;
                float averageWristZ = (rightWristNormalizedZ + leftWristNormalizedZ) / 2f;

                float averageEblowX = (rightElbowNormalizedX + leftElbowNormalizedY) / 2f;
                float averageElbowY = (rightElbowNormalizedY + leftElbowNormalizedY) / 2f;
                float averageElbowZ = (rightElbowNormalizedZ + leftElbowNormalizedZ) / 2f;

                float averageShoulderX = (rightShoulderNormalizedX + leftShoulderNormalizedY) / 2f;
                float averageShoulderY = (rightShoulderNormalizedY + leftShoulderNormalizedY) / 2f;
                float averageShoulderZ = (rightShoulderNormalizedZ + leftShoulderNormalizedZ) / 2f;

                float distance1 = Mathf.Sqrt(Mathf.Pow(averageEblowX - averageWristX, 2) + Mathf.Pow(averageElbowY - averageWristY, 2) + Mathf.Pow(averageElbowZ - averageWristZ, 2));
                float distance2 = Mathf.Sqrt(Mathf.Pow(averageShoulderX - averageEblowX, 2) + Mathf.Pow(averageShoulderY - averageElbowY, 2) + Mathf.Pow(averageShoulderZ - averageElbowZ, 2));
                float totalDistance = distance1 + distance2;

                float spineMidNormalizedX = (spineMid.Position.X - (-1f)) / (1f - (-1f));
                float spineMidNormalizedY = (spineMid.Position.Y - (-1f)) / (1f - (-1f));
                float spineMidNormalizedZ = (spineMid.Position.Z - _BodyManager.getMinZ()) / (_BodyManager.getMaxZ() - _BodyManager.getMinZ());

                float customCoordinateMaxX = spineMidNormalizedX + totalDistance;
                float customCoordinateMinX = spineMidNormalizedX - totalDistance;
                float customCoordinateMaxY = spineMidNormalizedY + totalDistance;
                float customCoordinateMinY = spineMidNormalizedY - totalDistance;

                float rightPersonalX = (rightHandNormalizedX - customCoordinateMinX) / (customCoordinateMaxX - customCoordinateMinX);
                float rightPersonalY = (rightHandNormalizedY - customCoordinateMinY) / (customCoordinateMaxY - customCoordinateMinY);
                float leftPersonalX = (leftHandNormalizedX - customCoordinateMinX) / (customCoordinateMaxX - customCoordinateMinX);
                float leftPersonalY = (leftHandNormalizedY - customCoordinateMinX) / (customCoordinateMaxY - customCoordinateMinY);


                endRightVertex = new Vector3(rightPersonalX * Screen.width, rightPersonalY * Screen.height, 0);
                endLeftVertex = new Vector3(leftPersonalX * Screen.width, leftPersonalY * Screen.height, 0);

                rightImage.transform.position = endRightVertex;
                leftImage.transform.position = endLeftVertex;
            }

        }


      

    }

}