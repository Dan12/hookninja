using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (transform.position.x-GameObject.FindWithTag ("MainCamera").transform.position.x < -4f) {
			//print ("Destroy");
			Destroy (gameObject);
		}
	}
}
