using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    private Animation anim;

    // Use this for initialization
    void Start () {

        anim = GetComponent<Animation>();
        StartCoroutine("MoveCamera");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator MoveCamera()
    {
        yield return new WaitForSeconds(5);
        anim.Play("cameramove");
    }
}
