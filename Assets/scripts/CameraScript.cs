using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	private float xOffset;
	private float prevX;
	
	// Use this for initialization
	void Start () {
		xOffset = (transform.position-GameObject.FindWithTag("Player").transform.position).x;
		prevX = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		float newX = (GameObject.FindWithTag ("Player").transform.position.x - xOffset);
		if(newX > prevX){
			transform.position = new Vector3(newX,0f,-10f);
			prevX = newX;
		}
	}
}
