using UnityEngine;
using System.Collections;

public class LavaScript : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D collision){
		GameObject.FindGameObjectWithTag ("GameEngine") .SendMessage ("gameOver");
	}
}
