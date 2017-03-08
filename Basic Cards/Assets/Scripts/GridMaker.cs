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
	public Transform playAreaDetector;
	public PlayAreaDetectorScript playAreaDetectorScript;

	public int boxCountX = 10;
	public int boxCountY = 10;

	public float sizeRatioOfSmallBox = 0.8f;

	public List<Transform> gridList;

	Vector3 zeroCord = new Vector3(0.0f, 0.0f, 0.0f);		//
	Vector3 framingBoxSize;
	Vector3 firstBoxCord;

	public RaycastHit2D hit;
	public Ray ray;
	public Vector3 rayPosition;
	//public bool mouseOnPlayArea;
	public float mouseXpos;
	public float mouseYpos;
	public Vector3 junkV3;
	public Vector2 origin;

	public Transform sphere;
	public Transform cube;

	RaycastHit2D[] hitInfo;

	void Start () {
		//mouseOnPlayArea = false;

		playAreaDetector.localScale = new Vector3(1.0f, 1.0f,1.0f);
		playAreaDetector.position = gameObject.transform.position;

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
			smallSquareInst.transform.localPosition = firstBoxCord + new Vector3(framingBoxSize.x*xi, -framingBoxSize.y*yi, -0.1f);
			if (xi >= boxCountX-1) {
				xi = 0;
				yi++;
			} else {
				xi++;
			}
			gridList.Add (smallSquareInst);
		}
	}

//	void hoverOverHighlight(){
//		RaycastHit hit2;
//		Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
//		if (Physics.Raycast(ray, out hit2)){
//			rayHit.collider.enabled = false;
//			Debug.Log ("raycasted");
//		}
//	}
//	void OnMouseEnter(){
//		mouseOnPlayArea = true;
//		Debug.Log ("gridmaker mouseonplayarea true");
//	}
//	void OnMouseExit(){
//		mouseOnPlayArea = false;
//		Debug.Log ("gridmaker mouseonplayarea false");
//	}
	void Update () {
		
		if (Input.GetMouseButtonDown(0))
		{
			float rayDistance = 20.0f;
			Vector3 mouseWorldPosition = Camera.main.transform.position;
			Vector3 screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
			Vector3 mouseWorldPosition2 = Camera.main.ScreenToWorldPoint(new Vector3((Input.mousePosition).x, (Input.mousePosition).y, screenPoint.z));
			Ray ray = new Ray (mouseWorldPosition2, Vector3.zero);
				//Debug.Log ("cast");
			hitInfo = Physics2D.GetRayIntersectionAll(ray, rayDistance);
			Debug.Log (hitInfo.Length);
			for(int i =0; hitInfo.Length > i; i++){
				Debug.Log (hitInfo[i].collider.gameObject.name);
			}
			
			//origin = new Vector2(junkV3.x, junkV3.y);
			//if (Physics2D.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), Vector2.zero)){
			//Debug.Log(hitInfo.transform.name);
			//}


			sphere.position = mouseWorldPosition;
			cube.position = mouseWorldPosition2;
		}
//		mouseXpos = Input.mousePosition.x;
//		mouseYpos = Input.mousePosition.y;
//		mouseXposAC = (new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f));
//		rayPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
//		//rayPosition = new Vector3 ((Input.mousePosition).x, (Input.mousePosition).y, 0.0f);
//		ray = new Ray (rayPosition, Vector3.zero);
//		hit = Physics2D.GetRayIntersection(ray, 0.0f);
//		if (hit) {
//			Debug.Log (hit.transform.name);
//		}

//		mouseXpos = Input.mousePosition.x;
//		mouseYpos = Input.mousePosition.y;
//		mouseXposAC = (new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f));
//		mouseYposAC = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f));
//		rayPosition = new Vector3 ((Input.mousePosition).x, (Input.mousePosition).y, 0.0f);
//		ray = new Ray (rayPosition, new Vector3(5.0f, 5.0f, 5.0f));
//		hit = Physics2D.GetRayIntersection(ray, 20.0f);
//		if (hit) {
//			Debug.Log (hit.transform.name);
//		}

//		//ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//		if (Physics2D.Raycast (ray, out hit)) {
//			if (hit.transform.tag == "Card") {
//				//hit.transform. = false;
//				Debug.Log ("raycasted");
//			}
//			Debug.Log (hit.transform.tag);
//		}
//		if 
//		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//			RaycastHit hit2;
//			Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
//			if (Physics.Raycast(ray, out hit2)){
//				rayHit.collider.enabled = false;
//				Debug.Log ("raycasted");
//			}
	}
}
