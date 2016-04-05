﻿using UnityEngine;
using System.Collections;
using Windows.Kinect;
using Microsoft.Kinect.VisualGestureBuilder;
using System.IO;

public class CustomGestureManager : MonoBehaviour {
    VisualGestureBuilderDatabase _gestureDatabase;
    VisualGestureBuilderFrameSource _gestureFrameSource;
    VisualGestureBuilderFrameReader _gestureFrameReader;
    KinectSensor _kinect;
    Gesture rightHandAbove;
    Gesture handSwipe;
    Gesture grasping;
    ParticleSystem _ps;

    public GameObject AttachedObject;
    public GameObject SpeechBubble;


    public void SetTrackingId(ulong id) {
        _gestureFrameReader.IsPaused = false;
        _gestureFrameSource.TrackingId = id;
        _gestureFrameReader.FrameArrived += _gestureFrameReader_FrameArrived;
    }

    // Use this for initialization
    void Start() {

        _kinect = KinectSensor.GetDefault();

        _gestureDatabase = VisualGestureBuilderDatabase.Create(Application.streamingAssetsPath + "/rightHandAbove.gbd");
        _gestureFrameSource = VisualGestureBuilderFrameSource.Create(_kinect, 0);

        foreach (var gesture in _gestureDatabase.AvailableGestures) {
            _gestureFrameSource.AddGesture(gesture);

            if (gesture.Name == "rightHandAbove") {
                rightHandAbove = gesture;
            }
            else if (gesture.Name == "HandSwipe") {
                handSwipe = gesture;
            }
            else if (gesture.Name == "Grasping") {
                grasping = gesture;
            }

        }

        _gestureFrameReader = _gestureFrameSource.OpenReader();
        _gestureFrameReader.IsPaused = true;
    }

    void _gestureFrameReader_FrameArrived(object sender, VisualGestureBuilderFrameArrivedEventArgs e) {
        VisualGestureBuilderFrameReference frameReference = e.FrameReference;
        using (VisualGestureBuilderFrame frame = frameReference.AcquireFrame()) {
            if (frame != null && frame.DiscreteGestureResults != null) {
                DiscreteGestureResult rightHandAboveResult = null;
                DiscreteGestureResult handSwipeGestureResult = null;
                DiscreteGestureResult grasppingResult = null;

                if (frame.DiscreteGestureResults.Count > 0) {
                    rightHandAboveResult = frame.DiscreteGestureResults[rightHandAbove];
                    handSwipeGestureResult = frame.DiscreteGestureResults[handSwipe];
                    grasppingResult = frame.DiscreteGestureResults[grasping];
                }
                if (rightHandAboveResult == null) {
                    return;
                }

                if (rightHandAboveResult.Detected == true && rightHandAboveResult.Confidence > 0.20f) {
                    Debug.Log("Right hand above head detected. Confidence is " + rightHandAboveResult.Confidence.ToString());
                    AttachedObject.GetComponent<diverScript>().GetSuperSucker();
                }

                if (handSwipeGestureResult.Detected == true && handSwipeGestureResult.Confidence > 0.20f) {
                    Debug.Log("Hand Swipe detected. Confidence is" + handSwipeGestureResult.Confidence.ToString());
                    SpeechBubble.GetComponent<SpeechBubble>().DismissSpeechBuble();
                }

                if (grasppingResult.Detected == true && grasppingResult.Confidence > 0.99f) {
                    Debug.Log("Grasping detected. Confidence is" + grasppingResult.Confidence.ToString());
                   // AttachedObject.GetComponent<PlayerController>().DestroyAlgae();
                }

            }
        }
    }
}
