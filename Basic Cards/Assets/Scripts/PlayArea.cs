﻿using System.Collections;
using System.Collections.Generic;
using System.Xml; //Needed for XML functionality
using System.Xml.Serialization; //Needed for XML Functionality
using System.IO;
using UnityEngine;

public class PlayArea: MonoBehaviour {

	public List<XMLBodyHitData> bodyLoaderData;

	public XMLBodyLoaderScript XMLBODYloaDER;

	public ActiveSquareBehaviour smallSquare; //added manually inside unity from prefabs
	Transform transformOriginal;
	public GameControllerScript gameControllerScript; //added manually inside unity

	int boxCountX = 7;
	int boxCountY = 8;

	public float sizeRatioOfSmallBox = 1.0f;

	private ActiveSquareBehaviour[][] grid;
	private Vector2 gridDimensions;
	private ActiveSquareState[][] gridOfStates;

	Vector3 zeroCord = Vector3.zero;
	Vector3 framingBoxSize;
	public Vector3 firstBoxCord;





	void Start () {
		GameObject XMLBodyHitLoaderScriptTEMP = GameObject.FindWithTag("BodyLoader");
		//Debug.Log ("XMLBODYTEMP "+ XMLBodyHitLoaderScriptTEMP);
		if(XMLBodyHitLoaderScriptTEMP != null){
			bodyLoaderData = XMLBodyHitLoaderScriptTEMP.GetComponent<XMLBodyLoaderScript>().bodyData;
//			Debug.Log ("bodyLoaderData count " +bodyLoaderData.Count);
		}
			
		if(XMLBodyHitLoaderScriptTEMP == null){
			Debug.Log ("Cannot find 'BodyLoader'object");}
//		XMLBODYloaDER.getBodyData();
//		Debug.Log(bodyLoaderData.Count);
//		boxCountX = bodyLoaderData [0].XDimOfBody;
//		boxCountY = bodyLoaderData [0].YDimOfBody;
		gridDimensions = new Vector2(boxCountX, boxCountY);

		//playAreaDetector.localScale = new Vector3(1.0f, 1.0f,1.0f);
		//playAreaDetector.position = gameObject.transform.position;

		ActiveSquareBehaviour smallSquareInst;
		transformOriginal = gameObject.transform;
		framingBoxSize = new Vector3(1.0f/boxCountX, 1.0f/boxCountY, 1.0f);
		firstBoxCord = zeroCord + new Vector3 ((-0.5f + framingBoxSize.x / 2), (0.5f - framingBoxSize.y / 2), 0.0f);
		//int yi = 0;
		//int xi = 0;
		gridOfStates = new ActiveSquareState[(int)gridDimensions.x][];	//grid of data for the prefab squares' states
		grid = new ActiveSquareBehaviour[(int)gridDimensions.x][];		//grid of prefab ActiveSquare
		for (int x = 0; x < gridDimensions.x; x++){
			gridOfStates [x] = new ActiveSquareState[(int)gridDimensions.y];
			grid[x] = new ActiveSquareBehaviour[(int)gridDimensions.y];
			for (int y = 0; y < gridDimensions.y; y++)
			{
				smallSquareInst = Instantiate (smallSquare, zeroCord, transformOriginal.rotation);
				smallSquareInst.transform.SetParent (gameObject.transform);
				smallSquareInst.transform.localScale = framingBoxSize * sizeRatioOfSmallBox;
				smallSquareInst.transform.localPosition = firstBoxCord + new Vector3(framingBoxSize.x*x, -framingBoxSize.y*y, 0.0f);
				smallSquareInst.SetGridCordX (x);
				smallSquareInst.SetGridCordY (y);

				if (bodyLoaderData.Find(XMLBodyHitData => XMLBodyHitData.nameOfBody == "plainTestBody").gridOfBody [x] [y] == 1) {
					smallSquareInst.OccupiedSquare();

				}

				grid[x][y] = smallSquareInst;
				gridOfStates[x][y] = smallSquareInst.activeSquareState;

//				grid[x][y].ActivateSquare ();
//				Debug.Log (x);
//				Debug.Log (y);
//				Debug.Log (bodyLoaderData.Find (XMLBodyHitData => XMLBodyHitData.nameOfBody == "plainTestBody").nameOfBody);
//				Debug.Log (bodyLoaderData.Find (XMLBodyHitData => XMLBodyHitData.nameOfBody == "plainTestBody").gridOfBody [x] [y]);


			}
		}
		//activateSmallSquare ();
//		Debug.Log (grid.Length +" "+ grid[0].Length );
	}
//	public void activateSmallSquare(){
//		
//		for (int x = 0; x < gridDimensions.x; x++){
////			grid[x] = new ActiveSquareBehaviour[(int)gridDimensions.y];
//			for (int y = 0; y < gridDimensions.y; y++)
//			{
////				grid [x] [y].ActivateSquare ();
//			}
//		}
//		//grid [x] [y].ActivateSquare ();
//	}
	public ActiveSquareBehaviour getSmallSquare(){
		//Debug.Log (grid [0] [0].GetComponent<BoxCollider2D>);
		return grid [0] [0];
	}
//	public ActiveSquareState[][] getActiveSquaresStates(){
//		//ActiveSquareState[][] gridOfStates;
//		int x = 0;
//		gridOfStates = new ActiveSquareState[(int)gridDimensions.x][];
////		Debug.Log ("x " + gridDimensions.x);
//		foreach(ActiveSquareBehaviour[] gridY in grid){
//			int y = 0;
////			Debug.Log ("y " + gridDimensions.y);
//			gridOfStates [x] = new ActiveSquareState[(int)gridDimensions.y];
//			foreach (ActiveSquareBehaviour square in gridY) {
//				gridOfStates[x][y] = square.activeSquareState;
//				//Debug.Log("state inside loop " +square.activeSquareState.getOccupiedState ());
//
//			}
//			x++;
//		}
//		return gridOfStates;
//	}
	public bool getActiveSquareStateSoftTarget(int xcordT, int ycordT){
		return gridOfStates [xcordT] [ycordT].getSoftTargetedState ();
	}
	public bool getActiveSquareStateOccupied(int xcordT, int ycordT){
		return gridOfStates [xcordT] [ycordT].getOccupiedState ();
	}

	public void squareHoveredOver(int xCord, int yCord){		//method used by the grid of active squares to signal that they are being hovered over
		if(gameControllerScript.currentClickedOnCardWeaponMatrix.isCardClickedOn){	//checks the main game controller to see if a card on the table has sent the signal that it is clicked on
			Vector2 middleOfWeaponHitArea = new Vector2(Mathf.Round((gameControllerScript.currentClickedOnCardWeaponMatrix.weaponHitData.gridOfHit[0].Length/2)),
				Mathf.Round((gameControllerScript.currentClickedOnCardWeaponMatrix.weaponHitData.gridOfHit.Length/2)));		//rounding the dimensions of the weaponhitArea to find the 'center' to base activate the grid
			//if (
			Vector2 upperLeftStartingPoint = new Vector2(xCord - middleOfWeaponHitArea.x, yCord - middleOfWeaponHitArea.y);
			//Debug.Log ("test");
			for (int x =0; x < gameControllerScript.currentClickedOnCardWeaponMatrix.weaponHitData.gridOfHit[0].Length; x++){
				//grid [upperLeftStartingPoint.x + i] [upperLeftStartingPoint.y].ActivateSquare ();
				for (int y = 0; y < gameControllerScript.currentClickedOnCardWeaponMatrix.weaponHitData.gridOfHit.Length; y++) {
					Vector2 tempStartingPoint = new Vector2 (upperLeftStartingPoint.x, upperLeftStartingPoint.y);
//					if (upperLeftStartingPoint.x<0){
//						tempStartingPoint.x = 0;
//					}
//					if (upperLeftStartingPoint.y<0){
//						tempStartingPoint.y = 0;
//					}
					if (gameControllerScript.currentClickedOnCardWeaponMatrix.weaponHitData.gridOfHit[x][y] != 0	//checks the grid hit area to see if its turned 'on' with 1, or 'off' with 0
							&& ((tempStartingPoint.x +x)>=0) && ((tempStartingPoint.y +y)>=0)						//checks if the grid hit area is outside of the grid target up and to the left
							&& ((tempStartingPoint.x +x)<boxCountX) && ((tempStartingPoint.y +y)<boxCountY)){		//checks if the grid hit area is outside of the grid target down and to the right
						grid [(int)tempStartingPoint.x + x] [(int)tempStartingPoint.y + y].TargetSquare ();		//activates the squares inside the area
					}

				}
			}
		}
		//gameControllerScript.square

		//testVec2 = new Vector2 (xCord, yCord);
	}
	public void squareHoveredOff(){
		hardResetSmallSquares ();
		//grid [xCord] [yCord].DeactivateSquare ();	
	}
	public void softResetSmallSquares(){
		foreach(ActiveSquareBehaviour[] gridY in grid){
			foreach (ActiveSquareBehaviour square in gridY) {
				square.softUntargetSquare ();
			}
		}
	}
	public void hardResetSmallSquares(){		//gamecontroller sent once another card is clicked on
		foreach(ActiveSquareBehaviour[] gridY in grid){
			foreach (ActiveSquareBehaviour square in gridY) {
				square.hardUntargetSquare ();
			}
		}
	}
}
//public class activeSquareState