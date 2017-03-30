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
		Debug.Log ("double: " + nameOfpart + " " + leftOrRight);
		partData = bPartXMLReader.getBodyData (nameOfpart);
		Instantiate (bodyPartObject, Vector3.zero, bodyPartObject.GetComponent<Transform>().rotation);
		bodyPartObject.CreateNewPart (partData, leftOrRight);
		return bodyPartObject;
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

	public void setBodyPart(BPartGenericScript incomingBodyPart){
		//Debug.Log (incomingBodyPart.getName());
		switch (incomingBodyPart.getType()) {
		case ("Arm"):
			if (incomingBodyPart.getSide ()) {
				leftArm = incomingBodyPart;
				break;
			} else {
				rightArm = incomingBodyPart;
				break;
			}
		case ("Head"):
			head = incomingBodyPart;
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
}

