using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartMakerScript : MonoBehaviour {
	BPartXMLReaderScript bPartXMLReader;
	public BPartGenericScript bodyPartObject;
	BodyPartDataHolder partData;
	public Transform placeHolder;
	public bool startupDone = false;

	void Start(){
		GameObject loaderScriptTemp = GameObject.FindWithTag("MainLoader");	
		GameObject LoaderMainTemp = GameObject.FindWithTag ("MainLoader");
		if (LoaderMainTemp != null) {
			bPartXMLReader = LoaderMainTemp.GetComponent<BPartXMLReaderScript> ();
			//Debug.Log ("GOT Bpart XML");
			//startupDone = true;
		}
		if (LoaderMainTemp == null && loaderScriptTemp != null) {
			Debug.Log ("Cannot find 'MainLoader'object");
		}
		//bPartXMLReader = gameObject.GetComponent<BPartXMLReaderScript>();
		//bodyPartObject = gameObject.GetComponent<>();
		GameObject EnemyScriptTemp = GameObject.FindWithTag ("EnemyController");
		if (LoaderMainTemp != null) {
			EnemyScriptTemp.GetComponent<EnemyScript> ().signalThatStartupIsDone();
		} else if (LoaderMainTemp != null) {
			Debug.Log ("could not find enemy script");
		}
	}
//	public BPartGenericScript makeBodyPart(string nameOfpart){
//		Debug.Log ("single: " + nameOfpart);
//		partData = bPartXMLReader.getBodyData (nameOfpart);
//		Instantiate (bodyPartObject, Vector3.zero, bodyPartObject.GetComponent<Transform>().rotation);
//		bodyPartObject.CreateNewPart (partData);
//		return bodyPartObject;
//	}
	public BPartGenericScript makeBodyPart(string nameOfpart, string leftOrRight){
//		Debug.Log ("check: " + nameOfpart + " " + leftOrRight);
		Debug.Log("name: "+ nameOfpart); 
		//Debug.Log("leftor right: "+ leftOrRight);
		partData = bPartXMLReader.getBodyData (nameOfpart);
		BPartGenericScript instaBodypart = Instantiate (bodyPartObject, Vector3.zero, bodyPartObject.GetComponent<Transform>().rotation);
		Debug.Log ("body data check: "+bPartXMLReader.getBodyData (nameOfpart).name);
		instaBodypart.CreateNewPart (partData, leftOrRight);
		//Debug.Log ("instantiated after: "+instaBodypart.getName());
		return instaBodypart;
	}
	public WholeBodyOfParts createWholeBody(WholeBodyOfParts incomingWholeBodyOfParts, Vector2 incomingDimensionsOfPlayArea){
		for (int i=0; i < 5; i++) {
//			Debug.Log (incomingWholeBodyOfParts.leftArm.getName());
//			Debug.Log (incomingWholeBodyOfParts.rightArm.getName());
//			Debug.Log (incomingWholeBodyOfParts.torso.getName());
		}
		if (incomingWholeBodyOfParts.bodyPartCheck ()) {

			Vector2 offSetToCenter = new Vector2
				(Mathf.Round(incomingDimensionsOfPlayArea.x/2)-Mathf.Round(incomingWholeBodyOfParts.torso.getDimensionsOfPart().x/2)+1,		//the far left point that the torso needs to be center, it's origin x point
					incomingWholeBodyOfParts.leftLeg.getAnchorPoint().y-1);
			
			incomingWholeBodyOfParts.torso.setTorsoOriginPosition (offSetToCenter);

			incomingWholeBodyOfParts.leftLeg.setGlobalPosition (incomingWholeBodyOfParts.torso.getGlobalAnchorPoint ("LeftLegPoint"));

			incomingWholeBodyOfParts.rightLeg.setGlobalPosition (incomingWholeBodyOfParts.torso.getGlobalAnchorPoint ("RightLegPoint"));

			incomingWholeBodyOfParts.head.setGlobalPosition (incomingWholeBodyOfParts.torso.getGlobalAnchorPoint ("HeadPoint"));

			incomingWholeBodyOfParts.leftShoulder.setGlobalPositionOffComplexAnchor (incomingWholeBodyOfParts.torso.getGlobalAnchorPoint("LeftShoulderPoint"), "TorsoPoint");

			incomingWholeBodyOfParts.rightShoulder.setGlobalPositionOffComplexAnchor (incomingWholeBodyOfParts.torso.getGlobalAnchorPoint("RightShoulderPoint"), "TorsoPoint");

			incomingWholeBodyOfParts.leftArm.setGlobalPosition (incomingWholeBodyOfParts.leftShoulder.getGlobalAnchorPoint ("ArmPoint"));

			incomingWholeBodyOfParts.rightArm.setGlobalPosition (incomingWholeBodyOfParts.rightShoulder.getGlobalAnchorPoint ("ArmPoint"));


		} else {
			Debug.Log ("There are body parts missing");
		}
		return incomingWholeBodyOfParts;
	}
}
public class WholeBodyOfParts{
	public BPartGenericScript leftArm;
	public BPartGenericScript rightArm;
	public BPartGenericScript head;
	public BPartGenericScript leftLeg;
	public BPartGenericScript rightLeg;
	public BPartGenericScript leftShoulder;
	public BPartGenericScript rightShoulder;
	public BPartGenericScript torso;
	public List <BPartGenericScript> listOfAllParts = new List<BPartGenericScript> ();
	public bool hasAnyBodyParts;

	public void setBodyPart(BPartGenericScript incomingBodyPart){
		//Debug.Log (incomingBodyPart.getType());
		hasAnyBodyParts = true;
		listOfAllParts.Add(incomingBodyPart);
		switch (incomingBodyPart.getType()) {
		case ("Arm"):
			if (incomingBodyPart.getSide ()) {
				leftArm = incomingBodyPart;
				//Debug.Log("left arm import check: " +leftArm.getName ());
				break;
			} else {
				rightArm = incomingBodyPart;
				//Debug.Log("left arm import check: " +leftArm.getName ());
				break;
			}
		case ("Head"):
			head = incomingBodyPart;
			//Debug.Log("left arm import check: " +leftArm.getName ());
			break;
		case ("Leg"):
			if (incomingBodyPart.getSide ()) {
				leftLeg = incomingBodyPart;
				break;
			} else {
				rightLeg = incomingBodyPart;
				break;
			}
		case ("Shoulder"):
			if (incomingBodyPart.getSide ()) {
				leftShoulder = incomingBodyPart;
				break;
			} else {
				rightShoulder = incomingBodyPart;
				break;
			}
		case ("Torso"):
			torso = incomingBodyPart;
			break;
		}
	}
	public BPartGenericScript getBodyPart(string nameOfPart){
		switch (nameOfPart) {
		case "leftArm":
			return leftArm;
		case "rightArm":
			return rightArm;
		case "head":
			return head;
		case "leftLeg":
			return leftLeg;
		case "rightLeg":
			return rightLeg;
		case "leftShoulder":
			return leftShoulder;
		case "rightShoulder":
			return rightShoulder;
		case "torso":
			return torso;
		}
		return null;
	}
	public int bodyPartCount(){
		int count = 0;
		foreach(BPartGenericScript part in listOfAllParts){
			count++;
		}
		return count;
	}
	public List<BPartGenericScript> getBrokenParts(){
		List<BPartGenericScript> listOfBrokenParts = new List<BPartGenericScript> ();
		foreach (BPartGenericScript bPart in listOfAllParts) {
			if (bPart.getCurrentHealth () <= 0) {
				listOfBrokenParts.Add (bPart);
			}
		}
		return listOfBrokenParts;
	}
	public bool bodyPartCheck(){
		if (leftArm != null && rightArm != null && head != null && leftLeg != null && rightLeg != null && leftShoulder != null && rightShoulder != null && torso != null) {
			return true;
		} else {
			return false;
		}
	}
	public bool hasBodyPart(){
		return hasAnyBodyParts;
	}
	public void resetBodyToZero(){
		 leftArm = null;
		rightArm = null;
		head = null;
		leftLeg = null;
		rightLeg = null;
		leftShoulder = null;
		rightShoulder = null;
		torso = null;
		listOfAllParts.Clear();
	}
}

