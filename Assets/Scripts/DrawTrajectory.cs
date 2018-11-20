using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTrajectory : MonoBehaviour {
	public LineRenderer lr;
	[SerializeField] private float _time;
	[SerializeField] private int _steps;

	void Awake() {
		Messenger.AddListener (GameEvent.SHOT_FIRED, OnShotFired);
	}

	void OnDestroy() {
		Messenger.RemoveListener (GameEvent.SHOT_FIRED, OnShotFired);
	}

	public void Draw(Vector2 force) {
//		lr.colorGradient = Color.red;

		Vector3 launchForce = force;
		Vector3 gravity = Physics2D.gravity;
		Rigidbody2D body = gameObject.GetComponent<Rigidbody2D> ();

		Vector3[] positions = new Vector3[_steps];
		Vector3[] velocities = new Vector3[_steps];

		velocities [0] = new Vector3 (0, 0); 
		positions [0] = gameObject.transform.position + velocities[0];

		velocities [1] = (launchForce + gravity) * _time * 1;
		positions [1] = positions [0] + (velocities [1] * _time * 1);

		for (int i = 2; i < _steps; i++) {
			velocities[i] = velocities[i-1] + (gravity * _time * i);
			positions[i] = positions [i-1] + (velocities [i] * _time * i);
		}
			
		lr.positionCount = _steps;
		lr.SetPositions (positions);
	}

	public void OnShotFired() {
		lr.positionCount = 0;
	}
}