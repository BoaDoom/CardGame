using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour {
	private BPartMakerScript BpartMaker;

	float healthMax = 200;
	float remainingHealth;
	public Text enemyHealthDisplayNumber;

	public Transform healthBarGraphic;
	private Vector3 healthBarStartingScale;

	private WholeBodyOfParts wholeBodyOfParts;
	CurrentWeaponHitBox incomingWeaponhitBox;

	void Start () {
		BpartMaker = gameObject.GetComponent<BPartMakerScript> ();

		remainingHealth = healthMax;
		healthBarStartingScale = healthBarGraphic.localScale;
		updateHealthDisplay ();

	}

	void Update () {
		
	}
	public void takeDamage(CurrentWeaponHitBox incomingDamage){			//only sent from GameController script
		Vector3 tempHealth = healthBarStartingScale;
		remainingHealth -= incomingDamage.weaponDamage;
		tempHealth.x = healthBarStartingScale.x * (remainingHealth / healthMax);
		healthBarGraphic.localScale = tempHealth;
		if (remainingHealth <= 0) {
			ResetHealthBar ();
		}
		updateHealthDisplay ();
		populateBody ();
	}
	public void ResetHealthBar(){
		healthBarGraphic.localScale = healthBarStartingScale;
		remainingHealth = healthMax;
	}
	private void updateHealthDisplay(){
		enemyHealthDisplayNumber.text = remainingHealth.ToString() + "/" + healthMax.ToString();
	}
	private void populateBody(){
		Debug.Log(BpartMaker.getBodyData ("light arm").name);
		wholeBodyOfParts = new WholeBodyOfParts (BpartMaker.getBodyData("light arm"));
	}
}
public class WholeBodyOfParts{
	BPartGenericScript leftArm;
	BPartGenericScript rightArm;
	BPartGenericScript head;
	BPartGenericScript leftLeg;
	BPartGenericScript rightLeg;
	BPartGenericScript leftShoulder;
	BPartGenericScript rightShoulder;
	BPartGenericScript torso;

	public WholeBodyOfParts(BodyPartDataHolder incomingBodyPartData){
		Debug.Log (incomingBodyPartData.name);
	}
}

