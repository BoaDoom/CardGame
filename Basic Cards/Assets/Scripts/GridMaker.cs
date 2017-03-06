using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMaker : MonoBehaviour {

//	public int gridWidthInBoxes;
//	public int gridHeightInBoxes;
//	public Transform smallSquare;
//
//	public List<Transform> gridList;
//
//	Transform smallGridSize;
//	Transform transformOriginal;
//
//	Vector3 zeroCord;
//	Vector3 smallSquareOutterGap;
//	public int ratioOfGap;
//	public int actualScaleOfPlayArea;
//	Vector3 smallSquareScale;

	public Transform smallSquare;
	Transform transformOriginal;

	public int boxCountX = 10;
	public int boxCountY = 10;

	public float sizeRatioOfSmallBox = 0.8f;

	public List<Transform> gridList;

	Vector3 zeroCord = new Vector3(0.0f, 0.0f, 0.0f);		//
	Vector3 framingBoxSize;
	Vector3 firstBoxCord;

	void Start () {
		
		Transform smallSquareInst;
		transformOriginal = gameObject.transform;
		framingBoxSize = new Vector3(1.0f/boxCountX, 1.0f/boxCountY, 1.0f);
		firstBoxCord = zeroCord + new Vector3 ((-0.5f + framingBoxSize.x / 2), (0.5f - framingBoxSize.y / 2), 0.0f);
		int yi = 0;
		int xi = 0;
		for (int i = 0; i < boxCountX*boxCountY; i++) {
			smallSquareInst = Instantiate (smallSquare, zeroCord, transformOriginal.rotation);
			smallSquareInst.transform.SetParent (gameObject.transform);
			smallSquareInst.transform.localScale = framingBoxSize * sizeRatioOfSmallBox;
			smallSquareInst.transform.localPosition = firstBoxCord + new Vector3(framingBoxSize.x*xi, -framingBoxSize.y*yi, 0.0f);
			if (xi >= boxCountX-1) {
				xi = 0;
				yi++;
			} else {
				xi++;
			}
			gridList.Add (smallSquareInst);
		}
	}

	void Update () {
		
	}
}
