using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSquareBehaviour : MonoBehaviour {

//	[SerializeField]
	int gridCordX;
//	[SerializeField]
	int gridCordY;

	public Sprite activatedSprite;
	public Sprite targetedSprite;
	Sprite defaultSprite;
	Sprite storedDefault;

	SpriteRenderer spriteRenderer;
	GridHitController gridHitController;


	void Start(){
		SpriteRenderer spriteRendererTemp = gameObject.GetComponent<SpriteRenderer>();
		if(spriteRendererTemp != null){
			spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
			//activatedSprite =
			defaultSprite = spriteRenderer.sprite;
			storedDefault = spriteRenderer.sprite;
		}
		if(spriteRendererTemp == null){
			Debug.Log ("Cannot find 'spriteRendererTemp'object");
		}

		GameObject gridHitControllerImport = GameObject.FindWithTag ("PlayArea");
		if(gridHitControllerImport != null){
			gridHitController = gridHitControllerImport.GetComponent<GridHitController>();
		}
		if(gridHitControllerImport == null){
			Debug.Log ("Cannot find 'gridHitControllerImport'object");
		}
	}
	public void SetGridCordX(int cordx){
		gridCordX = cordx;
	}
	public void SetGridCordY(int cordy){
		gridCordY = cordy;
	}
	public int GetGridCordX(){
		return gridCordX;
	}
	public int GetGridCordY(){
		return gridCordY;
	}
//	void OnTriggerStay2D(Collider2D other){
//		if (other.CompareTag("weaponHitBox")){
//			spriteRenderer.sprite = activatedSprite;
//		}
//	}
//	void OnTriggerExit2D(Collider2D other){
//		if (other.CompareTag("weaponHitBox")){
//			spriteRenderer.sprite = defaultSprite;
//		}
//	}
		
	void OnMouseEnter(){
		gridHitController.squareHoveredOver (gridCordX, gridCordY);
//		Debug.Log (gridCordX);
//		Debug.Log (gridCordY);
		//Debug.Log ("test");
	}
	void OnMouseExit(){
		gridHitController.squareHoveredOff (gridCordX, gridCordY);
	}
		
	public void TargetSquare(){
		spriteRenderer.sprite = targetedSprite;
	}
	public void UntargetSquare(){
		spriteRenderer.sprite = defaultSprite;
	}
	public void ActivateSquare(){
		spriteRenderer.sprite = activatedSprite;
//		storedDefault = defaultSprite;
//		defaultSprite = activatedSprite;
	}
	public void DeactivateSquare(){
//		defaultSprite = storedDefault;
		spriteRenderer.sprite = defaultSprite;
	}
//	void OnMouseExit(){
//		spriteRenderer.sprite = defaultSprite;
//	}
}
