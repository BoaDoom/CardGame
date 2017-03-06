using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour {

	//public Sprite[] cardFace;
//	private int cardNumber;
//	public float cardAttackValue;
	//public Sprite backOfCard;
	int cardSpriteNum;
	string nameOfCard; 
	int rankOfCard; 
	int attackDamageOfCard; 
	string typeOfAttack;

	//SpriteRenderer spriteRenderer;

	//private Transform playArea;
	//private GameObject deckBehaviourObject;
	private DeckBehaviour deckBehaviour;

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
	public int CardNumber{
		get{return cardSpriteNum;}
	}
	public float AttackValue{
		get{return attackDamageOfCard;}
	}

	public XMLData CardAttributes{
		set{
			cardSpriteNum = value.cardSpriteNum;
			nameOfCard = value.nameOfCard; 
			rankOfCard = value.rankOfCard; 
			attackDamageOfCard = value.attackDamageOfCard; 
			typeOfAttack = value.typeOfAttack;
		}
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
	public void hideCard(){
		gameObject.transform.localScale = new Vector3(0.0f,0.0f,0.0f);
	}
	public void deactivate(){
		gameObject.SetActive (false);
	}
	public void Update(){

	}
	

}
