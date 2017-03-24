using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour {
	public Button shuffleDiscardsButton;
	public Button shuffleEverythingButton;
	public Button discardEverythingButton;
	//public DeckBehaviour deckBehav;
	public DeckBehaviour deckBehaviour;
	public PlayArea playArea;
	public EnemyBehaviour enemyBehaviour;

	public CurrentWeaponHitBox currentClickedOnCardWeaponMatrix;
	//private bool boolCardClickedOn;

	void Start () {
		//boolCardClickedOn = false;
		currentClickedOnCardWeaponMatrix = new CurrentWeaponHitBox(false, null, 0);
		shuffleDiscardsButton.onClick.AddListener(shuffleDiscard);
		shuffleEverythingButton.onClick.AddListener(discardDrawThenShuffle);
		discardEverythingButton.onClick.AddListener(discardAllActiveShuffle);
		GameObject deckBehaviourObject = GameObject.FindWithTag("DeckBehaviour");				//whole block is for grabbing the Deck object so it can deal a card when clicked
			if(deckBehaviourObject != null){
				deckBehaviour = deckBehaviourObject.GetComponent<DeckBehaviour>();
			}
			if(deckBehaviourObject == null){
				Debug.Log ("Cannot find 'DeckBehaviour'object");
			}
		GameObject playAreaObject = GameObject.FindWithTag("PlayArea");
		if(playAreaObject != null){
			playArea = playAreaObject.GetComponent<PlayArea>();
		}
		if(playAreaObject == null){
			Debug.Log ("Cannot find 'DeckBehaviour'object");
		}
		
	}

	public void cardClickedOn(XMLWeaponHitData WeaponHitMatrix, int weaponDamage){		//command sent from the CardBehaviour script with info about the damage its doing
		currentClickedOnCardWeaponMatrix = new CurrentWeaponHitBox(true, WeaponHitMatrix, weaponDamage);
		playArea.hardResetSmallSquares ();
		//boolCardClickedOn = true;
	}
	public void cardClickedOff(){				//sent from the cardbehaviour
		playArea.softResetSmallSquares ();			//resets all the targetting squares if the card is released. If not in place, used cards never 'exit'
		currentClickedOnCardWeaponMatrix.isCardClickedOn = false;
	}

	public void discardDrawThenShuffle(){
		deckBehaviour.discardDrawThenShuffle();		//puts all draw pile cards into the discard and then shuffles discard
	}
	public void shuffleDiscard(){					//only shuffles discard
		deckBehaviour.shuffleDiscard();
	}
	public void discardAllActiveShuffle(){			//discards all active cards and cards in draw pile and then shuffles
		deckBehaviour.discardAllActiveShuffle();
	}


	public void enemyCardDamage(){		//is sent by the deckbahviour script that the active card was just played
//		Debug.Log("target: " +playArea.getActiveSquareStateSoftTarget(0,0));
//		Debug.Log("occupied: " +playArea.getActiveSquareStateOccupied(0,0));
		//enemyBehaviour.takeDamage (currentClickedOnCardWeaponMatrix);
		Vector2 gridDimensions = playArea.getGridDimensions();
		for (int x = 0; x < gridDimensions.x; x++) {
			for (int y = 0; y < gridDimensions.y; y++) {
				if (playArea.getActiveSquareStateSoftTarget(x,y) && playArea.getActiveSquareStateOccupied(x,y)){
					enemyBehaviour.takeDamage (currentClickedOnCardWeaponMatrix);
				}
			}
		}
	}

}
public class CurrentWeaponHitBox{
	public bool isCardClickedOn;
	public XMLWeaponHitData weaponHitData;
	public int weaponDamage;
	public CurrentWeaponHitBox(bool incomingCardClickedData, XMLWeaponHitData incomingWeaponHitData, int weaponDamageT){
		isCardClickedOn = incomingCardClickedData;
		weaponHitData = incomingWeaponHitData;
		weaponDamage = weaponDamageT;
	}
}
