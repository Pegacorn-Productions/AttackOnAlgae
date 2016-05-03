using UnityEngine;
using System.Collections;

public class coralScript : MonoBehaviour {

    public bool supersucker;
    private bool hasUrchin;
    public GameObject urchin;
    public GameObject algae;
    private GameObject algaeClone;
    public GameObject algaeRemovingObject;
    private GameObject algaeRemoveClone;
    public GameObject algaeSuckingPhysics;
    public Material healthyCoral;
    private bool removed = false;
    private bool called = false;


    private float prevTime;
    private ParticleSystem ps;
    private bool gotSuperSucked = false;


    // Use this for initialization
    void Start () {
        ps = GetComponent<ParticleSystem>();
        supersucker = false;
        hasUrchin = false;
        algaeClone = Instantiate(algae, new Vector3(transform.position.x, transform.position.y+0.5f, transform.position.z-0.25f), Quaternion.identity) as GameObject;
        algaeClone.transform.parent = GameObject.Find("All reefs").transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (gotSuperSucked && (Time.time - prevTime > 1.0f) && (ps.maxParticles > 0))
        {
            ps.maxParticles = ps.maxParticles - 500;
            prevTime = Time.time;
        }

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "supersucker")
        {
            if (removed == false) { 
              StartCoroutine("removeAlgae");
              removed = true;
             }
        }

    }

    IEnumerator removeAlgae()
    {
        algaeSuckingPhysics.GetComponent<ParticleSystem>().loop = true;
        GameObject algaeRemoveClone = Instantiate(algaeRemovingObject, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z - 0.25f), Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(5);
        Destroy(algaeClone);
        Debug.Log("removed og algae");
        yield return new WaitForSeconds(7);
        algaeSuckingPhysics.GetComponent<ParticleSystem>().loop = false;
        Debug.Log("turned off looping...");
        yield return new WaitForSeconds(3);
        gameObject.GetComponent <MeshRenderer>().material = healthyCoral;
    }



    public void getSuperSucked()
    {
      
            gotSuperSucked = true;
            Debug.Log("Function call");
            gameObject.GetComponent<MeshRenderer>().material = healthyCoral;
            called = true;

    }
}
