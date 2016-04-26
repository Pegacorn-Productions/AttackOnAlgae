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

    public Image image;
    public Image image2;

    void start() {
    }

    void Update() {
       // mousePos = Input.mousePosition;
       // if (Input.GetKeyDown(KeyCode.Space))
       //     startVertex = new Vector3(mousePos.x / Screen.width, mousePos.y / Screen.height, 0);
       // Debug.Log(mousePos.x / Screen.width + " " + mousePos.y / Screen.height);

       // image.transform.position = Input.mousePosition;
       

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

                float distance1 = Mathf.Sqrt(Mathf.Pow(rightElbowNormalizedX - rightWristNormalizedX, 2) + Mathf.Pow(rightElbowNormalizedY - rightWristNormalizedY, 2) + Mathf.Pow(rightElbowNormalizedZ - rightWristNormalizedZ, 2));
                float distance2 = Mathf.Sqrt(Mathf.Pow(rightShoulderNormalizedX - rightElbowNormalizedX, 2) + Mathf.Pow(rightShoulderNormalizedY - rightElbowNormalizedY, 2) + Mathf.Pow(rightShoulderNormalizedZ - rightElbowNormalizedZ, 2));
                float totalDistance = distance1 + distance2;

                float spineMidNormalizedX = (spineMid.Position.X - (-1f)) / (1f - (-1f));
                float spineMidNormalizedY = (spineMid.Position.Y - (-1f)) / (1f - (-1f));
                float spineMidNormalizedZ = (spineMid.Position.Z - _BodyManager.getMinZ()) / (_BodyManager.getMaxZ() - _BodyManager.getMinZ());

                float customCoordinateMaxX = spineMidNormalizedX + totalDistance;
                float customCoordinateMinX = spineMidNormalizedX - totalDistance;
                float customCoordinateMaxY = spineMidNormalizedY + totalDistance;
                float customCoordinateMinY = spineMidNormalizedY - totalDistance;

                

                endRightVertex = new Vector3(rightHandNormalizedX * Screen.width, rightHandNormalizedY * Screen.height, 0);
                endLeftVertex = new Vector3(leftHandNormalizedX * Screen.width, leftHandNormalizedY * Screen.height, 0);

               // Debug.Log(endRightVertex.ToString());



                image.transform.position = endRightVertex;
                image2.transform.position = endLeftVertex;
            }

        }


      

    }
    //void OnPostRender() {
    //    if (!mat) {
    //        Debug.LogError("Please Assign a material on the inspector");
    //        return;
    //    }
    //    GL.PushMatrix();
    //    mat.SetPass(0);
    //    GL.LoadOrtho();
    //    GL.Begin(GL.LINES);
    //    GL.Color(Color.red);
    //    GL.Vertex(startVertex);
    //    GL.Vertex(endRightVertex);
    //    GL.End();
    //    GL.Begin(GL.LINES);
    //    GL.Color(Color.red);
    //    GL.Vertex(startVertex);
    //    GL.Vertex(endLeftVertex);
    //    GL.End();
    //    GL.PopMatrix();
    //}


    void Example() {
        startVertex = new Vector3(0, 0, 0);
    }





    private Dictionary<Kinect.JointType, Kinect.JointType> _BoneMap = new Dictionary<Kinect.JointType, Kinect.JointType>()
    {
        { Kinect.JointType.FootLeft, Kinect.JointType.AnkleLeft },
        { Kinect.JointType.AnkleLeft, Kinect.JointType.KneeLeft },
        { Kinect.JointType.KneeLeft, Kinect.JointType.HipLeft },
        { Kinect.JointType.HipLeft, Kinect.JointType.SpineBase },
        
        { Kinect.JointType.FootRight, Kinect.JointType.AnkleRight },
        { Kinect.JointType.AnkleRight, Kinect.JointType.KneeRight },
        { Kinect.JointType.KneeRight, Kinect.JointType.HipRight },
        { Kinect.JointType.HipRight, Kinect.JointType.SpineBase },
        
        { Kinect.JointType.HandTipLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.ThumbLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.HandLeft, Kinect.JointType.WristLeft },
        { Kinect.JointType.WristLeft, Kinect.JointType.ElbowLeft },
        { Kinect.JointType.ElbowLeft, Kinect.JointType.ShoulderLeft },
        { Kinect.JointType.ShoulderLeft, Kinect.JointType.SpineShoulder },
        
        { Kinect.JointType.HandTipRight, Kinect.JointType.HandRight },
        { Kinect.JointType.ThumbRight, Kinect.JointType.HandRight },
        { Kinect.JointType.HandRight, Kinect.JointType.WristRight },
        { Kinect.JointType.WristRight, Kinect.JointType.ElbowRight },
        { Kinect.JointType.ElbowRight, Kinect.JointType.ShoulderRight },
        { Kinect.JointType.ShoulderRight, Kinect.JointType.SpineShoulder },
        
        { Kinect.JointType.SpineBase, Kinect.JointType.SpineMid },
        { Kinect.JointType.SpineMid, Kinect.JointType.SpineShoulder },
        { Kinect.JointType.SpineShoulder, Kinect.JointType.Neck },
        { Kinect.JointType.Neck, Kinect.JointType.Head },
    };
}