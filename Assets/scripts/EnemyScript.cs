using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	public float maxVelocity = 3f;
	public float velSpeedup = 0.02f;

	private Rigidbody2D rigidBody;

	private bool gameOver = false;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (rigidBody.velocity.x < maxVelocity && !gameOver) {
			rigidBody.velocity = new Vector2(rigidBody.velocity.x+velSpeedup,0f);
		}

		if (GameObject.FindWithTag ("Player").transform.position.x < this.gameObject.transform.position.x + 0.7f) {
			GameObject.FindGameObjectWithTag ("GameEngine") .SendMessage ("gameOver");
			rigidBody.drag = 0.5f;
			rigidBody.isKinematic = false;
			gameOver = true;
		}
	}
}
