using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour {
	public Button shuffleDiscardsButton;
	public Button shuffleEverythingButton;
	public Button discardEverythingButton;
	//public DeckBehaviour deckBehav;
	public DeckScript deckController;
	public PlayAreaScript playAreaController;
	public EnemyScript enemyController;

	public CurrentWeaponHitBox currentClickedOnCardWeaponMatrix;
	//private bool boolCardClickedOn;

	void Start () {
		//boolCardClickedOn = false;
		currentClickedOnCardWeaponMatrix = new CurrentWeaponHitBox(false, null, 0);
		shuffleDiscardsButton.onClick.AddListener(shuffleDiscard);
		shuffleEverythingButton.onClick.AddListener(discardDrawThenShuffle);
		discardEverythingButton.onClick.AddListener(discardAllActiveShuffle);
		GameObject deckControllerObjectTemp = GameObject.FindWithTag("DeckController");				//whole block is for grabbing the Deck object so it can deal a card when clicked
		if(deckControllerObjectTemp != null){
			deckController = deckControllerObjectTemp.GetComponent<DeckScript>();
			}
		if(deckControllerObjectTemp == null){
			Debug.Log ("Cannot find 'deckController'object");
			}
		GameObject playAreaControllerTemp = GameObject.FindWithTag("PlayAreaController");
		if(playAreaControllerTemp != null){
			playAreaController = playAreaControllerTemp.GetComponent<PlayAreaScript>();
		}
		if(playAreaControllerTemp == null){
			Debug.Log ("Cannot find 'DeckBehaviour'object");
		}
		
	}

	public void cardClickedOn(XMLWeaponHitData WeaponHitMatrix, int weaponDamage){		//command sent from the CardBehaviour script with info about the damage its doing
		currentClickedOnCardWeaponMatrix = new CurrentWeaponHitBox(true, WeaponHitMatrix, weaponDamage);
		playAreaController.hardResetSmallSquares ();
		//boolCardClickedOn = true;
	}
	public void cardClickedOff(){				//sent from the cardbehaviour
		playAreaController.softResetSmallSquares ();			//resets all the targetting squares if the card is released. If not in place, used cards never 'exit'
		currentClickedOnCardWeaponMatrix.isCardClickedOn = false;
	}

	public void discardDrawThenShuffle(){
		deckController.discardDrawThenShuffle();		//puts all draw pile cards into the discard and then shuffles discard
	}
	public void shuffleDiscard(){					//only shuffles discard
		deckController.shuffleDiscard();
	}
	public void discardAllActiveShuffle(){			//discards all active cards and cards in draw pile and then shuffles
		deckController.discardAllActiveShuffle();
	}


	public void enemyCardDamage(){		//is sent by the deckbahviour script that the active card was just played
//		Debug.Log("target: " +playAreaController.getActiveSquareStateSoftTarget(0,0));
//		Debug.Log("occupied: " +playAreaController.getActiveSquareStateOccupied(0,0));
		//enemyController.takeDamage (currentClickedOnCardWeaponMatrix);
		Vector2 gridDimensions = playAreaController.getGridDimensions();
		for (int x = 0; x < gridDimensions.x; x++) {
			for (int y = 0; y < gridDimensions.y; y++) {
				if (playAreaController.getActiveSquareStateSoftTarget(x,y) && playAreaController.getActiveSquareStateOccupied(x,y)){
					enemyController.takeDamage (currentClickedOnCardWeaponMatrix);
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
