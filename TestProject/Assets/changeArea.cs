using UnityEngine;
using System.Collections;

public class changeArea : MonoBehaviour {

    public GameObject area_aumakua;
    public GameObject[] all_aumakuas;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void onTriggerEnter(Collider collision)
    {
        //if(collision.gameObject.tag == "Player")
      //  {
            Debug.Log("Changing Aumakuas!");
            foreach(GameObject aumakua in all_aumakuas)
            {
                aumakua.SetActive(false);
            }
            area_aumakua.SetActive(true);
      //  }


    }
    void onCollisionEnter(Collider other)
    {
        Debug.Log("Collided");
    }
}
