using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class tourScript : MonoBehaviour
{

    public string[,] script = new string[30, 2];
    private int currLine;

    public GameObject diverSpeechBubble, fishSpeechBubble;
    private SpeechBubble diverSpeechScript, fishSpeechScript;

    public float textspeed = 5;

    private Animator diverAnim, fishAnim, CameraAnim;

    public GameObject[] diverTargets, fishTargets;
    private GameObject diverCurrentTarget, fishCurrentTarget;

    public GameObject fish, diver, cameraLocation, miniMap, viewcamera, supersucker; //viewcamera is basically the camera location, but y is at the feet of the diver


    public bool exitCutscene = false;
    private bool diverMove = true, fishMove = true;
    public bool moveOnFromBreakpoint = false;
    public bool moveOnToThree = false;
    public bool moveOnToFour = false;
    private bool goScript = false;
    private bool started = false;

    private bool supersuckerMove = false, superSuckerOn = true;


    public Image FadeImg;
    public float fadeSpeed = 0.1f;
    public bool sceneStarting = false;
    public bool sceneEnding = false;
    public bool fadeinIntro1 = false;
    public bool fadeoutIntro1 = false;
    public bool fadeinIntro2 = false;
    public bool fadeoutIntro2 = false;

    public Sprite[] maps;
    public Sprite[] gestures;

    public AudioClip music;

    public GameObject map;
    public GameObject gestures_img;
    public GameObject gestures_text;
    public GameObject thanks;
    public GameObject credits;

    private bool fishFinished = false, diverFinished = false;



    // Use this for initialization
    void Start()
    {

        diverSpeechScript = diverSpeechBubble.GetComponent<SpeechBubble>();
        fishSpeechScript = fishSpeechBubble.GetComponent<SpeechBubble>();
        diverAnim = gameObject.GetComponent<Animator>();
        fishAnim = gameObject.GetComponent<Animator>();
        CameraAnim = GameObject.Find("Main Camera").GetComponent<Animator>();

        //set the script for the different characters
        script[0, 0] = "diver"; script[0, 1] = "Hello! I'm so glad you're here!";
        script[1, 0] = "aumakua"; script[1, 1] = "It's about time you showed up, my reef is getting ruined!";
        script[2, 0] = "diver"; script[2, 1] = "Well, we are going to try and help fix it. But first, let's check in on this little urchin.";
        script[3, 0] = "diver"; script[3, 1] = "This is <color=green><i>Tripneustes gratilla</i></color>, a type of sea urchin.";
        script[4, 0] = "aumakua"; script[4, 1] = "It's also called a <color=green>Collector Urchin</color>!";
        script[5, 0] = "diver"; script[5, 1] = "This little friend eats, algae, which is a good thing because there's a lot of it here.";
        script[6, 0] = "aumakua"; script[6, 1] = "Yeah, and it's taking over my reef!";
        script[7, 0] = "diver"; script[7, 1] = "But even though there are a lot of these <b>Collector Urchins</b> here, they can't keep up with how fast this algae grows.";
        script[8, 0] = "aumakua"; script[8, 1] = "It's invasive!";
        script[9, 0] = "diver"; script[9, 1] = "Yep, so now we have to help out and try to remove what we can so the reef can bounce back and get healthy.";
        script[10, 0] = "aumakua"; script[10, 1] = "This is Kaneohe Bay, on Oahu. It used to be a nice reef, but lately these algae have shown up and they are taking over!";
        script[11, 0] = "diver"; script[11, 1] = "There are three species causing problems.";
        script[12, 0] = "diver"; script[12, 1] = "We have <color=green><i>acanthrophora spicifera</i></color>.";
        script[13, 0] = "aumakua"; script[13, 1] = "Sometimes people call it <color=green>Spiny Seaweed</color>!";
        script[14, 0] = "diver"; script[14, 1] = "There’s also  <color=green><i>kappaphycus alvarezii</i></color>";
        script[15, 0] = "diver"; script[15, 1] = "And lastly we’ve got, <color=green><i>gracilera salicornia</i></color>";
        script[16, 0] = "aumakua"; script[16, 1] = "It’s also called <color=green>gorilla ogo</color>!";
        script[17, 0] = "diver"; script[17, 1] = "So these three algae are taking over the reef, and all three were introduced by us humans.";
        script[18, 0] = "aumakua"; script[18, 1] = "That’s why you should fix it!";
        script[19, 0] = "diver"; script[19, 1] = "Yep, we can’t depend on our friend here to do all the work, the algae grows too fast.";
        script[20, 0] = "diver"; script[20, 1] = "So we’ve got to pull off what we can and use the Super Sucker to remove as much of it as we can.";
        script[21, 0] = "aumakua"; script[21, 1] = "What’s a Super Sucker?";
        script[22, 0] = "diver"; script[22, 1] = "It’s a special underwater vacuum that we can use to help clean algae off the reef! Call it down when you’re ready!";
        script[23, 0] = "diver"; script[23, 1] = "Great! Now move it over the algae covered coral to suck it off!";
        script[24, 0] = "diver"; script[24, 1] = "Nice work! Only 30 more seconds to go, suck up as much algae as you can!";
        script[25, 0] = "diver"; script[25, 1] = "Excellent!! Looks like the reef is feeling better already.";
        script[26, 0] = "aumakua"; script[26, 1] = "Wow, look how many fish have returned now that the reef is healthy!";
        script[27, 0] = "diver"; script[27, 1] = "Thanks for helping us remove all of that algae. Hopefully now these <b>Collector Urchins</b> will continue to help us keep the algae under control!";
        script[28, 0] = "aumakua"; script[28, 1] = "I really appreciate your hard work. Now I can live here happily again!";
        script[29, 0] = "diver"; script[29, 1] = "I hope you had fun helping out today! I'll see you later!";






        started = true;
        currLine = 0;
        GameObject.Find("intro").GetComponent<CanvasRenderer>().SetAlpha(0.0f);
        GameObject.Find("intro 2").GetComponent<CanvasRenderer>().SetAlpha(0.0f);
        GameObject.Find("Logo test").GetComponent<CanvasRenderer>().SetAlpha(0.0f);
        GameObject.Find("gestures").GetComponent<CanvasRenderer>().SetAlpha(0.0f);



    }

    void Awake()
    {
        // Set the texture so that it is the the size of the screen and covers it.
        FadeImg.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
        sceneStarting = false;
        sceneEnding = false;
        StartCoroutine("intro");
    }
    void Update()
    {
        // If the scene is starting...
        if (sceneStarting) StartScene();
        if (sceneEnding) EndScene();

        if(fadeinIntro1) GameObject.Find("intro").GetComponent<Image>().CrossFadeAlpha(1.0f, 4.0f, false);
        if (fadeoutIntro1) GameObject.Find("intro").GetComponent<Image>().CrossFadeAlpha(-1.0f, 4.0f, false);
        if (fadeinIntro2) GameObject.Find("intro 2").GetComponent<Image>().CrossFadeAlpha(1.0f, 4.0f, false);
        if (fadeoutIntro2) GameObject.Find("intro 2").GetComponent<Image>().CrossFadeAlpha(-1.0f, 4.0f, false);
    }



    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetMouseButtonDown(0))
        {
            goScript = true;
        }

        if (Input.GetMouseButtonDown(1))
        {
           moveOnFromBreakpoint = true;
            //THIS IS FOR TESTING ONLY, THE GESTURES SHOULD TAKE CARE OF THIS ALREADY
            GetSuperSucker();

        }

        if (goScript == true && started == false)
        {
            started = true;
            StartCoroutine("play");
        }

        if (moveOnFromBreakpoint == true)
        {
            Debug.Log("Moving on to part 2!");
        }

        if (supersuckerMove)
        {
            supersucker.transform.position = Vector3.MoveTowards(supersucker.transform.position, gameObject.transform.position, 0.05f);
        }
        if (exitCutscene == false)
        {
            if (fishCurrentTarget) moveFish(fishCurrentTarget);
            if (diverCurrentTarget) moveDiver(diverCurrentTarget);



        }


    }

    IEnumerator intro()
    {
        //fade pegacorn productions presents in
        fadeinIntro1 = true;
        yield return new WaitForSeconds(5);
        //fade it out
        fadeinIntro1 = false;
        fadeoutIntro1 = true;
        yield return new WaitForSeconds(3);
        fadeoutIntro1 = false;
        //fade in the attack on algae
        fadeinIntro2 = true;
        yield return new WaitForSeconds(5);
        fadeinIntro2 = false;
        yield return new WaitForSeconds(2);
        fadeoutIntro2 = true;
        yield return new WaitForSeconds(4);
        fadeoutIntro2 = false;
        //while fading into the scene
        sceneStarting = true;
        //pan camera down
        //keep it like that until player waves
        CameraAnim.SetBool("start", true);
        GameObject.Find("Logo test").GetComponent<Image>().CrossFadeAlpha(1.0f, 3.0f, false);
        GameObject.Find("gestures").GetComponent<Image>().CrossFadeAlpha(1.0f, 3.0f, false);
        yield return new WaitForSeconds(6);
        started = false;



    }

    IEnumerator outro()
    {

        yield return new WaitForSeconds(7);

        // camera pan up and out of the water
        CameraAnim.SetBool("exit", true);
        yield return new WaitForSeconds(10);
        thanks.SetActive(true);
        //thanks for playing shows up
        // fade to black
        yield return new WaitForSeconds(5);
        thanks.SetActive(false);
        sceneEnding = true;

        //start fading in credits here!!
        credits.SetActive(true);
        credits.GetComponent<Text>().CrossFadeColor(Color.white, 4.0f, false, false);

        yield return new WaitForSeconds(10);
        //fade out creds
        credits.GetComponent<Text>().CrossFadeColor(Color.black, 3.0f, false, false);

        yield return new WaitForSeconds(5);
        


        //reload scene
        SceneManager.LoadScene(0);
    }


    IEnumerator play()
    {
       
        Debug.Log("starting script");
        setDiverWave(false);
        CameraAnim.SetBool("move1", true);
        GameObject.Find("Logo test").GetComponent<Image>().CrossFadeAlpha(-1.0f, 3.0f, false);
        GameObject.Find("gestures").GetComponent<Image>().CrossFadeAlpha(-1.0f, 3.0f, false);
        gestures_text.SetActive(false);
        yield return new WaitForSeconds(4);
        GameObject.Find("Title Screen").SetActive(false);
        GameObject.Find("gestures").GetComponent<CanvasRenderer>().SetAlpha(0.0f);

        //Move camera to final view distance
        StartCoroutine("sayNextLine"); //Diver- Hello! I’m so glad you’re here!

        yield return new WaitForSeconds(2.5f);
        //move fish to first target
        while (fishFinished == false)
        {
            yield return new WaitForSeconds(1);
        }
        fishFinished = false;

        StartCoroutine("sayNextLine"); //Amakua - *Swims up to the camera then around to the Diver* It’s about time you showed up, my reef is getting ruined!
        yield return new WaitForSeconds(2.5f);
        //move diver to first target
        while (diverFinished == false)
        {
            yield return new WaitForSeconds(1);
        }
        diverFinished = false;
        yield return new WaitForSeconds(2.5f);
        //move fish to second target
        while (fishFinished == false)
        {
            yield return new WaitForSeconds(1);
        }
        fishFinished = false;
        yield return new WaitForSeconds(1.5f);

        StartCoroutine("sayNextLine"); //Diver- Well, we are going to try and help fix it. But first let’s check in on this little urchin *walks over to urchin*
        yield return new WaitForSeconds(3.5f);

        //Diver *walks over to urchin*
        while (diverFinished == false)
        {
            yield return new WaitForSeconds(1);
        }
        diverFinished = false;

        StartCoroutine("sayNextLine"); //Diver - This is Tripneustes gratilla a type of Sea Urchin... 
        yield return new WaitForSeconds(2.5f);

        StartCoroutine("sayNextLine"); //Amakua- It’s also called a Collector Urchin!
        yield return new WaitForSeconds(2.5f);

        StartCoroutine("sayNextLine"); //Diver - *nods and crouches down* This little friend eats algae, which is a good thing because there’s a lot of it here. *Waves towards beds of algae*
        yield return new WaitForSeconds(2.5f);

        //Amakua - *swims over the algae circling*
        while (fishFinished == false)
        {
            yield return new WaitForSeconds(1);
        }
        fishFinished = false;

        StartCoroutine("sayNextLine"); //Amakua -  Yeah, and it’s taking over my reef!
        yield return new WaitForSeconds(2.5f);

        StartCoroutine("sayNextLine"); //Diver - But even though there are a lot of these Collector Urchins here, they can’t keep up with how fast this algae grows.
        yield return new WaitForSeconds(2.5f);

        StartCoroutine("sayNextLine"); //Amakua - It’s invasive!
        yield return new WaitForSeconds(2.5f);

        //Diver- *stands up and goes over to some Algae* 
        while (diverFinished == false)
        {
            yield return new WaitForSeconds(1);
        }
        diverFinished = false;

        StartCoroutine("sayNextLine"); //Diver - Yep, so now we have to help out and try to remove what we can so the reef can bounce back and get healthy.
        yield return new WaitForSeconds(2.5f);


        map.GetComponent<CanvasRenderer>().SetAlpha(0.0f);
        map.SetActive(true);
        map.GetComponent<Image>().CrossFadeAlpha(1.0f, 3.0f, false);

        //*Map shows up, Amakua swims up towards it and says*
        while (fishFinished == false)
        {
            yield return new WaitForSeconds(1);
        }
        fishFinished = false;

        StartCoroutine("sayNextLine"); // Amakua - This is Kaneohe Bay, on Oahu. It used to be a nice reef, but lately these algae have shown up and they are taking over!
        yield return new WaitForSeconds(2.5f);


        StartCoroutine("sayNextLine"); //*Map changes to show the algae showing up and taking over Kaneohe bay* Diver- There are three species causing problems.
        yield return new WaitForSeconds(2.5f);

        map.GetComponent<Image>().sprite = maps[0];

        //Diver - *Walks over to one of the coral heads covered in algae *
        while (diverFinished == false)
        {
            yield return new WaitForSeconds(1);
        }
        diverFinished = false;

        StartCoroutine("sayNextLine"); // Diver -  We have acanthrophora spicifera
        yield return new WaitForSeconds(2.5f);

        StartCoroutine("sayNextLine"); //Amakua- Sometimes people call it Spiny Seaweed *image shows up underneath the map with a color block*
        //*map changes to show range and coverage of acanthrophora spicifera using the same color as the block*
        yield return new WaitForSeconds(2.5f);


        map.GetComponent<Image>().sprite = maps[1];

        // *Diver walks over to another coral head with algae*
        while (diverFinished == false)
        {
            yield return new WaitForSeconds(1);
        }
        diverFinished = false;

        StartCoroutine("sayNextLine"); // Diver - There’s also  kappaphycus alvarezii
        //*as before, image comes up with name and color block map changes to show range*
        yield return new WaitForSeconds(2.5f);


        map.GetComponent<Image>().sprite = maps[2];
        StartCoroutine("sayNextLine"); //And lastly we’ve got, gracilera salicornia
        yield return new WaitForSeconds(2.5f);


        StartCoroutine("sayNextLine"); //Amakua - It’s also called gorilla ogo!
        yield return new WaitForSeconds(2.5f);
        map.GetComponent<Image>().sprite = maps[3];

        StartCoroutine("sayNextLine"); //Diver- So these three algae are taking over the reef, and all three were introduced by us humans.
        yield return new WaitForSeconds(2.5f);

        StartCoroutine("sayNextLine"); //Amakua- That’s why you should fix it!
        yield return new WaitForSeconds(2.5f);

        //hide the map
        map.GetComponent<Image>().CrossFadeAlpha(-1.0f, 3.0f, false);


        StartCoroutine("sayNextLine"); // *Diver nods and kneels down and motions to sea urchin again* Diver - Yep, we can’t depend on our friend here to do all the work, the algae grows too fast.
        yield return new WaitForSeconds(2.5f);
        map.SetActive(false);

        //*Diver walks over to a coral head* 
        while (diverFinished == false)
        {
            yield return new WaitForSeconds(1);
        }
        diverFinished = false;

        StartCoroutine("sayNextLine"); //Diver - So we’ve got to pull to pull off what we can and use the Super Sucker to remove as much of it as we can.
        yield return new WaitForSeconds(2.5f);


        StartCoroutine("sayNextLine"); //Amakua - What’s a Super Sucker?
        yield return new WaitForSeconds(2.5f);

        StartCoroutine("sayNextLine"); //Diver- It’s a special underwater vacuum that we can use to help clean algae off the reef! Call it down when you’re ready!
        yield return new WaitForSeconds(2.5f);
        gestures_img.GetComponent<Image>().sprite = gestures[0];
        gestures_text.GetComponent<Text>().text = "Raise your hand!";
        gestures_img.GetComponent<Image>().CrossFadeAlpha(1.0f, 3.0f, false);
        gestures_text.SetActive(true);

        //need a way to wait for a signal from update
        int counter = 0;
        while (moveOnFromBreakpoint == false)
        {
            if(counter%13 == 0)
            {
                updateDiverBubble("In order to call down the supersucker, you'll need to raise your hand!");
                showDiverBubble(true);
                yield return new WaitForSeconds(5);
                showDiverBubble(false);

            }
            else if (counter % 30 == 0)
            {
                updateDiverBubble("C'mon, what are you waiting for? Call down the supersucker so we can get started cleaning up the reef!");
                showDiverBubble(true);
                yield return new WaitForSeconds(5);
                showDiverBubble(false);

            }

            yield return new WaitForSeconds(1);
            counter++;
        }

        

        //move camera into algae patch to get cleared
        gestures_img.GetComponent<Image>().sprite = gestures[1];
        gestures_text.GetComponent<Text>().text = "Move the supersucker!";
         StartCoroutine("sayNextLine"); //Diver- Great! Now move it over the algae covered coral to suck it off!
        yield return new WaitForSeconds(2.5f);

        //yield return new WaitForSeconds(60);

        StartCoroutine("sayNextLine"); //Diver- Nice work! Only 30 more seconds to go, suck up as much algae as you can!

        //yield return new WaitForSeconds(30);

        //now the player is doing supersucker stuff, should we give them a certain number of time to complete it or just wait for them to finish completely?
        gestures_text.SetActive(false);
        gestures_img.GetComponent<Image>().CrossFadeAlpha(-1.0f, 3.0f, false);


        //make all algae disappear slowly from corals <- NOT WORKING PROPERLY, LAGS EVERYTHING AND CALLS OVER AND OVER...
        // GameObject.Find("All reefs").GetComponent<receiverScript>().fire = true;

        //then diver congratulates player
        GameObject.Find("Main Camera").GetComponent<AudioSource>().clip = music;
        GameObject.Find("Main Camera").GetComponent<AudioSource>().Play();
        supersucker.SetActive(false);
        StartCoroutine("sayNextLine"); //Diver-Excellent!! Looks like the reef is feeling better already.
        yield return new WaitForSeconds(2.5f);

        

        StartCoroutine("sayNextLine"); //Aumakua-Wow, look how many fish have returned now that the reef is healthy!.
        yield return new WaitForSeconds(2.5f);

        StartCoroutine("sayNextLine"); //Diver-Thanks for helping us remove all of that algae. Hopefully now these <b>Collector Urchins</b> will continue to help us keep the algae under control!
        yield return new WaitForSeconds(2.5f);

        StartCoroutine("sayNextLine"); //Aumakua-I really appreciate your hard work! Now I can live here happily again.
        yield return new WaitForSeconds(2.5f);

        StartCoroutine("sayNextLine"); //Diver-I Hope you had fun helping out today! I'll see you later!
        yield return new WaitForSeconds(2.5f);
        //more fish spawn
        diverAnim.SetBool("wave", true);


        Debug.Log("Got to end of play coroutine!");
        StartCoroutine("outro");


    }

    IEnumerator sayNextLine()
    {

        if (script[currLine, 0] == "diver")
        {
            updateDiverBubble(script[currLine, 1]);
            showDiverBubble(true);
            currLine++;
            yield return new WaitForSeconds(5);
            showDiverBubble(false);
        }
        else if (script[currLine, 0] == "aumakua")
        {
            updateFishBubble(script[currLine, 1]);
            showFishBubble(true);
            currLine++;
            yield return new WaitForSeconds(5);
            showFishBubble(false);
        }
        yield break;

    }

    void turnCamera(GameObject target)
    {

        cameraLocation.GetComponent<Animator>().enabled = false;
        Vector3 newDir = new Vector3(target.transform.position.x, cameraLocation.transform.position.y, target.transform.position.z);
        cameraLocation.transform.rotation = Quaternion.Slerp(cameraLocation.transform.rotation, Quaternion.LookRotation(newDir - cameraLocation.transform.position), 1 * Time.deltaTime);

    }

    void moveDiver(GameObject moveTarget)
    {

        diverAnim.SetBool("move", true);


        if (moveTarget.transform.position != diver.transform.position)
        {
            Vector3 direction = moveTarget.transform.position - diver.transform.position;
            diverLookAtTarget(moveTarget);
            diver.transform.position = Vector3.MoveTowards(diver.transform.position, moveTarget.transform.position, 0.05f);
            Debug.Log("Moving!");
            turnCamera(moveTarget);
        }
        else
        {
            Debug.Log("Not Moving!");
            stopMoveDiver();
            diverLookAtTarget(viewcamera);

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
            fish.transform.position = Vector3.MoveTowards(fish.transform.position, moveTarget.transform.position, 0.05f);
            turnCamera(moveTarget);
        }
        else
        {
            stopMoveFish();
            fishLookAtTarget(viewcamera);

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
        Vector3 newDir = new Vector3(target.transform.position.x, fish.transform.position.y, target.transform.position.z);
        fish.transform.rotation = Quaternion.LookRotation(newDir - fish.transform.position);
    }

    void diverLookAtTarget(GameObject target)
    {
        Debug.Log("turning diver!");
        Vector3 newDir = new Vector3(target.transform.position.x, diver.transform.position.y, target.transform.position.z);
        diver.transform.rotation = Quaternion.LookRotation(newDir - diver.transform.position);
    }

    public void GetSuperSucker()
    {
        if (superSuckerOn == true)
        {
            supersucker.SetActive(true);
            supersuckerMove = true;
        }

    }

    public void setGoScriptTrue()
    {
        goScript = true;
    }


    //intro/exit stuff
    void FadeToClear()
    {
        // Lerp the colour of the image between itself and transparent.
        FadeImg.color = Color.Lerp(FadeImg.color, Color.clear, fadeSpeed * Time.deltaTime);
    }


    void FadeToBlack()
    {
        // Lerp the colour of the image between itself and black.
        FadeImg.color = Color.Lerp(FadeImg.color, Color.black, fadeSpeed * Time.deltaTime);
    }


    void StartScene()
    {
        // Fade the texture to clear.
        FadeToClear();

        // If the texture is almost clear...
        if (FadeImg.color.a <= 0.05f)
        {
            // ... set the colour to clear and disable the RawImage.
            FadeImg.color = Color.clear;
            FadeImg.enabled = false;

            // The scene is no longer starting.
            sceneStarting = false;
        }
    }


    public void EndScene()
    {
        // Make sure the RawImage is enabled.
        FadeImg.enabled = true;

        // Start fading towards black.
        FadeToBlack();

        // If the screen is almost black...
        //if (FadeImg.color.a >= 0.95f)
           //show the credits
    }

    //this will get called externally when the diver is done pathing
    //put whatever you want to happen after each path is complete here
   void diverDone()
    {
        diverFinished = true;
        Debug.Log("Diver has finished a path!");
    }

    //same as Diver
    void humuDone()
    {
        fishFinished = true;
        Debug.Log("Humuhumu has finished a path!");
    }


}
