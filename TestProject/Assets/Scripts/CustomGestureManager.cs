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
            else if (gesture.Name == "HandSwipe") {
                handSwipe = gesture;
            }
            else if (gesture.Name == "HandWave") {
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
                DiscreteGestureResult handSwipeResult = null;
                DiscreteGestureResult handWaveResult = null;

                if (frame.DiscreteGestureResults.Count > 0) {
                    handAboveHeadResult = frame.DiscreteGestureResults[handAboveHead];
                    handSwipeResult = frame.DiscreteGestureResults[handSwipe];
                    handWaveResult = frame.DiscreteGestureResults[handWave];
                }
                if (handAboveHeadResult == null) {
                    return;
                }

                if (handAboveHeadResult.Detected == true && handAboveHeadResult.Confidence > 0.30f) {
                   // Debug.Log("Right hand above head detected. Confidence is " + handAboveHeadResult.Confidence.ToString());
                    AttachedObject.GetComponent<diverScript>().GetSuperSucker();
                }

                if (handSwipeResult.Detected == true && handSwipeResult.Confidence > 0.80f) {
                  //  Debug.Log("Hand Swipe detected. Confidence is" + handSwipeResult.Confidence.ToString());
                    SpeechBubble.GetComponent<SpeechBubble>().DismissSpeechBuble();
                }

                if (handWaveResult.Detected == true && handWaveResult.Confidence > 0.01f) {
                    AttachedObject.GetComponent<tourScript>().setGoScriptTrue();
                   // Debug.Log("Hand Wave detected. Confidence is " + handWaveResult.Confidence.ToString());
                }

            }
        }
    }
}
