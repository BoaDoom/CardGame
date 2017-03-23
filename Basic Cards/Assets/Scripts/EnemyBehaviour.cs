using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour {
	float healthMax = 200;
	float remainingHealth;
	public Text enemyHealthDisplayNumber;

	public Transform healthBarGraphic;
	private Vector3 healthBarStartingScale;

	void Start () {
		remainingHealth = healthMax;
		healthBarStartingScale = healthBarGraphic.localScale;
		updateHealthDisplay ();
	}

	void Update () {
		
	}
	public void takeDamage(float incomingDamage){			//only sent from GameController script
		Vector3 tempHealth = healthBarStartingScale;
		remainingHealth -= incomingDamage;
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

}
