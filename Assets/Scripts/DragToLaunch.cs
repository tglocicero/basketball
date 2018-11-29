using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragToLaunch : MonoBehaviour {

	[SerializeField] private UIController _uiController;

	public Camera mainCamera;
	public float fireStrength;
	public float powerClamp;
	private Vector3 _pressPos;
	private Vector3 _releasePos;
	private Rigidbody2D _body;
	private Vector3 _launchVector;
	private LineRenderer _launchArrow;
	private bool mouseMoved;

	void Start() {
		_body = GetComponent<Rigidbody2D> ();
	}

	void Update() {
		if (_uiController._ammo > 0) {
		
			// Draw to Launch
			if (Input.GetMouseButtonDown (0)) {
				_pressPos = Input.mousePosition;
			}

			// Find launch vector
			_releasePos = Input.mousePosition;
			_launchVector = (mainCamera.ScreenToWorldPoint (_pressPos) - mainCamera.ScreenToWorldPoint (_releasePos)) * fireStrength;
			_launchVector = Vector2.ClampMagnitude (_launchVector, powerClamp);

			if (Input.GetMouseButton (0)) {
				gameObject.GetComponent<DrawTrajectory> ().Draw (_launchVector);
			}

			// Launch that mofo, delete the launchArrow too
			if (Input.GetMouseButtonUp (0)) {
				Messenger.Broadcast (GameEvent.SHOT_FIRED);
				_body.velocity = new Vector2 (0, 0);
				_body.AddForce (_launchVector);
			}
		}

	}
}
