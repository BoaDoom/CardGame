﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic; //Needed for Lists
using System.Xml; //Needed for XML functionality
using System.Xml.Serialization; //Needed for XML Functionality
using System.IO;
using System.Xml.Linq; //Needed for XDocument
using System.Linq;

public class BPartMakerScript : MonoBehaviour {

	public BPartGenericScript bPartGenericPrefab;


	XDocument xmlDoc; //create Xdocument. Will be used later to read XML file 
	IEnumerable<XElement> typeOfParts; //Create an Ienumerable list. Will be used to store XML Items. 
	IEnumerable<XElement> listOfParts;
	IEnumerable<XElement> storedAnchorPoints;
	public List <BodyPartDataHolder> BPartData = new List <BodyPartDataHolder>(); //Initialize List of XMLWeaponData objects.
//	private BPartGenericScript LeftViewerArm;
//	private BPartGenericScript RightViewerArm;
//	private BPartGenericScript Arms;
//	private BPartGenericScript Arms;
//	private BPartGenericScript Arms;

	private string BpartName = "none";
	private string BpartType = "none";
	IEnumerable<XElement> gridHitBox;
	IEnumerable<XElement> AnchorPoints;
	private int MaxHealth = 0;
	private int[][] gridOfBodyPart;
	private Vector2 anchorVector2;

	void Start ()
	{
		//DontDestroyOnLoad (gameObject); //Allows Loader to carry over into new scene 
		LoadXML (); //Loads XML File. Code below. 
		StartCoroutine(AssignData()); //Starts assigning XML data to data List. Code below
		//		Debug.Log("inside bodydata count "+BPartData.Count);
	}

	void LoadXML()

	{
		//Assigning Xdocument xmlDoc. Loads the xml file from the file path listed. 
		xmlDoc = XDocument.Load("assets/XMLdata/Bparts.xml");

		//This basically breaks down the XML Document into XML Elements. Used later. 
		typeOfParts = xmlDoc.Descendants("BPartsList").Elements ();
	}

	//this is our coroutine that will actually read and assign the XML data to our List 
	IEnumerator AssignData()
	{
		int t = 0;
		/*foreach allows us to look at every Element of our XML file and do something with each one. Basically, this line is saying “for each element in the xml document, do something.*/ 
		foreach (var partType in typeOfParts)
		{
			listOfParts = partType.Elements ();
			foreach (var part in listOfParts) {
				if (BpartName != part.Attribute ("name").Value.Trim ()) {		//if the next element has a new name, select that parrent and assign all it's children to these values
					BpartName = part.Attribute ("name").Value.Trim ();	
					BpartType = part.Parent.Name.ToString();
					MaxHealth = int.Parse (part.Element ("Health").Value.Trim ());

					int numberXCord = part.Element ("gridHitBox").Element ("line").Value.Trim ().Length;	//the length of the design shape line of 1's and 0's
					gridHitBox = part.Element ("gridHitBox").Descendants ();
					int numberYCord = part.Element ("gridHitBox").Descendants ().Count ();		//counts how many lines there are in the targeting grid, giving Y cords size
					int interationY = numberYCord - 1;

					gridOfBodyPart = new int[(int)numberXCord][];
					for (int i = 0; i < numberXCord; i++) {
						gridOfBodyPart [i] = new int[(int)numberYCord];		//instantiating the grid beforehand
					}
					foreach (XElement line in gridHitBox) {
						int interationX = 0;
						string lineOfNumbers = line.Value;
						foreach (char num in lineOfNumbers) {
							int newNum = (int)char.GetNumericValue (num);
							gridOfBodyPart [interationX] [interationY] = newNum;
						
							interationX++;
						}
						interationY--;
					}
					
					if (partType.Name.ToString() == "Torso" || partType.Name.ToString() == "Shoulder") {
						anchorVector2 = new Vector2 (0.0f, 0.0f);		//placeholder
					} else {
						//Debug.Log ((string)partType.Name.ToString());
						storedAnchorPoints = part.Element ("AnchorPoint").Descendants ();
						foreach (XElement cord in storedAnchorPoints) {
							if (cord.Name == "xCord") {
								anchorVector2.x = int.Parse (cord.Value);
							}
							if (cord.Name == "yCord") {
								anchorVector2.y = int.Parse (cord.Value);
							}
						}
					}
					BPartData.Add (new BodyPartDataHolder (BpartName, BpartType, MaxHealth, gridOfBodyPart, anchorVector2));
				}
			}
			//		Debug.Log ("bodydata after add " + BPartData.Count);
			//finishedLoading = true; //tell the program that we’ve finished loading data. 
			Debug.Log(BPartData[t].name);
			t++;
			yield return null;
		}
	}
	public BodyPartDataHolder getBodyData(string requestedNameOfPart){			//future efficiency, have each part be catagorized acording to their part type for better searching
		Debug.Log(BPartData.Find (BodyPartDataHolder => BodyPartDataHolder.name == "heavy arm"));
		return BPartData.Find (BodyPartDataHolder => BodyPartDataHolder.name == "requestedNameOfPart");
	}
}

public class BodyPartDataHolder{
	public string name;
	public string typeOfpart;
	public int maxHealth;
	public int[][] bodyPartGrid;
	public Vector2 anchor;
	public BodyPartDataHolder(string BpartName, string incBpartName, int incMaxHealth, int[][] incomingBodyPartGrid, Vector2 AnchorPoint){
		name = BpartName;
		typeOfpart = incBpartName;
		maxHealth = incMaxHealth;
		anchor = AnchorPoint;
		bodyPartGrid = new int[incomingBodyPartGrid.Length][];
		for(int i=0; i < incomingBodyPartGrid.Length; i++){	//transfering the int[][] grid
			bodyPartGrid [i] = new int[incomingBodyPartGrid[0].Length];
			for(int j=0; j < incomingBodyPartGrid[0].Length; j++){
				bodyPartGrid [i][j] = incomingBodyPartGrid[i][j];
			}
		}
		//Debug.Log (BpartName);
	}
}

