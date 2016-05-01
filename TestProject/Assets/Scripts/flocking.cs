using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Utility;

public class flocking : MonoBehaviour
{
	//You need to supply the gameObjects you want to represent the members of the flock as the "boid"
	//You also need to supply gameObjects to act as the spawn locations, 1-4
	public GameObject boid, spawnPtOne, spawnPtTwo, spawnPtThree, spawnPtFour;
	//This is to set the various sizes for the various spawn locations, so we can have the first spawn location spawn a few, the second spawn more etc
	public int flockSpawnOne, flockSpawnTwo, flockSpawnThree, flockSpawnFour;
	//This is to store the flock
	public List<GameObject> flock;
	//Values for various multipliers so we can adjust as needed
	public float repelMult, repelDistance, centerMult, centerDistance, matchMult, matchDistance, fearMult, scareRange;
	//This is the list we will use in order to keep track of things the flock should avoid (player, invisible boundaries, etc)
	public List<Collider> enemy;
	//This is the animator that will control the animations to fire for the flock, can be removed if you just want to use a simple looping animation or no animation at all
	private Animator anim;
	private float clip;
	private Random ran;
	private int temp;
	// Use this for initialization
	void Start()
	{
		temp = 0;
		//gathering up all the items the flock should avoid based on their tag
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
		for (int i = 0; i < enemies.Length; i++)
		{
			Collider col = enemies[i].GetComponent<Collider>();
			enemy.Add(col);
		}
		SpawnFlockOne();
	}


	//Call the following functions when whatever conditions are met that we want to set. Can also use this as a template for as many different spawning events as we want.
	void SpawnFlockOne()
	{
		Vector3 rPos;
		//spawning the flock at a spawn point 1
		for (int i = 0; i < flockSpawnOne; i++)
		{
			rPos = spawnPtOne.transform.position + Random.insideUnitSphere * 5;
			flock.Add(Instantiate(boid, rPos, boid.transform.rotation) as GameObject);

			Rigidbody phys = flock[flock.Count - 1].GetComponent<Rigidbody>();
			phys.velocity = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
			phys.velocity = Vector3.ClampMagnitude(phys.velocity, 1);
			phys.transform.LookAt(phys.transform.position + phys.velocity);
		}
	}

	void SpawnFlockTwo()
	{
		Vector3 rPos;
		//spawn point 2 etc
		for (int i = 0; i < flockSpawnTwo; i++)
		{
			rPos = spawnPtTwo.transform.position + Random.insideUnitSphere * 5;
			flock.Add(Instantiate(boid, rPos, boid.transform.rotation) as GameObject);

			Rigidbody phys = flock[flock.Count - 1].GetComponent<Rigidbody>();
			phys.velocity = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
			phys.velocity = Vector3.ClampMagnitude(phys.velocity, 1);
			phys.transform.LookAt(phys.transform.position + phys.velocity);
		}

	}

	void SpawnFlockThree()
	{
		Vector3 rPos;
		for (int i = 0; i < flockSpawnThree; i++)
		{
			rPos = spawnPtThree.transform.position + Random.insideUnitSphere * 5;
			flock.Add(Instantiate(boid, rPos, boid.transform.rotation) as GameObject);

			Rigidbody phys = flock[flock.Count - 1].GetComponent<Rigidbody>();
			phys.velocity = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
			phys.velocity = Vector3.ClampMagnitude(phys.velocity, 1);
			phys.transform.LookAt(phys.transform.position + phys.velocity);
		}

	}

	void SpawnFlockFour()
	{
		Vector3 rPos;
		for (int i = 0; i < flockSpawnFour; i++)
		{
			rPos = spawnPtFour.transform.position + Random.insideUnitSphere * 5;
			flock.Add(Instantiate(boid, rPos, boid.transform.rotation) as GameObject);

			Rigidbody phys = flock[flock.Count-1].GetComponent<Rigidbody>();
			phys.velocity = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
			phys.velocity = Vector3.ClampMagnitude(phys.velocity, 1);
			phys.transform.LookAt(phys.transform.position + phys.velocity);
		}
	}


	//Called oncer per frame if we wanna make sure something gets caught, catch it here, key presses, events etc
	void Update()
	{

		if (Input.GetKey("escape"))
		{
			Application.Quit();
		}

	}

	//Flocking stuff all happens here and the functions below, FixedUpdate gets used because it works better and goes on a constant time step which helps with the physics calculations
	void FixedUpdate ()
	{
		/*
        if(Input.GetMouseButtonDown(0) || Input.GetKeyUp(KeyCode.C))
        {
            Vector3 rPos = Random.insideUnitSphere * 30;
            flock.Add(Instantiate(boid, rPos, boid.transform.rotation) as GameObject);
            Rigidbody phys = flock[(flock.Count)-1].GetComponent<Rigidbody>();
            phys.velocity = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
            phys.velocity = Vector3.ClampMagnitude(phys.velocity, 1);
            phys.transform.LookAt(phys.transform.position + phys.velocity);
        }
        */

		for(int i = 0; i < flock.Count; i++)
		{
			Vector3 move = new Vector3(0,0,0);

			move += repel(flock[i]) * repelMult;
			move += cohesion(flock[i]) * centerMult;
			move += match(flock[i]) * matchMult;
			move += fear(flock[i]) * fearMult;
			move = Vector3.ClampMagnitude(move, 2);

			Rigidbody phys = flock[i].GetComponent<Rigidbody>();

			if (move.magnitude != 0)
			{
				phys.AddForce(move);
			}

			phys.velocity = Vector3.ClampMagnitude(phys.velocity, 1);
			phys.transform.LookAt(phys.transform.position + phys.velocity);
		}
	}

	Vector3 cohesion(GameObject bird)
	{
		Vector3 center = new Vector3();
		int divC = 0;

		for (int i = 0; i < flock.Count; i++)
		{
			if (flock[i] != bird && Vector3.Distance(flock[i].transform.position, bird.transform.position) < centerDistance)
			{
				center = center + flock[i].transform.position;
				divC++;
			}
		}

		center.Normalize();
		return center;
	}

	Vector3 repel(GameObject bird)
	{
		Vector3 repel = new Vector3();

		for(int i = 0; i < flock.Count; i++)
		{
			if(flock[i] != bird && (Vector3.Distance(flock[i].transform.position, bird.transform.position) < repelDistance))
			{
				repel = repel - (flock[i].transform.position - bird.transform.position);
			}
		}

		repel.Normalize();
		return repel;
	}

	Vector3 match(GameObject bird)
	{
		Vector3 match = new Vector3();

		for (int i = 0; i < flock.Count; i++)
		{
			if (flock[i] != bird && (Vector3.Distance(flock[i].transform.position, bird.transform.position) < matchDistance))
			{
				match = match + flock[i].GetComponent<Rigidbody>().velocity;

			}
		}

		match.Normalize();
		return match;
	}

	Vector3 fear(GameObject bird)
	{
		Vector3 repel = new Vector3(0, 0, 0);

		for (int i = 0; i < enemy.Count; i++)
		{
			Vector3 ping = enemy[i].ClosestPointOnBounds(bird.transform.position);
			if (Vector3.Distance(ping, bird.transform.position) < scareRange)
			{
				//Rigidbody rb = bird.GetComponent<Rigidbody>();
				Debug.Log("Predator Spotted!");
				repel = repel - (ping - bird.transform.position);
			}
		}
		repel.Normalize();
		return repel;
	}
}