﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour {
	public Button shuffleButton;
	public Button shuffleEverythingButton;
	public Button discardEverythingButton;
	//public DeckBehaviour deckBehav;
	public DeckBehaviour deckBehaviour;
	// Use this for initialization
	void Start () {
		shuffleButton.onClick.AddListener(pressShuffle);
		shuffleEverythingButton.onClick.AddListener(shuffleEverything);
		discardEverythingButton.onClick.AddListener(discardEverything);
		GameObject deckBehaviourObject = GameObject.FindWithTag("DeckBehaviour");				//whole block is for grabbing the Deck object so it can deal a card when clicked
			if(deckBehaviourObject != null){
				deckBehaviour = deckBehaviourObject.GetComponent<DeckBehaviour>();
			}
			if(deckBehaviourObject == null){
				Debug.Log ("Cannot find 'DeckBehaviour'object");
			}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void pressShuffle(){
		deckBehaviour.shuffleAll();
	}
	public void shuffleEverything(){
		deckBehaviour.shuffleEverything();
	}
	public void discardEverything(){
		deckBehaviour.discardEverything();
	}

}
