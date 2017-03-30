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

	void Start () {
		BpartMaker = gameObject.GetComponent<BodyPartMakerScript> ();

		remainingHealth = healthMax;
		healthBarStartingScale = healthBarGraphic.localScale;
		updateHealthDisplay ();
		wholeBodyOfParts = new WholeBodyOfParts();

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

	}
	public void ResetHealthBar(){
		healthBarGraphic.localScale = healthBarStartingScale;
		remainingHealth = healthMax;
	}
	private void updateHealthDisplay(){
		enemyHealthDisplayNumber.text = remainingHealth.ToString() + "/" + healthMax.ToString();
	}
	public void populateBody(){				//currently invoked by game controller script on mouse down
		//Debug.Log(BpartMaker.getBodyData ("light Arm").name);
		wholeBodyOfParts.setBodyPart( BpartMaker.makeBodyPart ("light arm", "left"));
		wholeBodyOfParts.setBodyPart( BpartMaker.makeBodyPart ("light arm", "right"));
		wholeBodyOfParts.setBodyPart( BpartMaker.makeBodyPart ("light head", "none"));
		wholeBodyOfParts.setBodyPart( BpartMaker.makeBodyPart ("light leg", "left"));
		wholeBodyOfParts.setBodyPart( BpartMaker.makeBodyPart ("light leg", "right"));
		wholeBodyOfParts.setBodyPart( BpartMaker.makeBodyPart ("light shoulder", "left"));
		wholeBodyOfParts.setBodyPart( BpartMaker.makeBodyPart ("light shoulder", "right"));
		wholeBodyOfParts.setBodyPart( BpartMaker.makeBodyPart ("light torso", "none"));
	}
}

