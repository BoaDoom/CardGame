using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMaker : MonoBehaviour {

	public int gridWidthInBoxes;
	public int gridHeightInBoxes;
	public Transform smallSquare;

	public List<Transform> gridList;

	Transform smallGridSize;
	Transform transformOriginal;

	Vector3 zeroCord;
	Vector3 smallSquareOutterGap;
	public int ratioOfGap;
	public int actualScaleOfPlayArea;
	Vector3 smallSquareScale;

	void Start () {
		ratioOfGap = 50;
		actualScaleOfPlayArea = 5;
		transformOriginal = gameObject.transform;
		smallSquareScale = new Vector3 ((gameObject.transform.localScale.x / ratioOfGap), (gameObject.transform.localScale.y) / ratioOfGap, 0.0f);
		smallSquareOutterGap = new Vector3 (gameObject.transform.localScale.x / (gridWidthInBoxes*actualScaleOfPlayArea), gameObject.transform.localScale.y / (gridHeightInBoxes*actualScaleOfPlayArea), 0.0f);
		zeroCord = new Vector3(transformOriginal.position.x-2.5f +smallSquareOutterGap.x*2.5f, transformOriginal.position.y+2.5f, transformOriginal.position.z);
		for (int i=0; i < (/*gridWidthInBoxes*gridHeightInBoxes*/10); i++){
			Transform smallSquareInst;
			smallSquareInst = Instantiate (smallSquare, zeroCord, transformOriginal.rotation);
			zeroCord.x += gameObject.transform.localScale.x / gridWidthInBoxes;
			smallSquareInst.transform.SetParent (gameObject.transform);
			smallSquareInst.transform.localScale = smallSquareScale;
			gridList.Add(smallSquareInst);


			//smallGridSize.transform.localScale = transformOriginal.localScale / 12;
			//smallGridSize.localScale.y = transformOriginal.localScale.y / 12;
			//smallGridSize. = transformOriginal.localScale.z / 12;
		}
	}

	void Update () {
		
	}
}
