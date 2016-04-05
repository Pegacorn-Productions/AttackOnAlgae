using UnityEngine;
using System.Collections;

public class coralScript : MonoBehaviour {

    public bool supersucker;
    private bool hasUrchin;
    public GameObject urchin;
    public GameObject algae;
    private GameObject algaeClone;

    // Use this for initialization
    void Start () {
        supersucker = false;
        hasUrchin = false;
        algaeClone = Instantiate(algae, new Vector3(transform.position.x, transform.position.y+0.5f, transform.position.z-0.25f), Quaternion.identity) as GameObject;
    }
	
	// Update is called once per frame
	void Update () {
        if(hasUrchin == true || supersucker == true)
        {
            Destroy(algaeClone);
            Destroy(algae);
        }
	
	}

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //for proof of concept, we can just add the urchin here
            GameObject urchinClone1 = Instantiate(urchin, new Vector3(transform.position.x, transform.position.y+2.7f, transform.position.z), Quaternion.identity) as GameObject;
            //and then set the boolean to true so we don't grow anymore algae
            hasUrchin = true;
        }

    }
}
