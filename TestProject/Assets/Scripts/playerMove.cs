using UnityEngine;
using System.Collections;

public class playerMove : MonoBehaviour {

    public float speed;
    public float rotationSpeed;
    public GameObject superSucker;


    private Rigidbody rb;
    private Vector3 prevlocation;

    private bool superSuckerOn;
    private GameObject mySuperSucker;

    // Use this for initialization
    void Start()
    {
       // rb = GetComponent<Rigidbody>();
        superSuckerOn = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.R)) {
            GetSuperSucker();
        }

        if (Input.GetKey(KeyCode.T)) {
            RemoveSuperSucker();
        }

    }

    public void GetSuperSucker() {
        if (superSuckerOn == false) {
            Debug.Log("Getting the super sucker");
            mySuperSucker = Instantiate(superSucker, this.transform.position, Quaternion.identity) as GameObject;
            superSuckerOn = true;
        }
    }

    public void RemoveSuperSucker() {
        superSuckerOn = false;
        Destroy(mySuperSucker);
    }

}
