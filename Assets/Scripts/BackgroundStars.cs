using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundStars : MonoBehaviour {
	public GameObject star;

	void Start () {
		InvokeRepeating ("CreateStarCoroutine", 0.0f, 0.03f);
	}

	private void CreateStarCoroutine() {
		StartCoroutine (CreateStar ());
	}
	
	private IEnumerator CreateStar() {
		GameObject newStar = Instantiate (star);
		// TODO change ranges to be edge of screen translated to world space so this'll work on any screen
		float x = Random.Range (-2.5f,2.5f) + gameObject.transform.position.x;
		float y = Random.Range (-5f,10f) + gameObject.transform.position.y;
		newStar.transform.position = new Vector2 (x, y);

		yield return new WaitForSeconds (3);

		Destroy (newStar);
	}
}
