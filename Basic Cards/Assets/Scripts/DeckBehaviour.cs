﻿using System.Collections;
using System.Collections.Generic;
using System.Xml; //Needed for XML functionality
using System.Xml.Serialization; //Needed for XML Functionality
using System.IO;
using UnityEngine;


public class DeckBehaviour : MonoBehaviour {
	
	public Sprite[] cardsFaces;		//all of the sprites to use for dealing cards
	public Sprite cardBack;			//the image for the back of the cards
	public CardBehaviour card;		//the gameobject of the actual cards

	public GameObject undrawnDeck;		//the object that symbolizes the Undrawn stack of cards
	//public EnemyBehaviour enemyBehaviour;

	public List<XMLWeaponHitData> weaponHitBoxData;
	public GameControllerScript gameController;
	//public

	//public XMLloaderScript XMLloader;
	public List<XMLData> cardData;

	public List<int> orderOfDrawPile;		//the current undrawn deck of cards
	public List<int> discardedCards;		//the cards that are out of play and used
	public List<CardBehaviour> drawnCards;			//the cards that have been drawn and are in play
	public Transform cardStartPosition;			//the location marker for the first card drawn
	public Transform deckStartPosition;			//undrawnDeck start position
	public Transform offScreenDeck;				//the actual location for storage of all the cards in the deck **//need to fix to be more efficient. Maybe not instantiate the cards untill drawn?

	//public weaponHitContainerBehaviour weaponHitSquaresPrefab;

	private Vector3 tableLocation;		//the variable location for each new card that is drawn
	public float cardGapX;				//the gap between the cards, used for spacing of the spawn points
	private float cardWidthX;			//the width of the card, used for spacing of the spawn points

	public SpriteRenderer weaponHitSmallSquarePrefab;
	private ActiveSquareBehaviour smallSquareSize;		//example of the square needed for the grid targeting
	private SpriteRenderer weaponSmallSquare;
	//private Vector3 playAreaCurrentRatioSize;
	//public GridMaker PlayArea;

	void Start () {
		GameObject XMLCardLoaderObject = GameObject.FindWithTag("CardLoader");
		if(XMLCardLoaderObject != null){
			cardData = XMLCardLoaderObject.GetComponent<XMLCardLoaderScript>().data;}
		if(XMLCardLoaderObject == null){
			Debug.Log ("Cannot find 'XMLCardLoaderObject'object");}

		GameObject XMLWeaponHitLoaderScriptTEMP = GameObject.FindWithTag("HitBoxLoader");
		if(XMLWeaponHitLoaderScriptTEMP != null){
			weaponHitBoxData = XMLWeaponHitLoaderScriptTEMP.GetComponent<XMLWeaponHitLoaderScript>().data;}
		if(XMLWeaponHitLoaderScriptTEMP == null){
			Debug.Log ("Cannot find 'weaponHitBoxLoader'object");}

		GameObject playAreaTemp = GameObject.FindWithTag("PlayArea");
		if (playAreaTemp != null) {
			//PlayArea = playAreaTemp;
			//playAreaCurrentRatioSize = playAreaTemp.transform.localScale;
		}
		if(playAreaTemp == null){
			Debug.Log ("Cannot find 'playArea'object");}

//		GameObject gameControllerTemp = GameObject.FindWithTag("GameController");
//		if(XMLWeaponHitLoaderScriptTEMP != null){
//			gameController = XMLWeaponHitLoaderScriptTEMP.GetComponent<XMLWeaponHitLoaderScript>().data;}
//		if(XMLWeaponHitLoaderScriptTEMP == null){
//			Debug.Log ("Cannot find 'weaponHitBoxLoader'object");}
	
		//weaponHitSmallBoxes = new ActiveSquareBehaviour[100];
		cardWidthX = card.transform.localScale.x;															//scale of card used for spacing
		Instantiate (undrawnDeck, deckStartPosition.position, deckStartPosition.rotation);					//making the object that symbolized the undrawn deck of cards
		undrawnDeck.GetComponent<SpriteRenderer>().sprite = cardBack;										//applying the back of the card graphic to it
		for (int i=0; i < cardsFaces.Length; i++){															//making as many cards as there are graphics for faces, gets the number from the Card prefab
			orderOfDrawPile.Add(i);
		}
		shuffleAll();							//shuffles all the cards in orderOfDrawPile

//		weaponSmallSquare = Instantiate (weaponHitSmallSquarePrefab, deckStartPosition.position, deckStartPosition.rotation);
//		weaponSmallSquare.transform.localScale = smallSquareSize.transform.localScale;
//		//weaponSmallSquare.tag="weaponHitBox";
//		weaponSmallSquare.transform.localScale = new Vector3(playAreaCurrentRatioSize.x*weaponSmallSquare.transform.localScale.x,playAreaCurrentRatioSize.y*weaponSmallSquare.transform.localScale.y,1.0f);
	}
	public void DealCard(){
		for (int i=0; i < 1; i++){
			if (drawnCards.Count < 5 && orderOfDrawPile.Count > 0) {								//does not allow a dealt card if there are more than 5 cards out and active, or if the draw pile is empty
				createCard();
				relocateDrawnCards();	
			} 
			else {
				Debug.Log ("too many cards in play or too few to draw from");
			}
		}
	}
	private void createCard(){
		CardBehaviour instCard;
		instCard = Instantiate (card, offScreenDeck.position, cardStartPosition.rotation);
		drawnCards.Add(instCard);
		instCard.CardAttributes = cardData[orderOfDrawPile[0]];
		instCard.setFace(cardsFaces[(orderOfDrawPile[0])]);
		orderOfDrawPile.RemoveAt(0);
		string instAttackType = instCard.TypeOfAttack;			//matching the card's attack with the same name of attack from the database of weaponhitdata to get the matrix of what is hit
		XMLWeaponHitData hitBoxDataForCard = weaponHitBoxData.Find (XMLWeaponHitData => XMLWeaponHitData.nameOfAttack == instAttackType);
		instCard.setWeaponHitBox(hitBoxDataForCard);
	}

	private void relocateDrawnCards(){
		int tempCount = drawnCards.Count;
		for (int i = 0; i < tempCount; i++) {
			float cardXPosition = cardStartPosition.transform.position.x + (cardWidthX + cardGapX) * i;
			tableLocation = new Vector3(cardXPosition, cardStartPosition.transform.position.y, cardStartPosition.transform.position.z);			//creates a vector3 to send to the card
			drawnCards[i].moveCard(tableLocation);																						//sends coordinates to the card on where to start on the game board
		}
	}

	public void updateCards(){								//is called when there are possible cards played and need to be resorted into the discard pile
		for (int i = 0; i < drawnCards.Count; i++){ //CardBehaviour drawnCard in drawnCards) {				//runs through all drawn cards
			if (!drawnCards[i].isActiveAndEnabled) {		//checks to see which ones are still active. Ontrigger2dCollision in CardBehavior deactivates cards when put into play area @void OnTriggerStay2D(Collider2D other)
				discardedCards.Add(drawnCards[i].CardNumber);			//moves any non active cards to discarded pile
				//enemyBehaviour.takeDamage(drawnCards[i].AttackValue);
				gameController.enemyCardDamage(drawnCards[i].AttackValue);
				Destroy(drawnCards[i].gameObject);
				drawnCards.RemoveAt(i);						//removes the non active card from the drawn pile
				i--;
			}
		}
	}
	public void shuffleAll(){												//a shuffle all command. Takes every single card from the original deck and reshuffles them.
		if (orderOfDrawPile.Count > 0) {										//checks the orderOfDrawPile has cards before trying to discard the remainder
			int tempCount = orderOfDrawPile.Count;							//store the current number of cards in DeckToDrawFrom
			for (int i = 0; i < tempCount; i++) {
				discardedCards.Add (orderOfDrawPile [0]);						//adds first card in list to discard deck
				orderOfDrawPile.RemoveAt (0);									//removes first card in list of DeckToDrawFrom
			}
		} else {				
			//Debug.Log ("the stack of cards being shuffled does not have any cards in it");
			Debug.Log (orderOfDrawPile.Count);
		}
		shuffleDiscard ();													//calls the shuffle discard function every time
	}
	public void shuffleDiscard(){											//function for the times when there are cards in play that you don't want to grab when you want to reshuffle
		int tempCount = discardedCards.Count;								//stores total number of discard cards
		for (int i=0; i <tempCount; i++){									//loops for every card in discard pile
			int rand = Random.Range(0,discardedCards.Count);				//picks a random number from 0 to current number of cards of discard. Subtracted one because count starts at 1, actual deck starts at 0
			orderOfDrawPile.Add(discardedCards[rand]);
			discardedCards.RemoveAt(rand);
		}
	}
	public void shuffleEverything(){
		foreach (CardBehaviour drawnCard in drawnCards) {
			drawnCard.deactivate ();
		}
		updateCards ();
		shuffleAll ();
	}
	public void discardEverything(){
		foreach (CardBehaviour drawnCard in drawnCards) {
			drawnCard.deactivate ();
		}
		updateCards ();
	}
}
