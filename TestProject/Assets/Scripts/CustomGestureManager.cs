using UnityEngine;
using System.Collections;
using Windows.Kinect;
using Microsoft.Kinect.VisualGestureBuilder;
using System.IO;

public class CustomGestureManager : MonoBehaviour 
{
    VisualGestureBuilderDatabase _gestureDatabase;
    VisualGestureBuilderFrameSource _gestureFrameSource;
    VisualGestureBuilderFrameReader _gestureFrameReader;
    KinectSensor _kinect;
    Gesture _salute;
    ParticleSystem _ps;

    public GameObject AttachedObject;

    public void SetTrackingId(ulong id)
    {
        _gestureFrameReader.IsPaused = false;
        _gestureFrameSource.TrackingId = id;
        _gestureFrameReader.FrameArrived += _gestureFrameReader_FrameArrived;
    }

	// Use this for initialization
	void Start () 
    {
    
        _kinect = KinectSensor.GetDefault();

        _gestureDatabase = VisualGestureBuilderDatabase.Create(Application.streamingAssetsPath + "/rightHandAbove.gbd");
        _gestureFrameSource = VisualGestureBuilderFrameSource.Create(_kinect, 0);

        foreach (var gesture in _gestureDatabase.AvailableGestures)
        {
            _gestureFrameSource.AddGesture(gesture);

            if (gesture.Name == "rightHandAbove")
            {
                _salute = gesture;
            }
        
        }

        _gestureFrameReader = _gestureFrameSource.OpenReader();
        _gestureFrameReader.IsPaused = true;
	}

    void _gestureFrameReader_FrameArrived(object sender, VisualGestureBuilderFrameArrivedEventArgs e)
    {
        VisualGestureBuilderFrameReference frameReference = e.FrameReference;
        using (VisualGestureBuilderFrame frame = frameReference.AcquireFrame())
        {
            if (frame != null && frame.DiscreteGestureResults != null)
            {
       

                DiscreteGestureResult result = null;

                if (frame.DiscreteGestureResults.Count > 0)
                    result = frame.DiscreteGestureResults[_salute];
                if (result == null)
                    return;

                if (result.Detected == true)
                {
                    Debug.Log("Gesture detected");
                   
                }
              
            }
        }
    }
}
