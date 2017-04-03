using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartMakerScript : MonoBehaviour {
	BPartXMLReaderScript bPartXMLReader;
	public BPartGenericScript bodyPartObject;
	BodyPartDataHolder partData;
	public Transform placeHolder;

	void Start(){
		bPartXMLReader = gameObject.GetComponent<BPartXMLReaderScript>();
		//bodyPartObject = gameObject.GetComponent<>();
	}
//	public BPartGenericScript makeBodyPart(string nameOfpart){
//		Debug.Log ("single: " + nameOfpart);
//		partData = bPartXMLReader.getBodyData (nameOfpart);
//		Instantiate (bodyPartObject, Vector3.zero, bodyPartObject.GetComponent<Transform>().rotation);
//		bodyPartObject.CreateNewPart (partData);
//		return bodyPartObject;
//	}
	public BPartGenericScript makeBodyPart(string nameOfpart, string leftOrRight){
		//Debug.Log ("double: " + nameOfpart + " " + leftOrRight);
		partData = bPartXMLReader.getBodyData (nameOfpart);
		BPartGenericScript instaBodypart = Instantiate (bodyPartObject, Vector3.zero, bodyPartObject.GetComponent<Transform>().rotation);
		instaBodypart.CreateNewPart (partData, leftOrRight);
		return instaBodypart;
	}
	public WholeBodyOfParts createWholeBody(WholeBodyOfParts incomingWholeBodyOfParts, Vector2 incomingDimensionsOfPlayArea){
		for (int i=0; i < 5; i++) {
//			Debug.Log (incomingWholeBodyOfParts.leftArm.getName());
//			Debug.Log (incomingWholeBodyOfParts.rightArm.getName());
//			Debug.Log (incomingWholeBodyOfParts.torso.getName());
		}
		if (incomingWholeBodyOfParts.bodyPartCheck ()) {
//			Debug.Log ("dimensions of part"+ incomingWholeBodyOfParts.torso.getDimensionsOfPart());
////			Debug.Log (Mathf.Round(incomingDimensionsOfPlayArea.x/2));
////			Debug.Log (Mathf.Round (incomingWholeBodyOfParts.torso.getDimensionsOfPart ().x / 2));
//			Debug.Log (Mathf.Round(incomingDimensionsOfPlayArea.x/2)-Mathf.Round(incomingWholeBodyOfParts.torso.getDimensionsOfPart().x/2));		//the far left point that the torso needs to be center, it's origin x point
//			Debug.Log (incomingWholeBodyOfParts.torso.getDimensionsOfPart().y);
			Vector2 offSetToCenter = new Vector2
				(Mathf.Round(incomingDimensionsOfPlayArea.x/2)-Mathf.Round(incomingWholeBodyOfParts.torso.getDimensionsOfPart().x/2)+1,		//the far left point that the torso needs to be center, it's origin x point
				incomingWholeBodyOfParts.leftLeg.getDimensionsOfPart().y-1);
			
			incomingWholeBodyOfParts.torso.setTorsoOriginPosition (offSetToCenter);
//			Debug.Log ("offset to center for torso" +offSetToCenter);
//			Debug.Log ("test for left leg point:" +incomingWholeBodyOfParts.torso.getGlobalAnchorPoint("LeftLegPoint"));
			incomingWholeBodyOfParts.leftLeg.setGlobalPosition (incomingWholeBodyOfParts.torso.getGlobalAnchorPoint ("LeftLegPoint"));
//			Debug.Log ("global origin left leg: "+incomingWholeBodyOfParts.leftLeg.getGlobalOriginPoint());
			incomingWholeBodyOfParts.rightLeg.setGlobalPosition (incomingWholeBodyOfParts.torso.getGlobalAnchorPoint ("RightLegPoint"));
//			Debug.Log ("global origin right leg: "+incomingWholeBodyOfParts.rightLeg.getGlobalOriginPoint());
			incomingWholeBodyOfParts.head.setGlobalPosition (incomingWholeBodyOfParts.torso.getGlobalAnchorPoint ("HeadPoint"));
//			Debug.Log ("global origin head: "+incomingWholeBodyOfParts.head.getGlobalOriginPoint());
			incomingWholeBodyOfParts.leftShoulder.setGlobalPositionOffComplexAnchor (incomingWholeBodyOfParts.torso.getGlobalAnchorPoint("LeftShoulderPoint"), "TorsoPoint");
//			Debug.Log ("global origin left shoulder: "+incomingWholeBodyOfParts.leftShoulder.getGlobalOriginPoint());
			incomingWholeBodyOfParts.rightShoulder.setGlobalPositionOffComplexAnchor (incomingWholeBodyOfParts.torso.getGlobalAnchorPoint("RightShoulderPoint"), "TorsoPoint");
//			Debug.Log ("global origin right shoulder: "+incomingWholeBodyOfParts.rightShoulder.getGlobalOriginPoint());

			//Debug.Log ("Arm point "+incomingWholeBodyOfParts.leftShoulder.getGlobalAnchorPoint ("ArmPoint"));
			incomingWholeBodyOfParts.leftArm.setGlobalPosition (incomingWholeBodyOfParts.leftShoulder.getGlobalAnchorPoint ("ArmPoint"));
//			Debug.Log ("global origin left arm: "+incomingWholeBodyOfParts.leftArm.getGlobalOriginPoint());
			incomingWholeBodyOfParts.rightArm.setGlobalPosition (incomingWholeBodyOfParts.rightShoulder.getGlobalAnchorPoint ("ArmPoint"));
//			Debug.Log ("global origin right arm: "+incomingWholeBodyOfParts.rightArm.getGlobalOriginPoint());

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

	public void setBodyPart(BPartGenericScript incomingBodyPart){
		//Debug.Log (incomingBodyPart.getType());
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
	public bool bodyPartCheck(){
		if (leftArm != null && rightArm != null && head != null && leftLeg != null && rightLeg != null && leftShoulder != null && rightShoulder != null && torso != null) {
			return true;
		} else {
			return false;
		}
	}
}

