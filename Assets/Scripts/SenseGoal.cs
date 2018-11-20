using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenseGoal : MonoBehaviour {
	[SerializeField] private GameObject hoop;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			Vector3 sensorPos = gameObject.transform.position;
			Vector3 playerPos = other.gameObject.transform.position;
			Vector3 hoopPos = hoop.transform.position;
			if (Vector3.Distance (hoopPos, playerPos) < Vector3.Distance (sensorPos, playerPos)) {
				Messenger.Broadcast (GameEvent.GOAL_SCORED);
			}
		}
	}
}
