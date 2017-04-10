using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour {
	private BodyPartMakerScript BpartMaker;

	public BPartGenericScript bodyPartObject;
	public PlayAreaScript playAreaScript;

	float healthMax = 0;
	float remainingHealth;
	public Text enemyHealthDisplayNumber;

	public Transform healthBarGraphic;
	private Vector3 healthBarStartingScale;

	private WholeBodyOfParts wholeBodyOfParts = new WholeBodyOfParts();
	CurrentWeaponHitBox incomingWeaponhitBox;

	private Vector2 playAreaDimensions;
	private int flagForBrokenParts;

	bool bodypartIsDone = false;
	bool playAreaIsDone = false;

	void Start () {
		GameObject playAreaScriptTemp = GameObject.FindWithTag("PlayAreaController");
		if(playAreaScriptTemp != null){
			playAreaScript = playAreaScriptTemp.GetComponent<PlayAreaScript>();
		}
		if(playAreaScriptTemp == null){
			Debug.Log ("Cannot find 'playAreaScriptTemp'object");}
		
		BpartMaker = gameObject.GetComponent<BodyPartMakerScript> ();

//		remainingHealth = healthMax;
		healthBarStartingScale = healthBarGraphic.localScale;
//		updateHealthDisplay ();

//		while (!BpartMaker.checkIfStartupIsDone()) {
//			Debug.Log ("hope this repeats");
//		}
		//populateBody ();
	}
	public void setPlayAreaDimensions(Vector2 incomingDimensions){
		//print ("inc dim "+incomingDimensions);
		playAreaDimensions = incomingDimensions;
		remainingHealth = healthMax;
	}

//	public void takeDamage(CurrentWeaponHitBox incomingDamage, int xCordOfHit, int yCordOfHit){			//only sent from GameController script
//		Vector3 tempHealth = healthBarStartingScale;
//
//		remainingHealth -= incomingDamage.weaponDamage;
//		tempHealth.x = healthBarStartingScale.x * (remainingHealth / healthMax);
//		healthBarGraphic.localScale = tempHealth;
//		if (remainingHealth <= 0) {
//			ResetHealthBar ();
//		}
//		updateHealthDisplay ();
//	}
//	public void takeDamage(){
//		updateHealthDisplay ();
//	}
	public void signalThatBodyPartIsDone(){		//comes from BodyPartMakerScript
//		print("body done");
		bodypartIsDone = true;
		if (playAreaIsDone) {
			populateBody ();
			playAreaScript.populateEnemyPlayAreaSquares ();
		}
	}
	public void signalThatPlayAreaIsDone(){		//comes from Playareascript
//		print("playarea is done");
		playAreaIsDone = true;
		if (bodypartIsDone) {
			populateBody ();
			playAreaScript.populateEnemyPlayAreaSquares ();
		}
	}
	public void ResetHealthBar(){
		healthBarGraphic.localScale = healthBarStartingScale;

	}
	public void updateHealthDisplay(){
		Vector3 tempHealth = healthBarStartingScale;
		float newHealth = 0;
		//Debug.Log ("Number of body parts: " + wholeBodyOfParts.listOfAllParts.Count);
		for (int i=0; i<wholeBodyOfParts.listOfAllParts.Count; i++){
			float currentHealth = wholeBodyOfParts.listOfAllParts [i].getCurrentHealth ();
			newHealth += wholeBodyOfParts.listOfAllParts [i].getCurrentHealth ();
			//Debug.Log (i+ " health: "+wholeBodyOfParts.listOfAllParts [i].getCurrentHealth ());
			if (currentHealth <= 0 && wholeBodyOfParts.listOfAllParts [i].getActive()) {
				wholeBodyOfParts.listOfAllParts [i].setAsInactive ();
				Debug.Log ("deactivated triggered");
			}
		}
		remainingHealth = newHealth;
		tempHealth.x = healthBarStartingScale.x * (remainingHealth / healthMax);
		healthBarGraphic.localScale = tempHealth;
		enemyHealthDisplayNumber.text = remainingHealth.ToString() + "/" + healthMax.ToString();
	}
	public void populateBody(){				//currently invoked by game controller script on button press
		healthMax = 0;
		//StartCoroutine (waitForBpartMakerScript ());
		wholeBodyOfParts.resetBodyToZero ();
		//Debug.Log(BpartMaker.getBodyData ("light Arm").name);
//		wholeBodyOfParts.setBodyPart( BpartMaker.makeBodyPart ("light arm", "left"));
//		//Debug.Log ("made heavy arm");
//		wholeBodyOfParts.setBodyPart( BpartMaker.makeBodyPart ("light arm", "right"));
//		wholeBodyOfParts.setBodyPart( BpartMaker.makeBodyPart ("light head", "none"));
//		wholeBodyOfParts.setBodyPart( BpartMaker.makeBodyPart ("light leg", "left"));
//		wholeBodyOfParts.setBodyPart( BpartMaker.makeBodyPart ("light leg", "right"));
//		wholeBodyOfParts.setBodyPart( BpartMaker.makeBodyPart ("light shoulder", "left"));
//		wholeBodyOfParts.setBodyPart( BpartMaker.makeBodyPart ("light shoulder", "right"));
//		wholeBodyOfParts.setBodyPart( BpartMaker.makeBodyPart ("light torso", "none"));
//
		wholeBodyOfParts.setBodyPart( BpartMaker.makeBodyPart ("large biped arm", "left"));
		//Debug.Log ("large biped arm, left");
		wholeBodyOfParts.setBodyPart( BpartMaker.makeBodyPart ("large biped arm", "right"));
		//Debug.Log ("large biped arm, "+ "right");
		wholeBodyOfParts.setBodyPart( BpartMaker.makeBodyPart ("large biped head", "left"));
		//Debug.Log ("soforth");
		wholeBodyOfParts.setBodyPart( BpartMaker.makeBodyPart ("large biped leg", "left"));
		//Debug.Log ("soforth");
		wholeBodyOfParts.setBodyPart( BpartMaker.makeBodyPart ("large biped leg", "right"));
		//Debug.Log ("soforth");
		wholeBodyOfParts.setBodyPart( BpartMaker.makeBodyPart ("large biped shoulder", "left"));
		//Debug.Log ("soforth");
		wholeBodyOfParts.setBodyPart( BpartMaker.makeBodyPart ("large biped shoulder", "right"));
		//Debug.Log ("soforth");
		wholeBodyOfParts.setBodyPart( BpartMaker.makeBodyPart ("large biped torso", "none"));
//		print ("play area dim " + playAreaDimensions);
		wholeBodyOfParts = BpartMaker.createWholeBody (wholeBodyOfParts, playAreaDimensions);		//setting internal location positions of each of the body parts in relation to eachother
		for (int i=0; i<wholeBodyOfParts.listOfAllParts.Count; i++){
			healthMax += wholeBodyOfParts.listOfAllParts [i].getCurrentHealth ();		//makes health pool
		}
		foreach (BPartGenericScript bPart in wholeBodyOfParts.listOfAllParts) {
			bPart.GetComponent<Transform>().SetParent(gameObject.GetComponent<Transform>());
		}
		remainingHealth = healthMax;
		updateHealthDisplay ();
	}
//	IEnumerator waitForBpartMakerScript(){
//		if (!BpartMaker.checkIfStartupIsDone ()) {
//			Debug.Log ("Checking...");
//			yield return waitForBpartMakerScript();
//		}
//	}

	public WholeBodyOfParts getWholeBodyOfParts(){
		return wholeBodyOfParts;
	}
	public bool hasBodyParts(){
		return wholeBodyOfParts.hasBodyPart();
	}
	public bool hasAtLeastOneBrokenPart(){
		if (flagForBrokenParts > 0) {
			return true;
		} else {
			return false;
		}
	}
	public void flagABrokenPart(){
		flagForBrokenParts++;
	}
	public void unflagABrokenPart(){
		flagForBrokenParts--;
	}
	public TargetSquareScript[][] populateCorrectPlayAreaSquares(TargetSquareScript[][] incomingSquareGrid){
	//Debug.Log (wholeBodyOfParts.listOfAllParts.Count);
		//print("grid x length: " +incomingSquareGrid[0].Length + " grid y length: "+incomingSquareGrid.Length);
		for (int i=0; i<wholeBodyOfParts.listOfAllParts.Count; i++){		//for every body part in the list
			for (int x=0; x<wholeBodyOfParts.listOfAllParts [i].getDimensionsOfPart ().x; x++){				//get the x dimensions and run through the grid of Y
				for (int y=0; y<wholeBodyOfParts.listOfAllParts [i].getDimensionsOfPart ().y; y++){			//get the y dimensions and run through every colloum of parts
					if (wholeBodyOfParts.listOfAllParts [i].getGridPoint(new Vector2(x, y))&& wholeBodyOfParts.listOfAllParts [i].getActive()){				//gets the body part point and asks the grid of bodypartnodes if they are on or off at the internal dimension of the part

						int outGoingXCord = ((int)wholeBodyOfParts.listOfAllParts [i].getGlobalOriginPoint().x)+x;
						int outGoingYCord = ((int)wholeBodyOfParts.listOfAllParts [i].getGlobalOriginPoint ().y) + y;
						//print ("incomingSquareGridX: "+ incomingSquareGrid[0].Length + " incomingSquareGridY: "+ incomingSquareGrid.Length);

						//print (incomingSquareGrid[7][7]);
//						print (outGoingXCord +" "+ outGoingYCord);
//						print (x +" "+ y);

//						print ("wholeBodyOfParts name: "+wholeBodyOfParts.listOfAllParts [i].getName());
//						print ("i: " +i+ " outGoingXCord: " +outGoingYCord+ " y: " + outGoingYCord);
//						print("whole body part list count: "+wholeBodyOfParts.listOfAllParts.Count);
						incomingSquareGrid[outGoingXCord][outGoingYCord].OccupiedSquare(wholeBodyOfParts.listOfAllParts [i]);

						//if grid point is on, it finds the relative relation of the body part node and turns it on as an Occupiedsquare in the play area. it finds the relative location on the grid because each
						//body part knows its own global origin point, the 0,0 location is the lower left field off the square of the body part. No redundency yet for overlapping body parts.
					}
				}
			}
		}
		return incomingSquareGrid;
	}
}

