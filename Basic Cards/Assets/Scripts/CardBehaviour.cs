using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour {

	//public Sprite[] cardFace;
	public int cardNumber;
	public float cardAttackValue;
	//public Sprite backOfCard;

	//public SpriteRenderer spriteRenderer;
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
		//spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	}
	public void setFace(Sprite cardFaceGraphic){
		gameObject.GetComponent<SpriteRenderer>().sprite = cardFaceGraphic;
	}
	public void setNumber(int importNumber){
		cardNumber = importNumber;
		switch (cardNumber) 
		{
		case 0:
		case 5:
			cardAttackValue = 1.0f;
			break;
		case 1:
		case 6:
			cardAttackValue = 2.0f;
			break;
		case 2:
		case 7:
			cardAttackValue = 3.0f;
			break;
		case 3:
		case 8:
			cardAttackValue = 4.0f;
			break;
		case 4:
		case 9:
			cardAttackValue = 5.0f;
			break;
		}

	}
	public int getNumber(){
		return cardNumber;
	}
	public float getAttackValue(){
		return cardAttackValue;
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
			deactivate();
			deckBehaviour.updateCards ();
		}
	}

	public void deactivate(){
		gameObject.SetActive (false);
	}
	public void Update(){

	}
	

}
