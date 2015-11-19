using UnityEngine;
using System.Collections;

public class MainScript : MonoBehaviour {

	// the floor/ground prefab
	public GameObject floorObject;
	
	// the wall prefab. Player will be able to fire the hook on walls
	public GameObject wallObject;
	
	// the player prefab
	public GameObject playerObject;
	
	// thePlayer variable will be playerObject instance during the game
	private GameObject thePlayer;
	
	// the distance joint we are going to build on the fly during the game
	private DistanceJoint2D hookJoint;
	
	// there is no debug draw mode to show distance joints so I am going to use a LineRenderer to draw it manually
	private LineRenderer rope;

	public float hookInvSpeed = 0.8f;
	
	
	// Use this for initialization
	void Start () {
		
		// initializing rope's LineRenderer
		rope = GetComponent<LineRenderer>();
		
		// placing the floor object on the stage
		Instantiate(floorObject);
		
		// adjusting floor position
		floorObject.transform.position = new Vector2(0f, -2.2f);
		
		// adding the player to stage
		thePlayer = Instantiate(playerObject);
		
		// adjusting player position
		thePlayer.transform.position = new Vector2(0f, -1.8f);
		
		// generating 8 random walls
		for (int i = 0; i < 8; i++) {
			GameObject randomWall = Instantiate(wallObject);
			float screenWidth = Screen.width / 200;
			float screenHeight = Screen.height / 200;
			float randomX = Random.Range(-screenWidth, screenWidth);
			float randomY = Random.Range(-screenHeight / 2, screenHeight);
			randomWall.transform.position = new Vector2(randomX, randomY);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		// if mouse is pressed...
		if (Input.GetButtonDown ("Fire1")) {
			
			// this is how I fire a ray cast to check for objects under the mouse
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			
			// if there's a collision and the colliding game object has been tagged as "Wall"...
			if (hit.collider != null && hit.collider.gameObject.CompareTag("Wall")) {
				
				// adding a distance joint to the player
				hookJoint = thePlayer.AddComponent<DistanceJoint2D>() as DistanceJoint2D;
				
				// connecting the distance joint to the object under the mouse 
				hookJoint.connectedBody = hit.collider.GetComponent<Rigidbody2D>();
				
				// calculating the distance between the player and the object under the mouse
				float distance = Vector2.Distance(hit.transform.position, thePlayer.transform.position);
				
				// setting distance joint distance accordingly
				hookJoint.distance = distance;
				
				// objects connected by the joint can collide
				hookJoint.enableCollision = true;
				
				// this LineRenderer has two points, marked as "0" and "1". Setting "0" points to wall position
				rope.SetPosition(0, hit.transform.position);
			}
		}
		
		// if mouse is released...
		if (Input.GetButtonUp("Fire1")) {
			
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
			rope.SetPosition(1, thePlayer.transform.position);
		}
	}
}
