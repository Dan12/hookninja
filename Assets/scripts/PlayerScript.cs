using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	// the distance joint we are going to build on the fly during the game
	private DistanceJoint2D hookJoint;

	// there is no debug draw mode to show distance joints so I am going to use a LineRenderer to draw it manually
	private LineRenderer rope;
	
	public float hookInvSpeed = 0.8f;

	private bool gameOver = false;

	// Use this for initialization
	void Start () {
		// initializing rope's LineRenderer
		rope = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		// if mouse is pressed...
		if (Input.GetButtonDown ("Fire1") && !gameOver) {
			
			// this is how I fire a ray cast to check for objects under the mouse
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			
			// if there's a collision and the colliding game object has been tagged as "Wall"...
			if (hit.collider != null && hit.collider.gameObject.CompareTag("Wall")) {

				// adding a distance joint to the player
				hookJoint = gameObject.AddComponent<DistanceJoint2D>() as DistanceJoint2D;
				
				// connecting the distance joint to the object under the mouse 
				hookJoint.connectedBody = hit.collider.GetComponent<Rigidbody2D>();
				
				// calculating the distance between the player and the object under the mouse
				float distance = Vector2.Distance(hit.transform.position, transform.position);
				
				// setting distance joint distance accordingly
				hookJoint.distance = distance;
				
				// objects connected by the joint can collide
				hookJoint.enableCollision = true;
				
				// this LineRenderer has two points, marked as "0" and "1". Setting "0" points to wall position
				rope.SetPosition(0, hit.transform.position);
			}
		}
		
		// if mouse is released...
		if (Input.GetButtonUp("Fire1") && !gameOver) {
			
			// if there is a distance joint...
			if (hookJoint) {
				
				// destroying the joint
				Destroy(hookJoint);
				
				// setting hookJoint variable to null
				hookJoint = null;
				
				// setting "0" and "1" points in the same position will cause the rope not to render
				rope.SetPosition(0, new Vector3(0, 0, 0));
				rope.SetPosition(1, new Vector3(0, 0, 0));
			}
		}
		
		// if there is a distance joint...
		if (hookJoint) {
			
			// shorten its distance by 0.05%
			hookJoint.distance = hookJoint.distance * hookInvSpeed;
			
			// setting "1" points of rope LineRenderer to player position, causing the rope to be displayed
			rope.SetPosition(1, transform.position);
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == "Ground")
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (1.5f, 0f);
	}

	public void died(){
		gameOver = false;
	}
}
