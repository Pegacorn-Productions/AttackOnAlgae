using UnityEngine;
using System.Collections;

public class TriggerSecretAnimation : MonoBehaviour {
	public bool secretTriggersActive = false;
	public bool animationStart;


	private void OnTriggerEnter(Collider other)
	{
		if (secretTriggersActive) {
			animationStart = true;
		}
		animationStart = false;//so it doesn't keep animating
	}
}
