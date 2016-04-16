using UnityEngine;
using System.Collections;

public class tourScript : MonoBehaviour {


    public GameObject diverSpeechBubble, fishSpeechBubble;
    private SpeechBubble diverSpeechScript, fishSpeechScript;

    public float textspeed = 5;

    private Animator diverAnim, fishAnim, CameraAnim;

    public GameObject[] diverTargets, fishTargets;
    public GameObject diverCurrentTarget, fishCurrentTarget;

    public GameObject fish, diver, cameraLocation, miniMap, viewcamera, supersucker; //viewcamera is basically the camera location, but y is at the feet of the diver


    public bool exitCutscene = false;
    private bool diverMove = true, fishMove = true;
    public bool moveOnToTwo = false;
    public bool moveOnToThree = false;
    public bool moveOnToFour = false;

    private bool supersuckerMove = false, superSuckerOn = true;
    


    // Use this for initialization
    void Start () {

        diverSpeechScript = diverSpeechBubble.GetComponent<SpeechBubble>();
        fishSpeechScript = fishSpeechBubble.GetComponent<SpeechBubble>();
        diverAnim = gameObject.GetComponent<Animator>();
        fishAnim = gameObject.GetComponent<Animator>();
        CameraAnim = GameObject.Find("Main Camera").GetComponent<Animator>();

        StartCoroutine("play");

    }
	
	// Update is called once per frame
	void FixedUpdate () {


        if (exitCutscene == true)
        {
            if(fishCurrentTarget) moveFish(fishCurrentTarget);
            if (diverCurrentTarget) moveDiver(diverCurrentTarget);
            
            
            
        }
        if (supersuckerMove)
        {
            supersucker.transform.position = Vector3.MoveTowards(supersucker.transform.position, cameraLocation.transform.position, 0.05f);
        }


    }

    IEnumerator play()
    {
        Debug.Log("starting script");
        yield return new WaitForSeconds(6);
        setDiverWave(false);

        updateDiverBubble("Hello! I'm so glad you're here!");
        showDiverBubble(true);
        fishCurrentTarget = fishTargets[1];
        yield return new WaitForSeconds(2);

        
        updateFishBubble("It's about time you showed up, my reef is getting ruined!");

        //need a way to wait for a signal from update
        while(moveOnToTwo == false)
        {
            yield return new WaitForSeconds(1);
        }

       
    }

    void moveDiver(GameObject moveTarget)
    {
        if (moveTarget.transform.position != diver.transform.position)
        {
            Vector3 direction = moveTarget.transform.position - diver.transform.position;
            diverLookAtTarget(moveTarget);
        }
        else
        {
            if (diverMove == false) stopMoveDiver();
            diverLookAtTarget(moveTarget);

        }
    }

    void stopMoveDiver()
    {
        diverAnim.SetBool("move", false);
    }

    void moveFish(GameObject moveTarget)
    {
        if (moveTarget.transform.position != fish.transform.position)
        {
            Vector3 direction = moveTarget.transform.position - fish.transform.position;
            fishLookAtTarget(moveTarget);
        }
        else
        {
            if (fishMove == false) stopMoveFish();
            fishLookAtTarget(moveTarget); 

        }
    }

    void stopMoveFish()
    {
        fishAnim.SetBool("move", false);
    }
   

    void setDiverWave(bool value)
    {
        diverAnim.SetBool("wave", value);
    }

    void showDiverBubble(bool value)
    {
        diverSpeechBubble.SetActive(value);
    }

    void updateDiverBubble(string text)
    {
        diverSpeechScript.text = text;
    }

    void destroyDiverBubble()
    {
        //run the animation to destroy the object
        showDiverBubble(false);
    }


    void showFishBubble(bool value)
    {
        fishSpeechBubble.SetActive(value);
    }

    void updateFishBubble(string text)
    {
        fishSpeechScript.text = text;
    }

    void destroyFishBubble()
    {
        //run the animation to destroy the object
        showFishBubble(false);
    }

    
    void fishLookAtTarget(GameObject target)
    {
        Vector3 newDir = Vector3.RotateTowards(fish.transform.position, target.transform.position, 0.05f, 0.05f);
        fish.transform.rotation = Quaternion.LookRotation(-newDir);
    }

    void diverLookAtTarget(GameObject target)
    {
        Vector3 newDir = Vector3.RotateTowards(diver.transform.position, target.transform.position, 0.05f, 0.05f);
        diver.transform.rotation = Quaternion.LookRotation(-newDir);
    }

    public void GetSuperSucker()
    {
        if (superSuckerOn == true)
        {
            supersucker.SetActive(true);
            supersuckerMove = true;
        }

    }




}
