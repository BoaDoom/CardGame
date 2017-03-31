﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSquareScript : MonoBehaviour {

//	[SerializeField]
	int gridCordX;
//	[SerializeField]
	int gridCordY;

	public Sprite occupiedUntargetedSprite;
	public Sprite occupiedTargetedSprite;
	public Sprite targetMissedSprite;
	Sprite defaultSprite;

	Sprite trueUntarget;
	Sprite trueTarget;
//	private int startingIntValue;

	public TargetSquareState activeSquareState;

	SpriteRenderer spriteRenderer;
	PlayAreaScript playArea;
//	ActiveSquareBehaviour(int startingIntValueImport){
//		startingIntValue = startingIntValueImport;
//	}

	void Awake(){
		activeSquareState = new TargetSquareState();
		SpriteRenderer spriteRendererTemp = gameObject.GetComponent<SpriteRenderer>();
		if(spriteRendererTemp != null){
			spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
			//occupiedSprite =
			defaultSprite = spriteRenderer.sprite;
//			trueUntarget = spriteRenderer.sprite;
		}
		trueUntarget = defaultSprite;
		trueTarget = targetMissedSprite;
		if(spriteRendererTemp == null){
			Debug.Log ("Cannot find 'spriteRendererTemp'object");
		}

		GameObject playAreaTemp = GameObject.FindWithTag ("PlayAreaController");
		if(playAreaTemp != null){
			playArea = playAreaTemp.GetComponent<PlayAreaScript>();
		}
		if(playAreaTemp == null){
			Debug.Log ("Cannot find 'playAreaImport'object");
		}
		hardUntargetSquare ();
	}
	public void SetGridCordX(int cordx){
		gridCordX = cordx;
	}
	public void SetGridCordY(int cordy){
		gridCordY = cordy;
	}
	public int GetGridCordX(){
		return gridCordX;
	}
	public int GetGridCordY(){
		return gridCordY;
	}
//	void OnTriggerStay2D(Collider2D other){
//		if (other.CompareTag("weaponHitBox")){
//			spriteRenderer.sprite = occupiedSprite;
//		}
//	}
//	void OnTriggerExit2D(Collider2D other){
//		if (other.CompareTag("weaponHitBox")){
//			spriteRenderer.sprite = defaultSprite;
//		}
//	}
		
	void OnMouseEnter(){
		playArea.squareHoveredOver (gridCordX, gridCordY);

	}
	void OnMouseExit(){
		playArea.squareHoveredOff ();
	}
		
	public void TargetSquare(){		//used by playarea to turn on and off targetting
		spriteRenderer.sprite = trueTarget;
		activeSquareState.setHardTargetedState(true);
		activeSquareState.setSoftTargetedState(true);
//		Debug.Log ("target triggered");

	}
	public void softUntargetSquare(){	//used by playarea to turn on and off targetting. The point of this is to turn off squares that arn't hovered over anymore, but to keep track of
										//where the last place the weapon shape was hovered over in case the user releases and 'fires' the weapon within the bounderies, the correct portions are hit
		spriteRenderer.sprite = trueUntarget;
		activeSquareState.setHardTargetedState(false);
//		Debug.Log ("soft untarget triggered");
	}
	public void hardUntargetSquare(){	//used by playarea to turn on and off targetting. Hard reset happens when Another
		spriteRenderer.sprite = trueUntarget;
		activeSquareState.setSoftTargetedState(false);
		activeSquareState.setHardTargetedState(false);		//redundent but needed
//		Debug.Log ("hard untarget triggered");
	}





	public void OccupiedSquare(){	//used by playarea to turn on and off if the enemy occupies the space
		trueTarget = occupiedTargetedSprite;
		trueUntarget = occupiedUntargetedSprite;
		activeSquareState.setOccupiedState(true);
	}
	public void DeactivateSquare(){		//used by playarea to turn on and off if the enemy occupies the space
		trueUntarget = defaultSprite;
		trueTarget = targetMissedSprite;
		activeSquareState.setOccupiedState(false);
	}

//	public TargetSquareState getStateOfSquare(){
//		return activeSquareState;
//	}
//	void OnMouseExit(){
//		spriteRenderer.sprite = defaultSprite;
//	}
}

