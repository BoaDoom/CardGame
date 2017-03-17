using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponHitContainerBehaviour : MonoBehaviour {


	List<SpriteRenderer> hitSquares;
	float heightOfHitSquares;
	float widthOfHitSquares;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}	
	public void takeInHitSquares(List<SpriteRenderer> newHitSquares, float widthOfall, float heightOfall){
		hitSquares = newHitSquares;
		heightOfHitSquares = heightOfall;
		widthOfHitSquares = widthOfall;
	}
	public void locationUpdate(Vector3 cardsPosition){
		gameObject.transform.localPosition = cardsPosition;
	}
}
