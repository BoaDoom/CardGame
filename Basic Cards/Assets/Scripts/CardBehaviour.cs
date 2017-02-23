using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour {

	//public Sprite[] cardFace;
	public int cardNumber;
	//public Sprite backOfCard;

	private SpriteRenderer spriteRenderer;
	//private DeckBehaviour deckBehaviour;
	private Transform playArea;
	private GameObject deckBehaviourObject;
	private DeckBehaviour deckBehaviour;
	//private GameObject PlayAreaObject;

	private bool played;

	public void Start() {
		played = false;
		GameObject deckBehaviourObject = GameObject.FindWithTag("DeckBehaviour");
		if(deckBehaviourObject != null){
			deckBehaviour = deckBehaviourObject.GetComponent<DeckBehaviour>();
		}
		if(deckBehaviourObject == null){
			Debug.Log ("Cannot find 'DeckBehaviour'object");
		}
	}
	public void setFace(Sprite cardFaceGraphic){
		spriteRenderer = GetComponent<SpriteRenderer> ();
		spriteRenderer.sprite = cardFaceGraphic;
	}
	public void setNumber(int number){
		cardNumber = number;
	}

	public void moveCard(Vector3 newPosition){
		gameObject.transform.position = newPosition;
	}
	
	private void OnMouseDown(){
		played = false;
	}
	private void OnMouseUp(){
		played = true;
	}
	void OnTriggerStay2D(Collider2D other){
		if (other.CompareTag("PlayArea") && played){
			gameObject.SetActive (false);
			deckBehaviour.updateCards ();
		}
	}
	public void Update(){

	}
	

}
