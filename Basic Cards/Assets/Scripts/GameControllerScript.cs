using System.Collections;
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
	private DeckScript enemyDeckController;
	private DeckScript playerDeckController;
	//public PlayAreaScript playAreaController;
	private EnemyScript enemyController;
	private PlayerScript playerController;

	public CurrentWeaponHitBox currentClickedOnCardWeaponMatrix{ get; set; }
	//private bool boolCardClickedOn;

	void Start () {
		GameObject loaderScriptTemp = GameObject.FindWithTag("MainLoader");				//whole block is for grabbing the Deck object so it can deal a card when clicked
		if(loaderScriptTemp == null){
			SceneManager.LoadScene("XMLLoaderScene"); //Only happens if coroutine is finished
//			StartCoroutine (StartUpLoader());
			return;
		}
		enemyController = gameObject.GetComponentInChildren<EnemyScript>();
		playerController = gameObject.GetComponentInChildren<PlayerScript>();

		enemyDeckController = enemyController.GetComponentInChildren<DeckScript>();
		playerDeckController = playerController.GetComponentInChildren<DeckScript>();
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

//		StartCoroutine (playAreaController.ManualStart ());
//		print ("manual play area done");

		StartCoroutine (enemyController.ManualStart ());
//		print ("manual enemy cont done");
//		playAreaController.populateEnemyPlayAreaSquares ();
//		print ("enemy play area population started");

	}
//	IEnumerator StartUpLoader(){
//		SceneManager.LoadScene("XMLLoaderScene"); //Only happens if coroutine is finished
//		print("its still going");
//		yield return null;
//	}


//	public void makeBody(){
//		enemyController.populateBody ();
//		//enemyController.takeDamage ();
//	}
//	public void makeActiveSquares(){
//		playAreaController.populateEnemyPlayAreaSquares ();
//	}

	public void cardClickedOn(XMLWeaponHitData WeaponHitMatrix, float weaponDamage){		//command sent from the CardBehaviour script with info about the damage its doing
		currentClickedOnCardWeaponMatrix = new CurrentWeaponHitBox(true, WeaponHitMatrix, weaponDamage);
		enemyController.getPlayAreaOfEnemy().hardResetSmallSquares ();

		//boolCardClickedOn = true;
	}
	public void cardClickedOff(){				//sent from the cardbehaviour
		enemyController.getPlayAreaOfEnemy().softResetSmallSquares ();			//resets all the targetting squares if the card is released. If not in place, used cards never 'exit'
		currentClickedOnCardWeaponMatrix.isCardClickedOn = false;
	}

	public void discardDrawThenShuffle(){
		enemyDeckController.discardDrawThenShuffle();		//puts all draw pile cards into the discard and then shuffles discard
	}
	public void shuffleDiscard(){					//only shuffles discard
		enemyDeckController.shuffleDiscard();

	}

	public void discardAllActiveShuffle(){			//discards all active cards and cards in draw pile and then shuffles
		enemyDeckController.discardAllActiveShuffle();
	}


	public void enemyCardDamage(){		//is sent by the deckbahviour script that the active card was just played
//		Debug.Log("target: " +playAreaController.getActiveSquareStateSoftTarget(0,0));
//		Debug.Log("occupied: " +playAreaController.getActiveSquareStateOccupied(0,0));
		//enemyController.takeDamage (currentClickedOnCardWeaponMatrix);
		Vector2 gridDimensions = enemyController.getPlayAreaOfEnemy().getGridDimensions();
		for (int x = 0; x < gridDimensions.x; x++) {
			for (int y = 0; y < gridDimensions.y; y++) {
				if (enemyController.getPlayAreaOfEnemy().getTargetSquareStateSoftTarget(x,y) && enemyController.getPlayAreaOfEnemy().getTargetSquareStateOccupied(x,y)){
					enemyController.getPlayAreaOfEnemy().takeAHit (currentClickedOnCardWeaponMatrix, x, y);
					enemyController.updateHealthDisplay ();
				}
			}
		}
		cardClickedOff ();
	}
	public DeckScript getEnemyDeckController(){
		return enemyDeckController;
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
