using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour {
	public Button shuffleButton;
	//public DeckBehaviour deckBehav;
	public DeckBehaviour deckBehaviour;
	// Use this for initialization
	void Start () {
		//Button shuffleButton = shufflebtn.GetComponent<Button>();
		Button shuffleButtonObject = GameObject.FindObjectOfType<Button>();				//whole block is for grabbing the Deck object so it can deal a card when clicked
		if(shuffleButtonObject != null){
			shuffleButton = shuffleButtonObject.GetComponent<Button>();
		}
		if(shuffleButtonObject == null){
			Debug.Log ("Cannot find 'Shuffle Button'object");
		}
		shuffleButton.onClick.AddListener(pressShuffle);
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
}
