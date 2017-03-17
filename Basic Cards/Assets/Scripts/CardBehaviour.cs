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
	private weaponHitContainerBehaviour weaponHitSquares;
	private ActiveSquareBehaviour tempSquares;
//	private Vector3 offSetDistance;
//	float heightOfHitSquares;
//	float widthOfHitSquares;

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
	public void takeInHitContainer(weaponHitContainerBehaviour newHitSquares){
		weaponHitSquares = newHitSquares;
//		heightOfHitSquares = heightOfall;
//		widthOfHitSquares = widthOfall;

	}
//	public void takeInHitSquares(List<ActiveSquareBehaviour> newHitSquares, float widthOfall, float heightOfall){
//		hitSquares = newHitSquares;
//		heightOfHitSquares = heightOfall;
//		widthOfHitSquares = widthOfall;
//
//	}

	void Update(){
		weaponHitSquares.locationUpdate (gameObject.transform.localPosition);
////		//Debug.Log(hitSquares[0].transform.localPosition);
////		//Debug.Log(gameObject.transform.localPosition);
////		offSetDistance = hitSquares[0].transform.localPosition - gameObject.transform.localPosition;
////		//Debug.Log(offSetDistance);
////		offSetDistance = new Vector3 (offSetDistance.x -(widthOfHitSquares/2), offSetDistance.y -(heightOfHitSquares/2), 0.0f);
////		int incriment = 0;
//		foreach (ActiveSquareBehaviour hitSquare in hitSquares) {
//			hitSquare.transform.localPosition = hitSquare.transform.localPosition + offSetDistance;
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
