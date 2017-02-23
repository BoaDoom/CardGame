using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndrawnDeck : MonoBehaviour {

	//private SpriteRenderer spriteRenderer;
	private GameObject deckBehaviourObject;
	private DeckBehaviour deckBehaviour;

	public void Start() {
		GameObject deckBehaviourObject = GameObject.FindWithTag("DeckBehaviour");				//whole block is for grabbing the Deck object so it can deal a card when clicked
		if(deckBehaviourObject != null){
			deckBehaviour = deckBehaviourObject.GetComponent<DeckBehaviour>();
		}
		if(deckBehaviourObject == null){
			Debug.Log ("Cannot find 'DeckBehaviour'object");
		}
		//spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void OnMouseDown(){																			//if the deck pile is clicked on, another card is dealt
		deckBehaviour.DealCard ();
	}
		
}
