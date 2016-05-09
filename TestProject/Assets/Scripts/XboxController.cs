using UnityEngine;
using System.Collections;

public class XboxController : MonoBehaviour {

    public GameObject playerGesture;
    public GameObject diver;
    public GameObject speechBubble;

    public bool useController = true;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("A")) {
            speechBubble.GetComponent<SpeechBubble>().DismissSpeechBuble();
        }
        if (Input.GetButtonDown("B")) {

        }

        if (Input.GetButtonDown("X")) {
            playerGesture.GetComponent<PlayerGesture>().forceStop();

        }

        if (Input.GetButtonDown("Y")) {
            diver.GetComponent<diverScript>().GetSuperSucker();
        }
        if (Input.GetButtonDown("Start")) {

            diver.GetComponent<tourScript>().setGoScriptTrue();
        }
        if (Input.GetButtonDown("Select")) {
            Application.Quit();
        }

        //Allow the use of left joystick to control supersucker
        if (useController) {
           // Debug.Log(Input.GetAxis("LeftJoystickX").ToString());
            playerGesture.GetComponent<PlayerGesture>().forcePush(Input.GetAxis("LeftJoystickX"), -1f*Input.GetAxis("LeftJoystickY"));
        }
    }
}