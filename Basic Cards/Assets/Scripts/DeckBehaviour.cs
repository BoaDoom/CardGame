using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeckBehaviour : MonoBehaviour {
	
	public Sprite[] cardsFaces;		//all of the sprites to use for dealing cards
	public Sprite cardBack;			//the image for the back of the cards
	public CardBehaviour card;		//the gameobject of the actual cards

	public GameObject undrawnDeck;		//the object that symbolizes the Undrawn stack of cards

	public List<CardBehaviour> deckToDrawFrom;		//the current undrawn deck of cards
	public List<CardBehaviour> discardedCards;		//the cards that are out of play and used
	public List<CardBehaviour> drawnCards;			//the cards that have been drawn and are in play
	public Transform cardStartPosition;			//the location marker for the first card drawn
	public Transform deckStartPosition;			//undrawnDeck start position
	public Transform offScreenDeck;				//the actual location for storage of all the cards in the deck //need to fix to be more efficient. Maybe not instantiate the cards untill drawn?

	//public int cardCount;

	private Vector3 spawnPosition;		//the variable location for each new card that is drawn
	public float cardGapX;				//the gap between the cards, used for spacing of the spawn points
	private float cardWidthX;			//the width of the card, used for spacing of the spawn points

	void Start () {
		//cardCount = -1;
		cardWidthX = card.transform.localScale.x;															//scale of card used for spacing
		Instantiate (undrawnDeck, deckStartPosition.position, deckStartPosition.rotation);					//making the object that symbolized the undrawn deck of cards
		undrawnDeck.GetComponent<SpriteRenderer>().sprite = cardBack;										//applying the back of the card graphic to it
		for (int i=0; i <cardsFaces.Length; i++){															//making as many cards as there are graphics for faces
			deckToDrawFrom.Add(Instantiate (card, offScreenDeck.position, cardStartPosition.rotation));		//placing these cards offscreen and also into the deckToDrawFrom list
			card.setNumber(i+1);																			//sets each card number in order
		}
		shuffleAll();
	}
	public void DealCard(){
		//int cardCount = 0;
		if (drawnCards.Count >= 5 || deckToDrawFrom.Count <= 0) {
		} 
		else {
			//cardCount
			float cardXPosition = cardStartPosition.transform.position.x + (cardWidthX + cardGapX) * drawnCards.Count;		//figures the width of the card to put down another based on how many cards have been dealt
			spawnPosition = new Vector3(cardXPosition, cardStartPosition.transform.position.y, cardStartPosition.transform.position.z);
			deckToDrawFrom [0].moveCard (spawnPosition);
			drawnCards.Add(deckToDrawFrom[0]);
			//Debug.Log (deckToDrawFrom.Count);
			deckToDrawFrom.RemoveAt(0);

			//if (cardCount >0){
			//	deckToDrawFrom[cardCount-1].SetActive(false);
			//}
		}
	}
	// Update is called once per frame
	void Update() {
		int tempCount = drawnCards.Count;
		for (int i = 0; i < tempCount; i++) {
			if (!drawnCards[i].isActiveAndEnabled) {
				discardedCards.Add(drawnCards [i]);
				drawnCards.RemoveAt(i);
				i = tempCount;
			}
		}
	}
	private void shuffleAll(){
		List<CardBehaviour> shuffledCards = new List<CardBehaviour>();
		if (deckToDrawFrom.Count >0){
			int tempCount = deckToDrawFrom.Count;
			for (int i = 0; i < tempCount; i++) { 
				discardedCards.Add(deckToDrawFrom[0]);
				deckToDrawFrom.RemoveAt(0);
				//Debug.Log (deckToDrawFrom.Count);
				//Debug.Log (discardedCards.Count);
			}
		}
		else {				
			Debug.Log ("the stack of cards being shuffled does not have any cards in it" );
			Debug.Log (deckToDrawFrom.Count);
		}

		int tempCount2 = discardedCards.Count;
		for (int i=0; i <tempCount2; i++){
			int rand = Random.Range(0,discardedCards.Count-1);
			shuffledCards.Add(discardedCards[rand]);
			discardedCards.RemoveAt(rand);
		}
		deckToDrawFrom = new List<CardBehaviour>(shuffledCards);
		shuffledCards.Clear();
	}
}
