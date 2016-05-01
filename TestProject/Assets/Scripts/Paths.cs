using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Paths : MonoBehaviour {

    public Color rayColor = Color.white;
    public List<Transform> path_objs = new List<Transform>();
    Transform[] wayPoints;

    void OnDrawGizmos()
    {
        Gizmos.color = rayColor;
        wayPoints = GetComponentsInChildren<Transform>();
        path_objs.Clear();
        
        foreach(Transform path_obj in wayPoints)
        {
            if(path_obj != this.transform)
            {
                path_objs.Add(path_obj);
            }
        }

        for(int i = 0; i < path_objs.Count; i++)
        {
            Vector3 position = path_objs[i].position;
            if(i > 0)
            {
                Vector3 previous = path_objs[i - 1].position;
                Gizmos.DrawLine(previous, position);
                Gizmos.DrawWireSphere(position, 0.3f);
            }
        }
    }

	// Use this for initialization
	void Start ()
    {

	
	}
	
	// Update is called once per frame
	void Update ()
    {

	
	}
}
