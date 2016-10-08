using System;
using UnityEngine;

public class BattleController : SceneController{
	[SerializeField] BattleField battleField;
	[SerializeField] BattleUICanvas battleUI;
	[SerializeField] Character character;

	private Vector3 movePosition = Vector3.zero;
	private Vector3 startPosition = Vector3.zero;

	public static BattleController Instance{ get{ return (BattleController) Current; } }

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
		if (touchEvent.phase == TouchPhase.Ended) {
			startPosition = Vector3.zero;
			movePosition = Vector3.zero;
		}else{
			if (startPosition == Vector3.zero) {
				startPosition = touchEvent.position;
			} else {
				movePosition = touchEvent.position - startPosition;
			}
		}
	}

	public void AddMessage(string message){
		battleUI.PutDebugText(message);
	}
}
