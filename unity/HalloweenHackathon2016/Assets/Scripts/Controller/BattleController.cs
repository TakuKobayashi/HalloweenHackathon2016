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
	private float pressingSecond = 0f;

	public static BattleController Instance{ get{ return (BattleController) Current; } }
	private float time = 0;

	protected override void OnStartSceneLoad(){
		battleField.Initialize ();
		battleUI.Initialize ();
		character.Initialize ();
	}

	void Update(){
		time += Time.deltaTime;
		if (time > (time % appearSecond)) {
			AppearEnemy ();
			time = 0;
		}
	}

	protected override void OnTouchEvent(TouchEvent touchEvent){
		base.OnTouchEvent (touchEvent);
		if (touchEvent.phase == TouchPhase.Ended) {
			if (pressingSecond < 1.0f) {
				character.Attack ();
			} else {
				character.Stop ();
			}
			startPosition = Vector3.zero;
			movePosition = Vector3.zero;
			pressingSecond = 0f;
		}else{
			if (startPosition == Vector3.zero) {
				startPosition = touchEvent.position;
			} else {
				movePosition = touchEvent.position - startPosition;
				character.Move(movePosition);
			}
			pressingSecond += Time.deltaTime;
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
