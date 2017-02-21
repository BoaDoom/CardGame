using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndrawnDeck : MonoBehaviour {

	//private SpriteRenderer spriteRenderer;
	private GameObject deckBehaviourObject;
	private DeckBehaviour deckBehaviour;

	public void Start() {
		GameObject deckBehaviourObject = GameObject.FindWithTag("DeckBehaviour");
		if(deckBehaviourObject != null){
			deckBehaviour = deckBehaviourObject.GetComponent<DeckBehaviour>();
		}
		if(deckBehaviourObject == null){
			Debug.Log ("Cannot find 'DeckBehaviour'object");
		}
		//spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void OnMouseDown(){
		GameObject deckBehaviourObject = GameObject.FindWithTag("PlayArea");
		if (deckBehaviourObject != null) {
		}
		deckBehaviour.DealCard ();
	}
		
}
