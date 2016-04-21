using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;

public class KinectPointer : MonoBehaviour {
    public Material mat;
    private Vector3 startVertex;
    private Vector3 mousePos;
    private Vector3 endRightVertex;
    private Vector3 endLeftVertex;

    public GameObject BodySourceManager;

    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodySourceManager _BodyManager;

    void Update() {
       // mousePos = Input.mousePosition;
       // if (Input.GetKeyDown(KeyCode.Space))
       //     startVertex = new Vector3(mousePos.x / Screen.width, mousePos.y / Screen.height, 0);
       // Debug.Log(mousePos.x / Screen.width + " " + mousePos.y / Screen.height);


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
                Kinect.Joint shoulderRight = body.Joints[Kinect.JointType.ShoulderRight];

                float refactoredX = leftHand.Position.X - shoulderRight.Position.X;
                float refactoredY = leftHand.Position.Y - shoulderRight.Position.Y;

                float rightNormalizedX = (rightHand.Position.X - (-1f)) / (1 - (-1f));
                float rightNormalizedY = (rightHand.Position.Y - (-1f)) / (1 - (-1f));

                float leftNormalizedX = (leftHand.Position.X - (-1f)) / (1 - (-1f));
                float leftNormalizedY = (leftHand.Position.Y - (-1f)) / (1 - (-1f));

                float spineBaseNormalizedX = (shoulderRight.Position.X - (-1f)) / (1 - (-1f));
                float spineBaseNormalizedY = (shoulderRight.Position.Y - (-1f)) / (1 - (-1f));

                float endX = rightNormalizedX - spineBaseNormalizedX;
                float endY = rightNormalizedY - spineBaseNormalizedY;


                startVertex = new Vector3(spineBaseNormalizedX, spineBaseNormalizedY, 0 );
                endRightVertex = new Vector3(rightNormalizedX, rightNormalizedY, 0);
                endLeftVertex = new Vector3(leftNormalizedX, leftNormalizedY, 0);

            }

        }




    }
    void OnPostRender() {
        if (!mat) {
            Debug.LogError("Please Assign a material on the inspector");
            return;
        }
        GL.PushMatrix();
        mat.SetPass(0);
        GL.LoadOrtho();
        GL.Begin(GL.LINES);
        GL.Color(Color.red);
        GL.Vertex(startVertex);
        GL.Vertex(endRightVertex);
        GL.End();
        GL.Begin(GL.LINES);
        GL.Color(Color.red);
        GL.Vertex(startVertex);
        GL.Vertex(endLeftVertex);
        GL.End();
        GL.PopMatrix();
    }


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