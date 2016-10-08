using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : SceneController{
	[SerializeField] BattleField battleField;
	[SerializeField] BattleUICanvas battleUI;
	[SerializeField] Character character;
	[SerializeField] List<Prefab> enemyPrefabList;
	[SerializeField] float appearSecond = 5f;

	private Vector3 movePosition = Vector3.zero;
	private Vector3 startPosition = Vector3.zero;

	public static BattleController Instance{ get{ return (BattleController) Current; } }
	private float time = 0;

	protected override void OnStartSceneLoad(){
		battleField.Initialize ();
		battleUI.Initialize ();
		character.Initialize ();
	}

	void Update(){
		character.Move(movePosition);
		time += Time.deltaTime;
		if (time > (time % appearSecond)) {
			AppearEnemy ();
			time = 0;
		}
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

	public void AppearEnemy(){
		int enemyPrefabNum = UnityEngine.Random.Range(0, enemyPrefabList.Count);
		Enemy enemy = enemyPrefabList [enemyPrefabNum].InstantiateTo<Enemy>(battleField.transform);
		enemy.Appear (character.transform);
	}

	public void AddMessage(string message){
		battleUI.PutDebugText(message);
	}
}
