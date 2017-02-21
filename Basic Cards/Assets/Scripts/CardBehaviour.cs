using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour {

	public Sprite[] cardFace;

	private SpriteRenderer spriteRenderer;
	private DeckBehaviour deckBehaviour;
	private Transform playArea;
	private GameObject deckBehaviourObject;
	private GameObject PlayAreaObject;

	private bool played;

	public void Start() {
		played = false;
		deckBehaviourObject = GameObject.FindWithTag("DeckBehaviour");
		if(deckBehaviourObject != null){
			deckBehaviour = deckBehaviourObject.GetComponent<DeckBehaviour>();
		}
		if(deckBehaviourObject == null){
			Debug.Log ("Cannot find 'DeckBehaviour'object");
		}

		PlayAreaObject = GameObject.FindWithTag("PlayArea");
		if(PlayAreaObject != null){
			playArea = PlayAreaObject.GetComponent<Transform>();
		}
		if(PlayAreaObject == null){
			Debug.Log ("Cannot find 'PlayArea'object");
		}

		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = cardFace[deckBehaviour.cardCount];
	}
	private void OnMouseDown(){
		played = false;
	}
	private void OnMouseUp(){
		if (played == true) {
			gameObject.SetActive (false);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag("PlayArea")){
			played = true;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		played = false;
	}
	

}
