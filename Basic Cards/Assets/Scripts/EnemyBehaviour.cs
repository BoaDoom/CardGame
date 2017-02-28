using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
	public float healthMax = 10;
	public float remainingHealth;

	public Transform healthBarGraphic;
	private Vector3 healthBarStartingScale;

	void Start () {
		remainingHealth = healthMax;
		healthBarStartingScale = healthBarGraphic.localScale;
	}

	void Update () {
		
	}
	public void takeDamage(float incomingDamage){
		Vector3 tempHealth = healthBarStartingScale;
		remainingHealth -= incomingDamage;
		tempHealth.x = healthBarStartingScale.x * (remainingHealth / healthMax);
		healthBarGraphic.localScale = tempHealth;
	}
	public void ResetHealthBar(){
		healthBarGraphic.localScale = healthBarStartingScale;
		remainingHealth = healthMax;
	}
}
