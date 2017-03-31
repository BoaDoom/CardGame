using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour {
	private BodyPartMakerScript BpartMaker;

	public BPartGenericScript bodyPartObject;

	float healthMax = 200;
	float remainingHealth;
	public Text enemyHealthDisplayNumber;

	public Transform healthBarGraphic;
	private Vector3 healthBarStartingScale;

	private WholeBodyOfParts wholeBodyOfParts;
	CurrentWeaponHitBox incomingWeaponhitBox;

	private Vector2 playAreaDimensions;

	void Start () {
		BpartMaker = gameObject.GetComponent<BodyPartMakerScript> ();

		remainingHealth = healthMax;
		healthBarStartingScale = healthBarGraphic.localScale;
		updateHealthDisplay ();
		wholeBodyOfParts = new WholeBodyOfParts();
	}
	public void setPlayAreaDimensions(Vector2 incomingDimensions){
		playAreaDimensions = incomingDimensions;
	}

	public void takeDamage(CurrentWeaponHitBox incomingDamage, int xCordOfHit, int yCordOfHit){			//only sent from GameController script
		Vector3 tempHealth = healthBarStartingScale;

		remainingHealth -= incomingDamage.weaponDamage;
		tempHealth.x = healthBarStartingScale.x * (remainingHealth / healthMax);
		healthBarGraphic.localScale = tempHealth;
		if (remainingHealth <= 0) {
			ResetHealthBar ();
		}
		updateHealthDisplay ();
	}
	public void ResetHealthBar(){
		healthBarGraphic.localScale = healthBarStartingScale;
		remainingHealth = healthMax;
	}
	private void updateHealthDisplay(){
		enemyHealthDisplayNumber.text = remainingHealth.ToString() + "/" + healthMax.ToString();
	}
	public void populateBody(){				//currently invoked by game controller script on button press
		//Debug.Log(BpartMaker.getBodyData ("light Arm").name);
		wholeBodyOfParts.setBodyPart( BpartMaker.makeBodyPart ("heavy arm", "left"));
		//Debug.Log ("made heavy arm");
		wholeBodyOfParts.setBodyPart( BpartMaker.makeBodyPart ("heavy arm", "right"));
		wholeBodyOfParts.setBodyPart( BpartMaker.makeBodyPart ("light head", "none"));
		wholeBodyOfParts.setBodyPart( BpartMaker.makeBodyPart ("heavy leg", "left"));
		wholeBodyOfParts.setBodyPart( BpartMaker.makeBodyPart ("heavy leg", "right"));
		wholeBodyOfParts.setBodyPart( BpartMaker.makeBodyPart ("light shoulder", "left"));
		wholeBodyOfParts.setBodyPart( BpartMaker.makeBodyPart ("light shoulder", "right"));
		wholeBodyOfParts.setBodyPart( BpartMaker.makeBodyPart ("light torso", "none"));
		wholeBodyOfParts = BpartMaker.createWholeBody (wholeBodyOfParts, playAreaDimensions);
	}
	public TargetSquareScript[][] populateCorrectPlayAreaSquares(TargetSquareScript[][] incomingSquareGrid){
	//Debug.Log (wholeBodyOfParts.listOfAllParts.Count);
		for (int i=0; i<wholeBodyOfParts.listOfAllParts.Count; i++){
			for (int x=0; x<wholeBodyOfParts.listOfAllParts [i].getDimensionsOfPart ().x; x++){
				for (int y=0; y<wholeBodyOfParts.listOfAllParts [i].getDimensionsOfPart ().y; y++){
					if (wholeBodyOfParts.listOfAllParts [i].getGridPoint(new Vector2(x, y))){
						incomingSquareGrid[(((int)wholeBodyOfParts.listOfAllParts [i].getGlobalOriginPoint().x)+x)][(((int)wholeBodyOfParts.listOfAllParts [i].getGlobalOriginPoint().y)+y)].OccupiedSquare();
						Debug.Log (x + " " + y);
					}
				}
			}
		}
		return incomingSquareGrid;
	}
}

