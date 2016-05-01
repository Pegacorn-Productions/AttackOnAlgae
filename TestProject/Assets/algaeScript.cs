using UnityEngine;
using System.Collections;

public class algaeScript : MonoBehaviour {

    public GameObject algaeRemovingObject;
    private GameObject algaeRemoveClone;
    public GameObject algaeSuckingPhysics;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
}
