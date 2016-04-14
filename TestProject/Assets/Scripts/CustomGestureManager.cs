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
    Gesture handAboveHead;
    Gesture handSwipe;
    Gesture grasping;
    Gesture handWave;
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

        _gestureDatabase = VisualGestureBuilderDatabase.Create(Application.streamingAssetsPath + "/gestureDatabase.gbd");
        _gestureFrameSource = VisualGestureBuilderFrameSource.Create(_kinect, 0);

        foreach (var gesture in _gestureDatabase.AvailableGestures) {
            _gestureFrameSource.AddGesture(gesture);

            if (gesture.Name == "HandAboveHead") {
                handAboveHead = gesture;
            }
            else if (gesture.Name == "HandSwipeV2") {
                handSwipe = gesture;
            }
            else if (gesture.Name == "GraspingV2") {
                grasping = gesture;
            }
            else if (gesture.Name == "HandWaveV2") {
                handWave = gesture;
            }

        }

        _gestureFrameReader = _gestureFrameSource.OpenReader();
        _gestureFrameReader.IsPaused = true;
    }

    void _gestureFrameReader_FrameArrived(object sender, VisualGestureBuilderFrameArrivedEventArgs e) {
        VisualGestureBuilderFrameReference frameReference = e.FrameReference;
        using (VisualGestureBuilderFrame frame = frameReference.AcquireFrame()) {
            if (frame != null && frame.DiscreteGestureResults != null) {
                DiscreteGestureResult handAboveHeadResult = null;
                ContinuousGestureResult handSwipeGestureResult = null;
                ContinuousGestureResult grasppingResult = null;
                ContinuousGestureResult handWaveResult = null;

                if (frame.DiscreteGestureResults.Count > 0) {
                    handAboveHeadResult = frame.DiscreteGestureResults[handAboveHead];
                   // handSwipeGestureResult = frame.ContinuousGestureResults[handSwipe];
                    //grasppingResult = frame.ContinuousGestureResults[grasping];
                    //handWaveResult = frame.ContinuousGestureResults[handWave];
                }
                if (handAboveHeadResult == null) {
                    return;
                }

                if (handAboveHeadResult.Detected == true && handAboveHeadResult.Confidence > 0.70f) {
                    Debug.Log("Right hand above head detected. Confidence is " + handAboveHeadResult.Confidence.ToString());
                    AttachedObject.GetComponent<diverScript>().GetSuperSucker();
                }

                //if (handSwipeGestureResult.Progress > 0.80f) {
                //    Debug.Log("Hand Swipe detected. Confidence is" + handSwipeGestureResult.Progress.ToString());
                //    SpeechBubble.GetComponent<SpeechBubble>().DismissSpeechBuble();
                //}

                //if (grasppingResult.Progress > 0.80f) {
                //    Debug.Log("Grasping detected. Confidence is" + grasppingResult.Progress.ToString());
                //   // AttachedObject.GetComponent<PlayerController>().DestroyAlgae();
                //}
                //if (handWaveResult.Progress > 0.80f) {
                //    Debug.Log("Hand Wave detected. Confidence is " + handWaveResult.Progress.ToString());
                //}

            }
        }
    }
}
