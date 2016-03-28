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

        Vector3 relativePos = prevlocation - transform.position;

        if (Input.GetKey("up"))
        {


            //transform.rotation = Quaternion.LookRotation(relativePos);
            transform.forward = new Vector3(0f, 0f, 1f);
            Vector3 newTransform = transform.position + transform.forward * speed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, newTransform, 1);

        }

        else if (Input.GetKey("down"))
        {


            // transform.rotation =  Quaternion.LookRotation(relativePos);
            transform.forward = new Vector3(0f, 0f, -1f);
            //transform.position += transform.forward * speed * Time.deltaTime;
            Vector3 newTransform = transform.position + transform.forward * speed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, newTransform, 1);
        }

        else if (Input.GetKey("left"))
        {


            //  transform.rotation = Quaternion.LookRotation(relativePos);
          
            transform.forward = new Vector3(-1f, 0f, 0f);
            //transform.position += transform.forward * speed * Time.deltaTime;
            Vector3 newTransform = transform.position + transform.forward * speed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, newTransform, 1);
        }

        else if (Input.GetKey("right"))
        {

            // transform.rotation = Quaternion.LookRotation(relativePos);
           
            transform.forward = new Vector3(1f, 0f, 0f);

            //transform.position += transform.forward * speed * Time.deltaTime;
            Vector3 newTransform = transform.position + transform.forward * speed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, newTransform, 1);
        }

      prevlocation = transform.position;



    }
}
