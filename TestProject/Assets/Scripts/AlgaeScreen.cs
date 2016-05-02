using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AlgaeScreen : MonoBehaviour {

    public Image algaeScreen;

    public bool spawnAlgae = true;
    private float oldTime;


    // Use this for initialization
    void Start() {
        oldTime = Time.time;
    }

    // Update is called once per frame
    void Update() {
  
        if (Time.time - oldTime > 3 && spawnAlgae == true) {
            oldTime = Time.time;
            Image newAlgae = Instantiate(algaeScreen);
            newAlgae.CrossFadeAlpha(0, 0, false);
            newAlgae.CrossFadeAlpha(1, 1f, false);

            newAlgae.transform.SetParent(this.transform, false);
            float randomValue1 = Random.Range(1, 3);
            float randomValue2 = Random.Range(1, 3);
            Vector3 position;

            if (randomValue1 % 2 == 0) {
                if (randomValue2 % 2 == 0) {
                    position = new Vector3(0, Random.Range(0, Screen.height), 0);
                }
                else {
                    position = new Vector3(Screen.width, Random.Range(0, Screen.height), 0);
                }
            }
            else {
                if (randomValue2 % 2 == 0) {
                    position = new Vector3(Random.Range(0, Screen.width), 0, 0);
                }
                else {
                    position = new Vector3(Random.Range(0, Screen.width), Screen.height, 0);
                }
            }
            newAlgae.transform.position = position;
        }
    }

    /// <summary>
    /// Used to set the spawning of algae on or off on the screen.
    /// Set to false if you don't want any more algae spawned.
    /// </summary>
    /// <param name="value"></param>
    public void setSpawnAlgae(bool value) {
        spawnAlgae = value;
    }

    /// <summary>
    /// Destroys all the algae on screen.
    /// </summary>
    public void destroyAllAlgae() {
        foreach (Transform child in this.transform) {
            Debug.Log(child.ToString());
            if (child.name.Equals("AlgaeScreen(Clone)")) {
                child.GetComponent<Image>().CrossFadeAlpha(0, 1f, false);
            }
        }
    }


}