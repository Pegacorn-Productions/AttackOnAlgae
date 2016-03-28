using UnityEngine;
using System.Collections;

public class follow : MonoBehaviour {

	Transform playerPos;
	Transform startPos;
	//Transform endPos;
	public float speed = 1.0f;
	private float startTime;
	private float journeyLength = 0f;
	private float distanceCovered = 0f;
	private float distanceBetween = 0f;
	Vector3 endPos;
	public GameObject Ball;

    //animation stuffs
  //  public Animator anim;
    
    private Vector3 lastPos;
    private bool playing;

	void Start () {
       
        //playerPos = GameObject.Find ("player_Rough").transform;
        //startTime = Time.time;
    }
	void Update(){
		transform.position = new Vector3(Ball.transform.position.x -1.0f, Ball.transform.position.y + 2.79f, Ball.transform.position.z - 4.0f);
        if (lastPos == transform.position)
        {
           // anim.SetInteger = 1;
            
            playing = false;
        }
        else if (playing != true)
        {
           // IdleAnim.Stop();
          //  SwimAnim.Play();
            playing = true;
        }
        //transform.Rotate (Ball.transform.rotation.x, Ball.transform.rotation.y, 0);
        /*var lookDir = Ball.transform.position - transform.position;
		lookDir.y = 0;
		transform.rotation = Quaternion.LookRotation (lookDir);*/
        lastPos = transform.position;
	}
	
	// Update is called once per frame
	/*void Update () {
	
		//transform.rotation = playerPos.rotation; //Vector3 (playerPos.rotation.x,playerPos.rotation.y,playerPos.rotation.z);
		startPos = transform;
		endPos = new Vector3 (playerPos.position.x - 1.0f, playerPos.position.y + 2.79f, playerPos.position.z - 4.0f);
		distanceBetween = Vector3.Distance (startPos.position,endPos);

		if (distanceBetween <= 3f) {
			//distanceCovered += (Time.time - startTime) * speed;
			print ("here1");
		}
		else if (distanceBetween <= 10f && distanceBetween >= 3f) {
			print ("here2");
			//distanceCovered += (Time.time - startTime) * speed;
			//float fracJourney = distanceCovered / journeyLength;

			transform.position = Vector3.Lerp (startPos.position, endPos, distanceBetween/10);
		} else {
			
			print ("here3");
			//distanceCovered = (Time.time - startTime) * speed;
			//transform.position = Vector3.Lerp (startPos.position, endPos, distanceCovered/5);
		}
		print (distanceBetween);
		
		startTime = Time.time;
	}*/
}
