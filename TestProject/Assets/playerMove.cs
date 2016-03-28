using UnityEngine;
using System.Collections;

public class playerMove : MonoBehaviour {

    public float speed;
    public float rotationSpeed;

    private Rigidbody rb;
    private Vector3 prevlocation;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 relativePos = transform.position - prevlocation;

        if (Input.GetKey("up"))
        {
            
            
           //transform.rotation = Quaternion.LookRotation(relativePos);
            transform.position += transform.forward * speed * Time.deltaTime;

        }

        else if (Input.GetKey("down"))
        {
            
            
           // transform.rotation =  Quaternion.LookRotation(relativePos);
            transform.position -= transform.forward * speed * Time.deltaTime;
        }

        else if (Input.GetKey("left"))
        {
            
         
          //  transform.rotation = Quaternion.LookRotation(relativePos);
            transform.position -= transform.right * speed * Time.deltaTime;
        }

        else if (Input.GetKey("right"))
        {
            
         
           // transform.rotation = Quaternion.LookRotation(relativePos);
            transform.position += transform.right * speed * Time.deltaTime;
        }

      prevlocation = transform.position;



    }
}
