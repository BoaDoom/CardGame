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
	private List<ComplexAnchorPoints> listOfComplexAnchorPoints = new List<ComplexAnchorPoints> ();
	private float maxHealth;

	//dependent but static variables
	private Vector2 globalOriginPoint;	//the anchor point location in the game hit area
	private Vector2 dimensions;		//dependent on the farthest location from the source (0,0) of the list of binaryDimensions
	private bool leftSide;		//default is left side

	//dependent and changable variables
	private float currentHealth;
	private bool active;
	private bool fullyDeactivated;

	public void takeDamage(float incomingDamage){
		currentHealth -= incomingDamage;
	}



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
			//nodesOfBP [i] = new BodyPartNode[incomingBodyPartData.bodyPartGrid[0].Length];
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
		//Debug.Log ("Dimensions of "+ bPartName +": " + dimensions);
		//dependent and changable variables
		if (incomingBodyPartData.simpleAnchorPoints) {			//checking to see if there is one anchor point or more
			if (leftSide){										//if left side (default design), then transfer anchor point normally
				anchorPoint = incomingBodyPartData.anchor;			//the location in which all parts will be located and placed
//				Debug.Log("Left side, anchorpoint: "+anchorPoint+" "+bPartName);
			}
			else if(!leftSide){								//if right side, mirror the anchor point across the X axis
				anchorPoint = new Vector2 (((dimensions.x) - (incomingBodyPartData.anchor.x+1)), incomingBodyPartData.anchor.y);
//				Debug.Log("Right side, anchorpoint: "+anchorPoint+" "+bPartName);

			}
		} else {
			//listOfComplexAnchorPoints = incomingBodyPartData.listOfComplexAnchorPoints;	
			if (leftSide) {										//if left side (default design), then transfer anchor point normally
				listOfComplexAnchorPoints = incomingBodyPartData.listOfComplexAnchorPoints;			//the location in which all parts will be located and placed
				//Debug.Log("complex list: "+ listOfComplexAnchorPoints.Count);
			} else 
			if (!leftSide) {								//if right side, mirror the anchor point across the X axis
				for (int i = 0; i < incomingBodyPartData.listOfComplexAnchorPoints.Count; i++) {
						listOfComplexAnchorPoints.Add (new ComplexAnchorPoints(
							incomingBodyPartData.listOfComplexAnchorPoints[i].nameOfPoint,
							new Vector2((dimensions.x-1) - (incomingBodyPartData.listOfComplexAnchorPoints[i].anchorPoint.x), incomingBodyPartData.listOfComplexAnchorPoints[i].anchorPoint.y),
							incomingBodyPartData.listOfComplexAnchorPoints[i].male));
					//the dimension.x+1 is to account for the origin of the points being at 1,1 rather than 0,0.
				}
				//listOfComplexAnchorPoints = 
			}
//			Debug.Log ("check anchor points: " + listOfComplexAnchorPoints[0].nameOfPoint + " " + listOfComplexAnchorPoints[0].anchorPoint);
//			Debug.Log ("check anchor points: " + listOfComplexAnchorPoints[1].nameOfPoint + " " + listOfComplexAnchorPoints[1].anchorPoint);
		}
		//Debug.Log (bPartName + " "+ leftSide);

		currentHealth = incomingBodyPartData.maxHealth;
		resetHealthToFull();
		active = true;
		fullyDeactivated = false;
		//Debug.Log("complex list for "+bPartName+ " : "+ listOfComplexAnchorPoints.Count);
	}
	public void resetHealthToFull(){
		currentHealth = maxHealth;
	}




	public void setTorsoOriginPosition(Vector2 incomingTorsoOriginPoint){
		//Debug.Log ("setting custom torso origin");
		globalOriginPoint = incomingTorsoOriginPoint;
	}

	public void setGlobalPosition(Vector2 incomingGlobalAnchorPoint){
		globalOriginPoint = incomingGlobalAnchorPoint - anchorPoint;
	}
	public Vector2 getGlobalOriginPoint(){
		return globalOriginPoint;
	}

	public void setGlobalPositionOffComplexAnchor(Vector2 incomingGlobalAnchorPoint, string pointOfConnection){
		globalOriginPoint = incomingGlobalAnchorPoint - (listOfComplexAnchorPoints.Find (ComplexAnchorPoints => ComplexAnchorPoints.nameOfPoint == pointOfConnection).anchorPoint);
//		Debug.Log ("ouput point: " +globalOriginPoint);
	}
	public Vector2 getGlobalAnchorPoint(string requestedAnchorPointName){
//		Debug.Log ("globalOriginpoint: "+globalOriginPoint+" requested anchor point:"+ getComplexAnchorPoint(requestedAnchorPointName).anchorPoint);
		return (globalOriginPoint + getComplexAnchorPoint(requestedAnchorPointName).anchorPoint);
	}


	public string getName(){
		return bPartName;
	}
	public string getType(){
		return bPartType;
	}
	public float getCurrentHealth(){
		return currentHealth;
	}

	public Vector2 getDimensionsOfPart(){
		return dimensions;
	}
	public Vector2 getAnchorPoint(){
		return anchorPoint;
	}
	public ComplexAnchorPoints getComplexAnchorPoint(string incomingRequest){
		return listOfComplexAnchorPoints.Find (ComplexAnchorPoints => ComplexAnchorPoints.nameOfPoint == incomingRequest);
	}
	public List<ComplexAnchorPoints> getComplexAllAnchorPoints(){
		return listOfComplexAnchorPoints;
	}
		
	public bool getSide(){
		return leftSide;
	}
	public bool getActive(){
		return active;
	}
	public void setAsActive(){
		active = true;
	}
	public void setAsInactive(){
		active = false;
	}

	public bool getFullyDeactivated(){
		return fullyDeactivated;
	}
	public void setFullyDeactivated(){
		fullyDeactivated  = true;
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