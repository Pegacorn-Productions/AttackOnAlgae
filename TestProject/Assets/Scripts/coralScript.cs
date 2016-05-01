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

    // Use this for initialization
    void Start () {
        supersucker = false;
        hasUrchin = false;
        algaeClone = Instantiate(algae, new Vector3(transform.position.x, transform.position.y+0.5f, transform.position.z-0.25f), Quaternion.identity) as GameObject;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "supersucker")
        {
            StartCoroutine("removeAlgae");
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
        yield return new WaitForSeconds(5);
        gameObject.GetComponent <MeshRenderer>().material = healthyCoral;
    }
}
