//todo: get speech bubble to pop up for aumakua
using UnityEngine;
using System.Collections;

public class diverScript : MonoBehaviour {


    private string[] part1d = new string[2];
    private string[] part2d = new string[2];
    private string[] part3d = new string[12];
    private string[] part4d = new string[2];

	private string[] part1a = new string[2];
	private string[] part2a = new string[2];
	private string[] part3a = new string[12];//will need to break up with animations
	private string[] part4a = new string[2];

    public GameObject speechBubbleObject;
	public GameObject speechBubbleObject2;
    private SpeechBubble speechBubbleScript;
	private SpeechBubble speechBubbleScript2;
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
		speechBubbleScript2 = speechBubbleObject2.GetComponent<SpeechBubble>();
        diverAnim = gameObject.GetComponent<Animator>();
        CameraAnim = GameObject.Find("Main Camera").GetComponent<Animator>();
        target = gameObject.transform.position;

       /* part1[0] = "Hi fellow diver! Welcome to the reef of Kaneohe Bay.";
        part1[1] = "Thanks for helping me with the reef restoration project!";
        part1[2] = "Swipe your hand when you are ready to get started!";

		//urchin
        part2[0] = "Great! First I want to introduce you to this little guy here.";
        part2[1] = "This is a Collector Urchin. He's going to help us clean the reef!";
        part2[2] = "These urchin eat algae, which is great because there is a lot here.";
        part2[3] = "By eating the algae, they'll help the coral reef return to good health.";
        part2[4] = "Swipe your hand, and I'll introduce you to the algae.";

		//algae acan
        part3[0] = "This algae is called acanthophora. It grows at at fast rate";
        part3[1] = "Originally introduced by humans, it is an invasive species.";
        part3[2] = "It grows so fast, it can cover this whole area in a matter of months.";
        part3[3] = "This is bad because the algae blocks light from the coral.";
        part3[4] = "The coral don't get the nutrients they need and become unhealthy.";
        part3[5] = "In order to remove all this algae, we'll use the supersucker!";
        part3[6] = "Swipe your hand if you are ready to use the supersucker on the algae!";
        
		//super sucker
        part4[0] = "Alright! Raise your hand to call down the supersucker!";
        part4[1] = "Great! Now grab at the algae in front of you to loosen it.";
        part4[2] = "That's it! Now bring it towards the supersucker to collect it.";*/

		 part1d[0] = "Hello! I’m so glad you’re here!";
		//Amakua - *Swims up to the camera then a round to the Dive r* 
		part1a[0]  = "It’s about time you showed up, my reef is getting ruined!";
		part1d[1] = "Well, we are going to try and he lp fix it. But first let’s check in on this little";
		part1a[1]  =""; 
		//urchin *walks over to urchin*
		part2d[0] = "This is <b>Tripneustes gratilla</b> a type of Sea Urchin...";
		part2a[0]  ="It’s also called a <b>Collector Urchin!</b>";
		//Diver - *nods and crouches down* 
		part2d[1] = "This little friend eats algae , which is a good thing because there’s a lot of it here.";
		part2a [1] = "";
		//diver-*Waves towards beds of algae*
		//Amakua - *swims ove r the algae circling and saying* 
		part3d[0] = "";
		part3a[0] = "Yeah, and it’s taking over my reef!";
		part3d[1] = "But even though there are a lot of these Collector Urchins here , the y can’t keep up with how fast this algae grows.";
		part3a[1] = "";
		part3d [2] = "They were brought over by foreign ships but now they’re growing wild. In fact, they’re growing so fast that they are smothering the corals .";
		part3a[2] = "It’s <b>invasive</b>! This means its harming our ecosystem and destroying our environment!";
		//Diver- *s tands up and goe s ove r to some Algae* 
		part3d[3] = "Yep, so now we have to help out and try to remove what we can so the reef can bounce back and get healthy.";
		//*Map shows up, Amakua swims up towa rds it and sa ys*
		part3a[3] = "This is Kaneohe Bay, on Oahu. It used to be a nice reef, but lately the algae have shown up and they are taking over!";
		//*Map change s to show the a lga e showing up and taking over Kaneohe bay*
		part3d[4] = "There are three species causing problems .";
		//*Walks over to one of the coral heads covered in algae* 
		part3a[4] = "";
		part3d[5] = "This is acanthrophora spicifera";
		part3a[5] = "Some times people call it Spiny Seaweed";
		//*image shows up unde rnea th the map with a color block*
		//*map change s to show range and cove rage of a canthrophora spicife ra us ing the same color as the block*
		//*walks ove r to anothe r cora l he ad with a lgae* 
		part3d[6] = "There ’s also kappaphycus alvarezii";
		part3a[6] = "";
		//*as before , image come s up with name and color block map change s to show range*
		part3d[7] = "And lastly we’ve got, gracilera salicornia";
		//*a s before*
		part3a[7] = "It’s also called gorilla ogo!";
		part3d[8] = "So these three algae are taking over the reef, and all three were introduced by us humans.";
		part3a[8] = "Tha t’s why you should fix it!";
		//*Diver nods and knee ls down and motions to sea urchin aga in*
		part3d[9] = "Yep, we can’t depend on our friend here to do all the work, the algae grows too fast.";
		//*Diver wa lks ove r to a cora l head*
		part3a[9] = "";
		part3d[10] = "So we’ve got to pull to pull off what we can and use the Super Sucker to remove as much of it as we can.";
		part3a[10] = "What’s a Super Sucker?";
		part3d[11] = "It’s a special underwater vacuum that we can use to help clean algae off the reef! Call it down when you’re ready!";
		part3a[11] = ""; 

		part4d[0] = "Alright! Raise your hand to call down the supersucker!";
		part4a[0] = "Great! Now grab at the algae in front of you to loosen it.";
		part4d[1] = "That's it! Now bring it towards the supersucker to collect it.";
		part4a[1] = "";
		/* Diver runs around to various coral heads to direct the 
		player to suck up the algae, the 
		reef changes as this occurs and the coral slowly becomes more 
		healthy and fish begin to 
		show up from off screen and populate the area. 
		(Allow player to suck up algae until all algae in fro
		nt of them is removed, then destroy all 
		other algae before pan out. If player takes too long 
		or walk away, we can make the algae 
		propagate until they take over the whole screen and fa
		de to green.) 
		Some sort of conclusion and wrap up, Amakua says thanks etc,
		 reef looks different and 
		lots of sea life around. (Camera moves back up, can se
		e the reef with lots of fish, moves 
		above water and shows a boat full of algae) -  */


		

        StartCoroutine("partOne");

    }

    IEnumerator partOne()
    {
        Debug.Log("starting script");
        yield return new WaitForSeconds(6);
        diverAnim.SetBool("wave", false);
        //speechBubbleObject.SetActive(true);
		speechBubbleObject2.SetActive(true);

        int index = 0,index2 = 0;
		int i;
		for(i = 0; i < part1d.Length;i++) {
			//this is for the diver
			/*if(part1d[i] != "")
			{
				//hide other speech bubble
				speechBubbleScript2.addDismiss = true;

	            speechBubbleScript.text = part1d[i];
	            if (index == part1d.Length - 1) speechBubbleScript.addDismiss = true;
	            else yield return new WaitForSeconds(textspeed);
	            index++;
			}*/
			//this is for the aumakua
			if(part1a[i] != "")
			{
				//speechBubbleScript.addDismiss = true;
				//hide other speech bubble
				speechBubbleScript2.text = part1a[i];
				if (index2 == part1a.Length - 1) speechBubbleScript2.addDismiss = true;
				else yield return new WaitForSeconds(textspeed);
				index2++;
			}

        }
        speechBubbleScript.addDismiss = false;
        yield break;
    }

      IEnumerator partTwo()
    {
        exitCutscene = true;
        diverAnim.SetBool("move", true);
        speechBubbleScript.text = part2d[0];
        yield return new WaitForSeconds(1f);

        target = firstTarget.transform.position;
        move = false;

        int index = 0;
        foreach (string text in part2d)
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
        speechBubbleScript.text = part3d[0];
        yield return new WaitForSeconds(1f);

        target = secondTarget.transform.position;
        miniMap.SetActive(true);
        move = false;

        int index = 0;
        foreach (string text in part3d)
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
        foreach (string text in part4d)
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