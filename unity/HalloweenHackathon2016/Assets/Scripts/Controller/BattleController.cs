using System;
using UnityEngine;

public class BattleController : SceneController{
	[SerializeField] BattleField battleField;
	[SerializeField] BattleUICanvas battleUI;
	[SerializeField] Character character;

	private Vector3 movePosition = Vector3.zero;
	private Vector3 startPosition = Vector3.zero;

	protected override void OnStartSceneLoad(){
		battleField.Initialize ();
		battleUI.Initialize ();
		character.Initialize ();
	}

	void Update(){
		character.Move(movePosition);
	}

	protected override void OnTouchEvent(TouchEvent touchEvent){
		base.OnTouchEvent (touchEvent);
		if (touchEvent.phase == TouchPhase.Began) {
			movePosition = Vector3.zero;
			startPosition = touchEvent.position;
		} else if (touchEvent.phase == TouchPhase.Moved) {
			movePosition = touchEvent.position - startPosition;
		} else {
			startPosition = Vector3.zero;
			movePosition = Vector3.zero;
		}
		battleUI.PutDebugText("Phase:" + touchEvent.phase + "\n" + "TouchPosition:" + touchEvent.position + "\n" + "StartPosition:" + startPosition + "\n" + "MovePosition:" + movePosition);
	}
}
