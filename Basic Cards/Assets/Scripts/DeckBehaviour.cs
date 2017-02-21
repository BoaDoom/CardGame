using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeckBehaviour : MonoBehaviour {
	
	public Sprite[] cardsFaces;		//all of the sprites to use for dealing cards
	public Sprite cardBack;
	public GameObject card;
	public GameObject undrawnDeck;

	public List<GameObject> allDealtCards;
	public List<GameObject> shuffledCards;
	public Transform cardStartPosition;
	public Transform deckStartPosition;
	public Transform offScreenDeck;

	public int cardCount;

	private Vector3 spawnPosition;
	public float cardGapX;
	private float cardWidthX;

	public float clickRate;
	private float nextClick;

	void Start () {
		cardCount = -1;
		cardWidthX = card.transform.localScale.x;
		Instantiate (undrawnDeck, deckStartPosition.position, deckStartPosition.rotation);
		undrawnDeck.GetComponent<SpriteRenderer>().sprite = cardBack;
		for (int i=0; i <cardsFaces.Length; i++){
			allDealtCards.Add (Instantiate (card, offScreenDeck.position, cardStartPosition.rotation));
		}
		shuffle();
	}
	public void DealCard(){
		cardCount++;
		if (allDealtCards.Count >= 5) {
		} 
		else {
			float cardXPosition = cardStartPosition.transform.position.x + (cardWidthX + cardGapX) * cardCount;		//figures the width of the card to put down another based on how many cards have been dealt
			spawnPosition = new Vector3 (cardXPosition, cardStartPosition.transform.position.y, cardStartPosition.transform.position.z);

			//if (cardCount >0){
			//	allDealtCards[cardCount-1].SetActive(false);
			//}
		}
	}
	// Update is called once per frame
	void Update () {
//		if (Input.GetButton("Fire1") && Time.time > nextClick)
//		{
//			nextClick = Time.time + clickRate;
//			DealCard ();
//		}
		
	}
	private void shuffle(){
		for (int i=0; i <allDealtCards.Count; i++){
			shuffle.Add (Instantiate (card, offScreenDeck.position, cardStartPosition.rotation));
		}
	}
}
