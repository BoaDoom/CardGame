using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour {

	int cardSpriteNum;
	string nameOfCard; 
	int rankOfCard; 
	int attackDamageOfCard; 
	string typeOfAttack;

	private DeckBehaviour deckBehaviour;
	private GridMaker gridMaker;
	private List<ActiveSquareBehaviour> hitSquares;
	private ActiveSquareBehaviour tempSquares;
	private Vector3 offSetDistance;

	//private bool clicked;
	private bool cardInPlayArea;
	private bool active;
	private Sprite storedSprite;
	private SpriteRenderer spriteRenderer;

	public void Start() {
		//ActiveSquareBehaviour[] hitSquares;
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		active = true;
		cardInPlayArea = false;
		GameObject deckBehaviourObject = GameObject.FindWithTag("DeckBehaviour");
		if(deckBehaviourObject != null){
			deckBehaviour = deckBehaviourObject.GetComponent<DeckBehaviour>();
		}
		if(deckBehaviourObject == null){
			Debug.Log ("Cannot find 'DeckBehaviour'object");
		}
		GameObject gridMakerObject = GameObject.FindWithTag("PlayArea");
		if(gridMakerObject != null){
			gridMaker = gridMakerObject.GetComponent<GridMaker>();
		}
		if(gridMakerObject == null){
			Debug.Log ("Cannot find 'gridMaker'object");
		}
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
	public string TypeOfAttack{
		get{ return typeOfAttack; }
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
	public void takeInHitSquares(List<ActiveSquareBehaviour> newHitSquares/*, float widthOfall, float heightOfall*/){
//		hitSquares = newHitSquares;
		//ActiveSquareBehaviour tempThing;
		tempSquares.transform.localPosition = newHitSquares[0].transform.localPosition;
//		offSetDistance = new Vector3(hitSquares[0].transform.localScale.x,hitSquares[0].transform.localScale.y,hitSquares[0].transform.localScale.z);
//		offSetDistance = new Vector3 (offSetDistance.x -(widthOfall/2), offSetDistance.y -(heightOfall/2), 0.0f);
//		int incriment = 0;
//		foreach (ActiveSquareBehaviour hitSquare in hitSquares) {
//			hitSquare.transform.localPosition = offSetDistance;
//		}
	}




	public void moveCard(Vector3 newPosition){
		gameObject.transform.position = newPosition;
	}
	
	private void OnMouseDown(){
		//clicked = false;
	}
	private void OnMouseUp(){
		//clicked = true;
		if (cardInPlayArea) {
			deactivate ();
			deckBehaviour.updateCards ();
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag("PlayArea") && active){
			hideCard ();
			cardInPlayArea = true;

		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.CompareTag("PlayArea") && !active){
			showCard ();
			cardInPlayArea = false;
		}
	}

	public void hideCard(){
		active = false;
		storedSprite = spriteRenderer.sprite;
		spriteRenderer.sprite = null;
	}
	public void showCard(){
		active = true;
		spriteRenderer.sprite = storedSprite;
		//storedSprite = null;
	}

	public void deactivate(){
		gameObject.SetActive (false);
	}



}
