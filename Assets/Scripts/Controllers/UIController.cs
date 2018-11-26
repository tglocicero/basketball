using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {
	[SerializeField] private Text scoreLabel;
	[SerializeField] private Text ammoLabel;
	[SerializeField] private Image empty1;
	[SerializeField] private Image empty2;
	[SerializeField] private Image empty3;
	[SerializeField] private GameObject _gameOverScreen;
	[SerializeField] private Button _restartButton;
	private int _ammo;
	private float _score;

	void Awake() {
		Messenger.AddListener (GameEvent.GOAL_SCORED, OnGoalScored);
		Messenger.AddListener (GameEvent.SHOT_FIRED, OnShotFired);
		Messenger.AddListener (GameEvent.AMMO_COLLECTED, OnAmmoCollected);
		Messenger.AddListener (GameEvent.GAME_OVER, OnGameOver);
	}

	void OnDestroy() {
		Messenger.RemoveListener (GameEvent.GOAL_SCORED, OnGoalScored);
		Messenger.RemoveListener (GameEvent.SHOT_FIRED, OnShotFired);
		Messenger.RemoveListener (GameEvent.AMMO_COLLECTED, OnAmmoCollected);
		Messenger.RemoveListener (GameEvent.GAME_OVER, OnGameOver);
	}

	void Start() {
		AddAmmo (1);
		Time.timeScale = 1;
		_restartButton.onClick.AddListener (RestartGame);
		_gameOverScreen.transform.localScale = new Vector3 (0,0,0);
	}
		
	private void OnGoalScored() {
		_score += 1;
		scoreLabel.text = _score.ToString ();
		AddAmmo (1);
	}

	private void AddAmmo(int amt) {
		_ammo += amt;

		if (_ammo < 0) {
			_ammo = 0;
			Messenger.Broadcast (GameEvent.GAME_OVER);
		} else if (_ammo > 3) {
			_ammo = 3;
		}

		if (_ammo == 0) {
			empty1.enabled = true;
			empty2.enabled = true;
			empty3.enabled = true;
		} else if (_ammo == 1) {
			empty1.enabled = false;
			empty2.enabled = true;
			empty3.enabled = true;
		} else if (_ammo == 2) {
			empty1.enabled = false;
			empty2.enabled = false;
			empty3.enabled = true;
		} else if (_ammo == 3) {
			empty1.enabled = false;
			empty2.enabled = false;
			empty3.enabled = false;
		} 
		ammoLabel.text = _ammo.ToString ();
	}

	private void OnShotFired() {
		AddAmmo(-1);
	}

	private void OnAmmoCollected() {
		AddAmmo (1);
	}
		
	private void OnGameOver() {
		_gameOverScreen.transform.localScale = new Vector3 (1,1,1);
		Time.timeScale = 0;
	}

	private void RestartGame() {
		Scene game = SceneManager.GetActiveScene ();
		SceneManager.LoadScene (game.name);
	}
}
