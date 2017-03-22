using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour {
	public Button shuffleButton;
	public Button shuffleEverythingButton;
	public Button discardEverythingButton;
	//public DeckBehaviour deckBehav;
	public DeckBehaviour deckBehaviour;
	public GridHitController playArea;
	public EnemyBehaviour enemyBehaviour;

	public CurrentWeaponHitBox currentClickedOnCardWeaponMatrix;
	//private bool boolCardClickedOn;

	void Start () {
		//boolCardClickedOn = false;
		currentClickedOnCardWeaponMatrix = new CurrentWeaponHitBox(false, null);
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
		GameObject playAreaObject = GameObject.FindWithTag("PlayArea");
		if(playAreaObject != null){
			playArea = playAreaObject.GetComponent<GridHitController>();
		}
		if(playAreaObject == null){
			Debug.Log ("Cannot find 'DeckBehaviour'object");
		}
		
	}
	void Update(){
	}
	public void cardClickedOn(XMLWeaponHitData WeaponHitMatrix){
		currentClickedOnCardWeaponMatrix = new CurrentWeaponHitBox(true, WeaponHitMatrix);
		//boolCardClickedOn = true;
	}
	public void cardClickedOff(){
		playArea.resetSmallSquares ();
		currentClickedOnCardWeaponMatrix.isCardClickedOn = false;
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


	public void enemyCardDamage(float incomingDamage){
		enemyBehaviour.takeDamage (incomingDamage);
	}

}
public class CurrentWeaponHitBox{
	public bool isCardClickedOn;
	public XMLWeaponHitData weaponHitData;
	public CurrentWeaponHitBox(bool incomingCardClickedData, XMLWeaponHitData incomingWeaponHitData){
		isCardClickedOn = incomingCardClickedData;
		weaponHitData = incomingWeaponHitData;
	}
}
