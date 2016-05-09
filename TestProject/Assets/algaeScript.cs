using UnityEngine;
using System.Collections;

public class algaeScript : MonoBehaviour {

    public GameObject algaeRemovingObject;
    private GameObject algaeRemoveClone;
    public GameObject algaeSuckingPhysics;

    private float prevTime;
    private ParticleSystem ps;
    private bool gotSuperSucked = false;
    private bool called = false;

    // Use this for initialization
    void Start () {
        ps = GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {

        if (gotSuperSucked && (Time.time - prevTime > 1.0f) && (ps.maxParticles > 0))
        {
            ps.maxParticles = ps.maxParticles - 100;
            prevTime = Time.time;
        }

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "supersucker")
        {
            StartCoroutine("removeAlgae");
        }

    }

    IEnumerator removeAlgae()
    {
        algaeSuckingPhysics.GetComponent<ParticleSystem>().loop = true;
        GameObject algaeRemoveClone = Instantiate(algaeRemovingObject, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
        Debug.Log("removed og algae");
        Debug.Log(algaeSuckingPhysics.GetComponent<ParticleSystem>().loop);
        yield return new WaitForSeconds(7);
        algaeSuckingPhysics.GetComponent<ParticleSystem>().loop = false;
        Debug.Log("turned off looping...");
        Debug.Log(algaeSuckingPhysics.GetComponent<ParticleSystem>().loop);
    }

    public void getSuperSucked()
    {
        if (called == false)
        {
            gotSuperSucked = true;
            Debug.Log("Function call");
            called = true;
        }
    }
}
