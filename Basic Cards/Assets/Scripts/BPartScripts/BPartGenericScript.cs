using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPartGenericScript : MonoBehaviour {

	//completely static, non dependent variables
	private string bPartType;				//arm,head,legs,shoulder, or torso
	private string bPartName;
	private BodyPartNode[][] nodesOfBP;		//given an open grid, the list of active hitable points by list of vectors
	private Vector2 anchorPoint;			//the location in which all parts will be located and placed
	private float maxHealth;

	//dependent but static variables
	private Vector2 anchorPointLocation;	//the anchor point location in the game hit area
	private Vector2 dimensions;		//dependent on the farthest location from the source (0,0) of the list of binaryDimensions
	private bool leftSide;		//default is left side

	//dependent and changable variables
	private float currentHealth;
	private bool active;
	// Use this for initialization

	public void CreatNewPart(){
		
	}
}

public class BodyPartNode{
	private bool exists;
	public BodyPartNode(){
		exists = false;
	}
	public bool getState(){
		return exists;
	}


	public void turnOn(){
		exists = true;
	}
	public void turnOff(){
		exists = false;
	}
}