using UnityEngine;
using System.Collections;

public class AlgaeController : MonoBehaviour {

    private ParticleSystem ps;
    private int maxParticles;
    private ParticleSystem.MinMaxCurve emissionRate;
    private float radius;
    private float waitTime = 2; //wait 10 seconds before moving and populating
    public GameObject algae;
    private GameObject algaeClone;
    private float movementX = 0;
    private float movementY = 0;
    private float movementZ = 0;
    private int expandLimit = 2;

    private float lerpTime = 100f;
    private float currentLerpTime;
    private Vector3 source;
    private float startTime;
    private float speed = 1.0f;
    
    // Use this for initialization
	void Start () {
        ps = GetComponent<ParticleSystem>();
        emissionRate = ps.emission.rate;
        maxParticles = ps.maxParticles;
        radius = ps.shape.radius;
        source = ps.transform.position;
        startTime = Time.time;
        
	}
	
	// Update is called once per frame
	void Update () {

        if (maxParticles != 1000) {
            ps.maxParticles = maxParticles;
        }

        if (movementX != 0f || movementY != 0f || movementZ != 0f) {
           
            Vector3 destination = new Vector3(movementX, movementY, movementZ);
            float journeyLength = Vector3.Distance(source, destination  );
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;

            transform.position = Vector3.Lerp(source, destination, fracJourney);

            if (transform.position == destination) {
                movementX = 0;
                movementY = 0;
                movementZ = 0;
            }
        }

        if (Time.time < startTime + waitTime) {
            return;
        }

        if (this.expandLimit > 0) {
            algaeClone = Instantiate(algae, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
            float[] movement = new float[3] {(transform.position.x +  Random.Range(-20f, 20f)), 0f, (transform.position.z + Random.Range(-20f, 20f))};
            algaeClone.SendMessage("setMovement", movement);
            int temp = 0;
            algaeClone.SendMessage("setExpandLimit", temp);
            int temp2 = 1;
            SendMessage("setParticleNumber", temp2);
            this.expandLimit--;
        }
      
	}

    public void setMovement(float[] movement) {
        this.movementX = movement[0];
        this.movementY = movement[1];
        this.movementZ = movement[2];
    }   

    public void setExpandLimit(int limit) {
        this.expandLimit = limit;
    }

    public void setParticleNumber(int maxParticle) {
        this.maxParticles = maxParticle;        
    }

 
}
