using UnityEngine; // 41 Post - Created by DimasTheDriver on Dec/12/2011 . Part of the 'Unity: How to create a speech balloon' post. Available at: http://www.41post.com/?p=4545 
using System.Collections;

[ExecuteInEditMode]
public class SpeechBubble : MonoBehaviour 
{

	public GameObject otherObject;
	tourScript script;

	public GameObject suckerObject;
	// Animator suckerAnimator;

	//this game object's transform
	private Transform goTransform;
	//the game object's position on the screen, in pixels
	private Vector3 goScreenPos;
	//the game objects position on the screen
	private Vector3 goViewportPos;

    public static float area = Screen.height * Screen.width;

	//the width of the speech bubble
    public float bubbleWidth = Mathf.Sqrt(area) * 0.1f; 
	//the height of the speech bubble
	public float bubbleHeight = Mathf.Sqrt(area) * 0.1f;

	//an offset, to better position the bubble 
	public float offsetX = 0;
	public float offsetY = 150;

	//an offset to center the bubble 
	private float centerOffsetX;
	private float centerOffsetY;

	public int bubbleX;
	public int bubbleY;

	private bool dismissed = false;
	private bool dismissed2 = false;
	private bool dismissed3 = false;
	public int sw = Screen.width;
	public int sh = Screen.height;

	public float swr = Screen.width * 0.03f;
	public float shr = Screen.height * -0.02f;
	public float test = (area * 0.1f)/2f;

	private float oldTime = Time.time;

	//a material to render the triangular part of the speech balloon
	public Material mat;
	Material temp;

	public string text;
	public bool addDismiss = false;

	//a guiSkin, to render the round part of the speech balloon
	public GUISkin guiSkin;

	//use this for early initialization
	void Awake() 
	{
		//get this game object's transform
		goTransform = this.GetComponent<Transform>();
		script = otherObject.GetComponent<tourScript>();
	}

	//use this for initialization
	void Start()
	{
		//temp = mat;
		mat = null;
		bubbleX = Screen.width/4;//(int)((200/867) * Screen.width);
		bubbleY = Screen.height/3 *2;//(int)((200/310) * Screen.height);
		oldTime = Time.time;

		// suckerAnimator = suckerObject.GetComponent<Animator>();
		//if the material hasn't been found
		/* if (!mat) 
		{
			Debug.LogError("Please assign a material on the Inspector.");
			return;
		}*/

		//if the guiSkin hasn't been found
		if (!guiSkin) 
		{
			Debug.LogError("Please assign a GUI Skin on the Inspector.");
			return;
		}

		//Calculate the X and Y offsets to center the speech balloon exactly on the center of the game object
		centerOffsetX = bubbleWidth/2;
		centerOffsetY = bubbleHeight/2;

	}


	//Called once per frame, after the update
	void LateUpdate() 
	{
		//find out the position on the screen of this game object
		goScreenPos = Camera.main.WorldToScreenPoint(goTransform.position);	

		//Could have used the following line, instead of lines 70 and 71
		//goViewportPos = Camera.main.WorldToViewportPoint(goTransform.position);
		goViewportPos.x = goScreenPos.x/(float)Screen.width;
		goViewportPos.y = goScreenPos.y/(float)Screen.height;

	}

	//Draw GUIs
	void OnGUI()
	{
		//Begin the GUI group centering the speech bubble at the same position of this game object. After that, apply the offset
        offsetX = Screen.width * 0.10f;
        offsetY = Screen.height * 0.50f;
		GUI.BeginGroup(new Rect(goScreenPos.x-centerOffsetX-offsetX,Screen.height-goScreenPos.y-centerOffsetY-offsetY,Screen.width*1f,Screen.height*1f));

		// if (addDismiss)
		//   {

		//   }

		//If the button is pressed, dismiss
		if (!dismissed) {
			//mat = temp;

			//Render the round part of the bubble
			GUI.Label(new Rect(0,Screen.height*0.1f,Mathf.Sqrt(area) * 0.6f,Mathf.Sqrt(area) * 0.6f),"",guiSkin.customStyles[0]);

			//Render the text
			GUI.Label(new Rect(Screen.width*0.01f,Screen.height*0.15f,Mathf.Sqrt(area)*0.5f,Mathf.Sqrt(area) * 0.5f),text,guiSkin.label);
		//	if (GUI.Button(new Rect(69, 102, Mathf.Sqrt(area* 0.02f),Mathf.Sqrt(area* 0.002f)), "Dismiss"))
		//	{
			//	dismissed = true;
				// mat = null;
				//script.moveOnFromBreakpoint = true;
			//}


		}
		else if (!dismissed2)
		{
			//  mat = temp;
			//Render the round part of the bubble
			GUI.Label(new Rect(26,-6,Mathf.Sqrt(area* 0.1f),Mathf.Sqrt(area*0.1f)),"",guiSkin.customStyles[0]);

			//Render the text
			GUI.Label(new Rect(52,19,Mathf.Sqrt(area* 0.05f),Mathf.Sqrt(area* 0.05f)),text,guiSkin.label);
			if (GUI.Button(new Rect(69, 102, Mathf.Sqrt(area* 0.02f),Mathf.Sqrt(area* 0.002f)), "Dismiss"))
			{
				dismissed2 = true;
				// mat = null;
				script.moveOnToThree = true;
			}



		}
		else if (!dismissed3)
		{
			//mat = temp;
			//Render the round part of the bubble
			GUI.Label(new Rect(26,-6,Mathf.Sqrt(area* 0.1f),Mathf.Sqrt(area*0.1f)),"",guiSkin.customStyles[0]);

			//Render the text
			GUI.Label(new Rect(52,19,Mathf.Sqrt(area* 0.05f),Mathf.Sqrt(area* 0.05f)),text,guiSkin.label);
			if (GUI.Button(new Rect(69, 102, Mathf.Sqrt(area* 0.02f),Mathf.Sqrt(area* 0.002f)), "Dismiss"))
			{
				dismissed2 = true;
				// mat = null;
				script.moveOnToFour = true;
			}


		}

		GUI.EndGroup();
	}

	//Called after camera has finished rendering the scene
	void OnRenderObject()
	{
		//push current matrix into the matrix stack
		GL.PushMatrix();
		//set material pass
		if (mat != null) {
			mat.SetPass (0);
		}
		//load orthogonal projection matrix
		GL.LoadOrtho();
		//a triangle primitive is going to be rendered
		//GL.Begin(GL.TRIANGLES);

		//set the color
		GL.Color(Color.white);

		//Define the triangle vetices
		GL.Vertex3(goViewportPos.x, goViewportPos.y+(offsetY/3)/Screen.height, 0.1f);
		GL.Vertex3(goViewportPos.x - (bubbleWidth/3)/(float)Screen.width, goViewportPos.y+offsetY/Screen.height, 0.1f);
		GL.Vertex3(goViewportPos.x - (bubbleWidth/8)/(float)Screen.width, goViewportPos.y+offsetY/Screen.height, 0.1f);

		GL.End();
		//pop the orthogonal matrix from the stack
		GL.PopMatrix();
	}


	public void DismissSpeechBuble() {

        /*	if (Time.time - oldTime > 3) {
                oldTime = Time.time;
                if (dismissed == false) {
                    dismissed = true;
                    script.moveOnFromBreakpoint = true;
                }
                else if (dismissed2 == false) {
                    dismissed = true;
                    script.moveOnToThree = true;
                }
                else if (dismissed3 == false) {
                    dismissed = true;
                    script.moveOnToFour = true;
                }
            } 
            else {
                return;
            }*/
        Debug.Log("Dismiss msg");

	}
}