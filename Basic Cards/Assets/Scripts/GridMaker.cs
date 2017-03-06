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

	public float sizeRatioOfSmallBox = 0.9f;

	//int 

	Vector3 zeroCord = new Vector3(0.0f, 0.0f, 0.0f);		//
	Vector3 framingBoxSize;
	Vector3 firstBoxCord;

	void Start () {
		

		Transform smallSquareInst;
		transformOriginal = gameObject.transform;
		framingBoxSize = new Vector3(1.0f/boxCountX, 1.0f/boxCountY, 1.0f);
		firstBoxCord = zeroCord + new Vector3 ((-0.5f + framingBoxSize.x / 2), (0.5f - framingBoxSize.y / 2), 0.0f);

		smallSquareInst = Instantiate (smallSquare, zeroCord, transformOriginal.rotation);
		smallSquareInst.transform.SetParent (gameObject.transform);


		smallSquareInst.transform.localScale = framingBoxSize * sizeRatioOfSmallBox;

		smallSquareInst.transform.localPosition = firstBoxCord;

		//smallSquareInst.transform.localScale = framingBoxSize * sizeRatioOfSmallBox;		//setting box to be smaller







//		ratioOfGap = 50;
//		actualScaleOfPlayArea = 5;
//		transformOriginal = gameObject.transform;
//		smallSquareScale = new Vector3 ((gameObject.transform.localScale.x / ratioOfGap), (gameObject.transform.localScale.y) / ratioOfGap, 0.0f);
//		smallSquareOutterGap = new Vector3 (gameObject.transform.localScale.x / (gridWidthInBoxes*actualScaleOfPlayArea), gameObject.transform.localScale.y / (gridHeightInBoxes*actualScaleOfPlayArea), 0.0f);
//		zeroCord = new Vector3(transformOriginal.position.x-2.5f +smallSquareOutterGap.x*2.5f, transformOriginal.position.y+2.5f, transformOriginal.position.z);
//		for (int i=0; i < (/*gridWidthInBoxes*gridHeightInBoxes*/10); i++){
//			Transform smallSquareInst;
//			smallSquareInst = Instantiate (smallSquare, zeroCord, transformOriginal.rotation);
//			zeroCord.x += gameObject.transform.localScale.x / gridWidthInBoxes;
//			smallSquareInst.transform.SetParent (gameObject.transform);
//			smallSquareInst.transform.localScale = smallSquareScale;
//			gridList.Add(smallSquareInst);
//
//
			//smallGridSize.transform.localScale = transformOriginal.localScale / 12;
			//smallGridSize.localScale.y = transformOriginal.localScale.y / 12;
			//smallGridSize. = transformOriginal.localScale.z / 12;
//		}
	}

	void Update () {
		
	}
}
