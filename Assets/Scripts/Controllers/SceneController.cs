using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {

	[SerializeField] private GameObject playerObject;
	public PlayerCharacter player;

	[SerializeField] public GameObject _hoop;
	private float _hoopSide;

	[SerializeField] private GameObject _ammoPickup;

	[SerializeField] private Camera _mainCamera;

	void Awake() {
		Messenger.AddListener (GameEvent.GOAL_SCORED, OnGoalScored);
		Messenger.AddListener (GameEvent.AMMO_COLLECTED, OnAmmoCollected);
	}

	void OnDestroy() {
		Messenger.RemoveListener (GameEvent.GOAL_SCORED, OnGoalScored);
		Messenger.RemoveListener (GameEvent.AMMO_COLLECTED, OnAmmoCollected);
	}
		
	void Start() {
		player = playerObject.GetComponent<PlayerCharacter> ();
		_hoopSide = -.5f;
		InstantiateHoop ();
		InstantiateAmmoPickup ();
	}
	
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Time.timeScale = .1f;
		}

		if (Input.GetMouseButtonUp (0)) {
			Time.timeScale = 1f;
			playerObject.GetComponent<Rigidbody2D> ().gravityScale = 1;
		}
	}

	void InstantiateHoop() {
		_hoop = Instantiate (_hoop);
		_hoopSide *= -1;
		Vector2 hoopPos = _mainCamera.ScreenToWorldPoint (new Vector2 (Screen.width / 2 + (Screen.width/4 * -_hoopSide), Screen.height/1.5f + Random.Range(-Screen.height/8,Screen.height/8)));
		_hoop.transform.position = hoopPos;
		_hoop.transform.localScale = new Vector3 ((_hoopSide * 2), 1, 1);
		_hoop.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0,90 * _hoopSide * -2));
	}

	void InstantiateAmmoPickup() {
		if (true) {
			_ammoPickup = Instantiate (_ammoPickup);
			Vector2 ammoPos = _mainCamera.ScreenToWorldPoint (new Vector2 (Screen.width / 2 + (Screen.width/4 * _hoopSide), Screen.height/1.5f + Random.Range(-Screen.height/8,Screen.height/8)));
			_ammoPickup.transform.position = ammoPos;
		}
	}

	private void OnGoalScored() {
		GameObject hoopAlias = _hoop;
		InstantiateHoop ();
		Destroy (hoopAlias);
	}

	private void OnAmmoCollected() {
		GameObject ammoAlias = _ammoPickup;
		InstantiateAmmoPickup ();
		Destroy (ammoAlias);
	}
}
