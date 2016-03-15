using UnityEngine;
using System.Collections;

public class playerMove : MonoBehaviour {


    public GameObject Ball;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(Ball.transform.position.x, Ball.transform.position.y, Ball.transform.position.z);

        transform.LookAt(Ball.GetComponent<PlayerController>().movement * 10);
    }
}
