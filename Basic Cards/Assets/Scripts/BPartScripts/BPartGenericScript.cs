using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPartGenericScript : MonoBehaviour {

	private BodyPartDataHolder bodyPartData;

	//completely static, non dependent variables
	private string bPartType;				//arm,head,legs,shoulder, or torso
	private string bPartName;
	private BodyPartNode[][] nodesOfBP;		//given an open grid, the list of active hitable points by list of vectors
	private Vector2 anchorPoint;			//the location in which all parts will be located and placed
	private List<ComplexAnchorPoints> complexAnchorPoints;
	private float maxHealth;

	//dependent but static variables
	private Vector2 anchorGlobalPointLocation;	//the anchor point location in the game hit area
	private Vector2 dimensions;		//dependent on the farthest location from the source (0,0) of the list of binaryDimensions
	private bool leftSide;		//default is left side

	//dependent and changable variables
	private float currentHealth;
	private bool active;

//	public void CreateNewPart(BodyPartDataHolder incomingBodyPartData){
//		bPartType = incomingBodyPartData.typeOfpart;				//arm,head,legs,shoulder, or torso
//		bPartName = incomingBodyPartData.name;
//		nodesOfBP = new BodyPartNode[incomingBodyPartData.bodyPartGrid.Length][];
//		for(int i=0; i < incomingBodyPartData.bodyPartGrid.Length; i++){	//transfering the int[][] grid
//			nodesOfBP [i] = new BodyPartNode[incomingBodyPartData.bodyPartGrid[0].Length];
//			for(int j=0; j < incomingBodyPartData.bodyPartGrid[0].Length; j++){
//				BodyPartNode bodyPartNode = new BodyPartNode ();
//				if (incomingBodyPartData.bodyPartGrid [i] [j] == 1) {
//					bodyPartNode.turnOn ();
//				}
//				nodesOfBP [i] [j] = bodyPartNode;
//			}
//		}
//		anchorPoint = incomingBodyPartData.anchor;			//the location in which all parts will be located and placed
//		maxHealth = incomingBodyPartData.maxHealth;
//
//		dimensions = new Vector2(nodesOfBP.Length, nodesOfBP[0].Length);		//dependent on the farthest location from the source (0,0) of the list of binaryDimensions
//				//default is left side
//
//		//dependent and changable variables
//		currentHealth = incomingBodyPartData.maxHealth;
//		resetHealthToFull();
//		active = true;
//	}
	public void CreateNewPart(BodyPartDataHolder incomingBodyPartData, string leftOrRight){
		bPartType = incomingBodyPartData.typeOfpart;				//arm,head,legs,shoulder, or torso
		bPartName = incomingBodyPartData.name;


		maxHealth = incomingBodyPartData.maxHealth;

		nodesOfBP = new BodyPartNode[incomingBodyPartData.bodyPartGrid.Length][];
		if (leftOrRight == "left" || leftOrRight== "none") {
			leftSide = true;		//default is left side
		} else {
			leftSide = false;
		}
		for(int i=0; i < incomingBodyPartData.bodyPartGrid.Length; i++){	//transfering the int[][] grid
			int g = incomingBodyPartData.bodyPartGrid.Length - i-1;
			if (leftSide) {
				nodesOfBP [i] = new BodyPartNode[incomingBodyPartData.bodyPartGrid[0].Length];
			}
			else{									//mirroring the body part for right hand pieces
				nodesOfBP [g] = new BodyPartNode[incomingBodyPartData.bodyPartGrid[0].Length];
			}
			nodesOfBP [i] = new BodyPartNode[incomingBodyPartData.bodyPartGrid[0].Length];
			for(int j=0; j < incomingBodyPartData.bodyPartGrid[0].Length; j++){
				BodyPartNode bodyPartNode = new BodyPartNode ();
				if (incomingBodyPartData.bodyPartGrid [i] [j] == 1) {
					bodyPartNode.turnOn ();
				}
				if (leftSide) {
					nodesOfBP [i] [j] = bodyPartNode;
				}
				else{									//mirroring the body part for right hand pieces
					nodesOfBP [g] [j] = bodyPartNode;
				}
			}
		}
		dimensions = new Vector2(nodesOfBP.Length, nodesOfBP[0].Length);		//dependent on the farthest location from the source (0,0) of the list of binaryDimensions
		//dependent and changable variables
		if (incomingBodyPartData.simpleAnchorPoints) {			//checking to see if there is one anchor point or more
			if (leftSide){										//if left side (default design), then transfer anchor point normally
				anchorPoint = incomingBodyPartData.anchor;			//the location in which all parts will be located and placed
			}
			else if(!leftSide){								//iff right side, mirror the anchor point across the X axis
				anchorPoint = new Vector2 ((dimensions.x - incomingBodyPartData.anchor.x), incomingBodyPartData.anchor.y);
			}
		} else {
			if (leftSide) {										//if left side (default design), then transfer anchor point normally
				complexAnchorPoints = incomingBodyPartData.complexAnchorPoints;			//the location in which all parts will be located and placed
			} else if (!leftSide) {								//iff right side, mirror the anchor point across the X axis


				/////////////run through loop to mirror every set of Vector2 points contained in the list
				//complexAnchorPoints = new Vector2 ((dimensions.x - incomingBodyPartData.anchor.x), incomingBodyPartData.anchor.y);
			}
		}

		currentHealth = incomingBodyPartData.maxHealth;
		resetHealthToFull();
		active = true;
	}
	public void resetHealthToFull(){
		currentHealth = maxHealth;
	}
	public void setGlobalAnchorPoint(Vector2 incomingGlobalAnchorPoint){
		anchorGlobalPointLocation = incomingGlobalAnchorPoint;
	}
	public string getName(){
		return bPartName;
	}
	public string getType(){
		return bPartType;
	}
	public bool getSide(){
		return leftSide;
	}
	public bool getGridPoint(Vector2 incomingPoint){
		return nodesOfBP [(int)incomingPoint.x] [(int)incomingPoint.y].getState ();
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