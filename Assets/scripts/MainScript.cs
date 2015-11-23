using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainScript : MonoBehaviour {

	// the floor/ground prefab
	public GameObject floorObject;
	
	// the wall prefab. Player will be able to fire the hook on walls
	public GameObject wallObject;
	
	// the player prefab
	public GameObject playerObject;

	//lava
	public GameObject lavaObject;

	//enemy
	public GameObject enemyObject;

	public Text diedText;

	private float wallXPos = 0f;

	private float pieceWidth = 1f;
	private float[] xChange = {0.5f,1.5f};
	private float[] yChange = {-1f,2f};

	private float lavaPos;

	// Use this for initialization
	void Awake () {

		lavaPos = pieceWidth;
		
		// placing the floor object on the stage
		Instantiate(floorObject);
		
		// adjusting floor position
		floorObject.transform.position = new Vector2(0f, -2.2f);
		
		// adding the player to stage
		GameObject thePlayer = Instantiate(playerObject);
		
		// adjusting player position
		thePlayer.transform.position = new Vector2 (0f, -1.8f);

		GameObject enemy = Instantiate (enemyObject);
		enemy.transform.position = new Vector2 (-3.6f,0f);

		placeWall ();

		placeLava ();
	}

	void placeWall(){
		GameObject randomWall = Instantiate(wallObject);
		float randomX = Random.Range(wallXPos+xChange[0], wallXPos+xChange[1]);
		float randomY = Random.Range(yChange[0],yChange[1]);
		randomWall.transform.position = new Vector2(randomX, randomY);
		wallXPos = randomX;
		if (wallXPos < GameObject.FindWithTag ("MainCamera").transform.position.x+5f)
			placeWall ();
	}

	void placeLava(){
		int numPlace = Mathf.RoundToInt(Random.Range (0, 2));
		GameObject newPeice = null;
		if(numPlace < 1)
			newPeice = Instantiate(lavaObject);
		else
			newPeice = Instantiate(floorObject);
		newPeice.transform.position = new Vector2(lavaPos, -2.2f);
		lavaPos+= pieceWidth;
		if (lavaPos < GameObject.FindWithTag ("MainCamera").transform.position.x+5f)
			placeLava ();
	}
	
	// Update is called once per frame
	void Update () {
		if (wallXPos < GameObject.FindWithTag ("MainCamera").transform.position.x+5f)
			placeWall ();

		if (lavaPos < GameObject.FindWithTag ("MainCamera").transform.position.x+5f)
			placeLava ();

		//print (GameObject.FindWithTag ("MainCamera").transform.position.x);
		//print (wallXPos);
	}

	public void gameOver(){
		print ("GameOver");
		GameObject.FindGameObjectWithTag ("Player") .SendMessage ("died");
		diedText.enabled = true;
	}
}
