using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;
using UnityEngine.UI;

public class KinectPointer : MonoBehaviour {
    private Vector3 endRightVertex;
    private Vector3 endLeftVertex;

    public GameObject BodySourceManager;

    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodySourceManager _BodyManager;

    public Image rightImage;
    public Image leftImage;

    /// <summary>
    /// Updates the pointer position on screen with the right and left hands.
    /// Position of the cursor is the relative distance between the hands and spines.
    /// The custom coordinate plane system from using the spine is based on the length of the person's arm.
    /// </summary>
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
                Kinect.Joint spineMid = body.Joints[Kinect.JointType.SpineMid];
                Kinect.Joint spineShoulder = body.Joints[Kinect.JointType.SpineShoulder];
                Kinect.Joint spineBase = body.Joints[Kinect.JointType.SpineBase];

                float spineShoulderNormalizedX = (spineShoulder.Position.X - (-1f)) / (1f - (-1f));
                float spineShoulderNormalizedY = (spineShoulder.Position.Y - (-1f)) / (1f - (-1f));
                float spineShoulderNormalizedZ = ((spineShoulder.Position.Z - _BodyManager.getMinZ()) / (_BodyManager.getMaxZ() - _BodyManager.getMinZ()));

                float spineBaseNormalizedX = (spineBase.Position.X - (-1f)) / (1f - (-1f));
                float spineBaseNormalizedY = (spineBase.Position.Y - (-1f)) / (1f - (-1f));
                float spineBaseNormalizedZ = ((spineBase.Position.Z - _BodyManager.getMinZ()) / (_BodyManager.getMaxZ() - _BodyManager.getMinZ()));

                float rightHandNormalizedX = (rightHand.Position.X - (-1f)) / (1f - (-1f));
                float rightHandNormalizedY = (rightHand.Position.Y - (-1f)) / (1f - (-1f));
                float rightHandNormalizedZ = ((rightHand.Position.Z - _BodyManager.getMinZ()) / (_BodyManager.getMaxZ() - _BodyManager.getMinZ()));

                float leftHandNormalizedX = (leftHand.Position.X - (-1f)) / (1f - (-1f));
                float leftHandNormalizedY = (leftHand.Position.Y - (-1f)) / (1f - (-1f));
                float leftHandNormalizedZ = ((leftHand.Position.Z - _BodyManager.getMinZ()) / (_BodyManager.getMaxZ() - _BodyManager.getMinZ()));

                float distance1 = Mathf.Sqrt(Mathf.Pow(spineShoulderNormalizedX - spineBaseNormalizedX, 2) + Mathf.Pow(spineShoulderNormalizedY - spineBaseNormalizedY, 2) + Mathf.Pow(spineShoulderNormalizedZ - spineBaseNormalizedZ, 2));
                float totalDistance = distance1;

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