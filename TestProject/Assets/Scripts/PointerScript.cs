using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PointerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.transform.name.Equals("AlgaeScreen(Clone)")) {
            Image otherImage = other.GetComponent<Image>();
            otherImage.CrossFadeAlpha(0, 0.5f, false);
        }
    }
}
