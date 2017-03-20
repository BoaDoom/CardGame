using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMaker : MonoBehaviour {

	public ActiveSquareBehaviour smallSquare;
	Transform transformOriginal;
	public Transform playAreaDetector;
	//public PlayAreaDetectorScript playAreaDetectorScript;

	public int boxCountX = 10;
	public int boxCountY = 10;

	public float sizeRatioOfSmallBox = 1.0f;

	//public List<Transform> gridList;
	private ActiveSquareBehaviour[][] grid;
	private Vector2 gridDimensions;

	Vector3 zeroCord = Vector3.zero;
	Vector3 framingBoxSize;
	public Vector3 firstBoxCord;




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
	void Update () {

	}
}