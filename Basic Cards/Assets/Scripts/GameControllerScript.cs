﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour {
	public Button shuffleDiscardsButton;
	public Button MakeSquaresButton;
	public Button discardEverythingButton;
	public Button makeBodyButton;
	//public DeckBehaviour deckBehav;
	public DeckScript deckController;
	public PlayAreaScript playAreaController;
	public EnemyScript enemyController;

	public CurrentWeaponHitBox currentClickedOnCardWeaponMatrix{ get; set; }
	//private bool boolCardClickedOn;

	void Start () {
		GameObject loaderScriptTemp = GameObject.FindWithTag("MainLoader");				//whole block is for grabbing the Deck object so it can deal a card when clicked
		if(loaderScriptTemp == null){
			SceneManager.LoadScene("XMLLoaderScene"); //Only happens if coroutine is finished
//			StartCoroutine (StartUpLoader());
			return;
		}
//		print ("and still");
		//boolCardClickedOn = false;
		currentClickedOnCardWeaponMatrix = new CurrentWeaponHitBox(false, null, 0);
		shuffleDiscardsButton.onClick.AddListener(shuffleDiscard);
//		MakeSquaresButton.onClick.AddListener(makeActiveSquares);
		discardEverythingButton.onClick.AddListener(discardAllActiveShuffle);
//		makeBodyButton.onClick.AddListener(makeBody);

//		GameObject deckControllerObjectTemp = GameObject.FindWithTag("DeckController");				//whole block is for grabbing the Deck object so it can deal a card when clicked
//		if(deckControllerObjectTemp != null){
//			deckController = deckControllerObjectTemp.GetComponent<DeckScript>();
//			}
//		if(deckControllerObjectTemp == null){
//			Debug.Log ("Cannot find 'deckController'object");
//			}
//		GameObject playAreaControllerTemp = GameObject.FindWithTag("PlayAreaController");
//		if(playAreaControllerTemp != null){
//			playAreaController = playAreaControllerTemp.GetComponent<PlayAreaScript>();
//		}
//		if(playAreaControllerTemp == null){
//			Debug.Log ("Cannot find 'DeckBehaviour'object");
//		}

		StartCoroutine (playAreaController.ManualStart ());
//		print ("manual play area done");
		StartCoroutine (enemyController.ManualStart ());
//		print ("manual enemy cont done");
		playAreaController.populateEnemyPlayAreaSquares ();
//		print ("enemy play area population started");
	}
//	IEnumerator StartUpLoader(){
//		SceneManager.LoadScene("XMLLoaderScene"); //Only happens if coroutine is finished
//		print("its still going");
//		yield return null;
//	}

	void Update(){
//		if (enemyController.hasBodyParts() && enemyController.hasAtLeastOneBrokenPart()) {
//			//int i = 0;
//
//			foreach (BPartGenericScript bPart in enemyController.getWholeBodyOfParts().getBrokenParts()){		//for every body part in the list
//				
//				if (!bPart.getActive() && !bPart.getFullyDeactivated ()) {	//if part is not active and not fully deactivated, deactivate it's squares
//					//Debug.Log (enemyController.getWholeBodyOfParts().listOfAllParts.Count);
//					bPart.setFullyDeactivated ();
//					for (int x = 0; x < (bPart.getDimensionsOfPart ().x); x++) {				//get the x dimensions and run through the grid of Y
//						for (int y = 0; y < (bPart.getDimensionsOfPart ().y); y++) {			//get the y dimensions and run through every colloum of parts
//							if (bPart.getGridPoint (new Vector2 (x, y))) {				//gets the body part point and asks the grid of bodypartnodes if they are on or off at the internal dimension of the part
//								int outGoingXCord = ((int)bPart.getGlobalOriginPoint ().x) + x;
//								int outGoingYCord = ((int)bPart.getGlobalOriginPoint ().y) + y;
//								playAreaController.getSmallSquare (outGoingXCord, outGoingYCord).DeactivateSquare ();
//							}
//						}
//					}
//					enemyController.unflagABrokenPart ();
//				}
//			}
//			//UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
//		}
	}
//	public void makeBody(){
//		enemyController.populateBody ();
//		//enemyController.takeDamage ();
//	}
	public void makeActiveSquares(){
		playAreaController.populateEnemyPlayAreaSquares ();
	}

	public void cardClickedOn(XMLWeaponHitData WeaponHitMatrix, float weaponDamage){		//command sent from the CardBehaviour script with info about the damage its doing
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
				if (playAreaController.getTargetSquareStateSoftTarget(x,y) && playAreaController.getTargetSquareStateOccupied(x,y)){
					playAreaController.takeAHit (currentClickedOnCardWeaponMatrix, x, y);
					enemyController.updateHealthDisplay ();
				}
			}
		}
		cardClickedOff ();
	}

}
public class CurrentWeaponHitBox{
	public bool isCardClickedOn{ get; set; }
	public XMLWeaponHitData weaponHitData{ get;  set; }
	public float weaponDamage{ get; private set; }
	public CurrentWeaponHitBox(bool incomingCardClickedData, XMLWeaponHitData incomingWeaponHitData, float weaponDamageT){
		isCardClickedOn = incomingCardClickedData;
		weaponHitData = incomingWeaponHitData;
		weaponDamage = weaponDamageT;
	}
}
