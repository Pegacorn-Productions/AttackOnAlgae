using UnityEngine;
using System.Collections;

public class diverScript : MonoBehaviour {


    private string[] part1 = new string[3];
    private string[] part2 = new string[5];
    private string[] part3 = new string[7];
    private string[] part4 = new string[3];

    public GameObject speechBubbleObject;
    private SpeechBubble speechBubbleScript;
    public float textspeed = 5;

    private Animator diverAnim;
    private Animator CameraAnim;
    public GameObject firstTarget;
    public GameObject secondTarget;
    public GameObject thirdTarget;
    private Vector3 target;
    public GameObject cameraLocation;
    public GameObject miniMap;

    public GameObject viewcamera;

    public bool exitCutscene = false;
    private bool move = true;
    public bool moveOnToTwo = false;
    public bool moveOnToThree = false;
    public bool moveOnToFour = false;

    public GameObject supersucker;
    private bool supersuckerMove = false;
    private bool superSuckerOn = true;

    // Use this for initialization
    void Start() {

        speechBubbleScript = speechBubbleObject.GetComponent<SpeechBubble>();
        diverAnim = gameObject.GetComponent<Animator>();
        CameraAnim = GameObject.Find("Main Camera").GetComponent<Animator>();
        target = gameObject.transform.position;

        part1[0] = "Hi fellow diver! Welcome to the reef of Kaneohe Bay.";
        part1[1] = "Thanks for helping me with the reef restoration project!";
        part1[2] = "Swipe your hand when you are ready to get started!";

        part2[0] = "Great! First I want to introduce you to this little guy here.";
        part2[1] = "This is a Collector Urchin. He's going to help us clean the reef!";
        part2[2] = "These urchin eat algae, which is great because there is a lot here.";
        part2[3] = "By eating the algae, they'll help the coral reef return to good health.";
        part2[4] = "Swipe your hand, and I'll introduce you to the algae.";

        part3[0] = "This algae is called acanthophora. It grows at at fast rate";
        part3[1] = "Originally introduced by humans, it is an invasive species.";
        part3[2] = "It grows so fast, it can cover this whole area in a matter of months.";
        part3[3] = "This is bad because the algae blocks light from the coral.";
        part3[4] = "The coral don't get the nutrients they need and become unhealthy.";
        part3[5] = "In order to remove all this algae, we'll use the supersucker!";
        part3[6] = "Swipe your hand if you are ready to use the supersucker on the algae!";

        part4[0] = "Alright! Raise your hand to call down the supersucker!";
        part4[1] = "Great! Now grab at the algae in front of you to loosen it.";
        part4[2] = "That's it! Now bring it towards the supersucker to collect it.";

        StartCoroutine("partOne");

    }

    IEnumerator partOne()
    {
        Debug.Log("starting script");
        yield return new WaitForSeconds(6);
        diverAnim.SetBool("wave", false);
        speechBubbleObject.SetActive(true);

        int index = 0;
        foreach (string text in part1) {
            speechBubbleScript.text = text;
            if (index == part1.Length - 1) speechBubbleScript.addDismiss = true;
            else yield return new WaitForSeconds(textspeed);
            index++;

        }
        speechBubbleScript.addDismiss = false;
        yield break;
    }

    IEnumerator partTwo()
    {
        exitCutscene = true;
        diverAnim.SetBool("move", true);
        speechBubbleScript.text = part2[0];
        yield return new WaitForSeconds(1f);

        target = firstTarget.transform.position;
        move = false;

        int index = 0;
        foreach (string text in part2)
        {
            Debug.Log(text);
            speechBubbleScript.text = text;
            //if (index == part2.Length-1) speechBubbleScript.addDismiss = true;
            yield return new WaitForSeconds(textspeed);
            index++;

        }


    }

    IEnumerator partThree()
    {
        exitCutscene = true;
        move = true;
        diverAnim.SetBool("move", true);
        speechBubbleScript.text = part3[0];
        yield return new WaitForSeconds(1f);

        target = secondTarget.transform.position;
        miniMap.SetActive(true);
        move = false;

        int index = 0;
        foreach (string text in part3)
        {
            Debug.Log(text);
            speechBubbleScript.text = text;
            //if (index == part2.Length-1) speechBubbleScript.addDismiss = true;
            yield return new WaitForSeconds(textspeed);
            index++;

        }

    }
    IEnumerator partFour()
    {
        CameraAnim.SetBool("tosupersucker", true);
        superSuckerOn = true;
        int index = 0;
        foreach (string text in part4)
        {
            Debug.Log(text);
            speechBubbleScript.text = text;
            //if (index == part2.Length-1) speechBubbleScript.addDismiss = true;
            yield return new WaitForSeconds(textspeed);
            index++;

        }

    }



    // Update is called once per frame
    void FixedUpdate () {
        if (Input.GetKey(KeyCode.R)) {
            GetSuperSucker();
        }
        Vector3 diverdirection = speechBubbleObject.transform.position - viewcamera.transform.position;
        viewcamera.transform.rotation = Quaternion.LookRotation(diverdirection);

        if (moveOnToTwo == true)
        {
            StartCoroutine("partTwo");
            moveOnToTwo = false;
        }
        if (moveOnToThree == true)
        {
            StartCoroutine("partThree");
            moveOnToThree = false;
        }

        if(moveOnToFour == true)
        {
            StartCoroutine("partFour");
            moveOnToFour = false;
        }

        if (supersuckerMove)
        {
            supersucker.transform.position = Vector3.MoveTowards(supersucker.transform.position, gameObject.transform.position, 0.05f);
        }


        if (exitCutscene == true)
        {
            

            if (target != gameObject.transform.position)
            {
                Vector3 direction = target - transform.position;
                transform.rotation = Quaternion.LookRotation(direction);
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target, 0.05f);
            }
            else
            {
                if(move == false) diverAnim.SetBool("move", false);
                Vector3 newDir = Vector3.RotateTowards(transform.position, cameraLocation.transform.position, 0.05f, 0.05f);
                transform.rotation = Quaternion.LookRotation(-newDir);
            }
        }

        
	}

    public void GetSuperSucker()
    {
        if (superSuckerOn == true) {
            supersucker.SetActive(true);
            supersuckerMove = true;
        }
  
    }
}
