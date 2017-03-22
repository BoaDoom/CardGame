using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHitController : MonoBehaviour {

	public ActiveSquareBehaviour smallSquare; //added manually inside unity from prefabs
	Transform transformOriginal;
	public Transform playAreaDetector; //added manually inside unity
	//public PlayAreaDetectorScript playAreaDetectorScript;
	public GameControllerScript gameControllerScript; //added manually inside unity

	public int boxCountX = 10;
	public int boxCountY = 10;

	public float sizeRatioOfSmallBox = 1.0f;

	//public List<Transform> gridList;
	private ActiveSquareBehaviour[][] grid;
	private Vector2 gridDimensions;

	Vector3 zeroCord = Vector3.zero;
	Vector3 framingBoxSize;
	public Vector3 firstBoxCord;

	//Vector2 testVec2;



	void Start () {

		gridDimensions = new Vector2(boxCountX, boxCountY);

		playAreaDetector.localScale = new Vector3(1.0f, 1.0f,1.0f);
		playAreaDetector.position = gameObject.transform.position;

		ActiveSquareBehaviour smallSquareInst;
		transformOriginal = gameObject.transform;
		framingBoxSize = new Vector3(1.0f/boxCountX, 1.0f/boxCountY, 1.0f);
		firstBoxCord = zeroCord + new Vector3 ((-0.5f + framingBoxSize.x / 2), (0.5f - framingBoxSize.y / 2), 0.0f);
		//int yi = 0;
		//int xi = 0;
		grid = new ActiveSquareBehaviour[(int)gridDimensions.x][];
		for (int x = 0; x < gridDimensions.x; x++){
			grid[x] = new ActiveSquareBehaviour[(int)gridDimensions.y];
			for (int y = 0; y < gridDimensions.y; y++)
			{
				smallSquareInst = Instantiate (smallSquare, zeroCord, transformOriginal.rotation);
				smallSquareInst.transform.SetParent (gameObject.transform);
				smallSquareInst.transform.localScale = framingBoxSize * sizeRatioOfSmallBox;
				smallSquareInst.transform.localPosition = firstBoxCord + new Vector3(framingBoxSize.x*x, -framingBoxSize.y*y, 0.0f);
				smallSquareInst.SetGridCordX (x);
				smallSquareInst.SetGridCordY (y);

				grid[x][y] = smallSquareInst;

			}
		}
	}
	public ActiveSquareBehaviour getSmallSquare(){
		//Debug.Log (grid [0] [0].GetComponent<BoxCollider2D>);
		return grid [0] [0];
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
						grid [(int)tempStartingPoint.x + x] [(int)tempStartingPoint.y + y].ActivateSquare ();		//activates the squares inside the area
					}
//					Debug.Log (x);
//					Debug.Log (y);
				}
			}

			//Debug.Log(middleOfWeaponHitArea);

			//Debug.Log(gameControllerScript.currentClickedOnCardWeaponMatrix.weaponHitData.gridOfHit[0].Length);
			//Debug.Log(gameControllerScript.currentClickedOnCardWeaponMatrix.weaponHitData.gridOfHit.Length);
			//grid [xCord] [yCord].ActivateSquare ();	
		}

		//testVec2 = new Vector2 (xCord, yCord);
	}
	public void squareHoveredOff(int xCord, int yCord){
		resetSmallSquares ();
		//grid [xCord] [yCord].DeactivateSquare ();	
	}
	public void resetSmallSquares(){
		foreach(ActiveSquareBehaviour[] gridY in grid){
			foreach (ActiveSquareBehaviour square in gridY) {
				square.DeactivateSquare ();
			}
		}
	}
	void Update () {

	}
}