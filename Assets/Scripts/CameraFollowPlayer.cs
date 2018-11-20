using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {
	public GameObject player;

	void Update () {
		gameObject.transform.position = new Vector3(gameObject.transform.position.x, player.transform.position.y + 2f, gameObject.transform.position.z);
	}
}
