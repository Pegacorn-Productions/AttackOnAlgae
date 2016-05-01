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
        GameObject algaeRemoveClone = Instantiate(algaeRemovingObject, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z - 0.25f), Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(5);
        Destroy(algaeClone);
    }
}
