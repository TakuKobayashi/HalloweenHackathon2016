using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class Character : MonoBehaviour {
	private ThirdPersonCharacter TPSCharacter;
	Vector3 startPosition = Vector3.zero;
	Vector3 movePosition = Vector3.zero;

	void Start () {
		TPSCharacter = GetComponent<ThirdPersonCharacter>();
		BattleController.Current.OnTouch = MoveControl;
	}

	void Update(){
		move(movePosition);
	}

	bool MoveControl(TouchEvent touchEvent){
		if (touchEvent.phase == TouchPhase.Began) {
			movePosition = Vector3.zero;
			startPosition = touchEvent.position;
		} else if (touchEvent.phase == TouchPhase.Moved) {
			movePosition = touchEvent.position - startPosition;
		} else {
			startPosition = Vector3.zero;
			movePosition = Vector3.zero;
		}
		return true;
	}

	private void move(Vector3 movePosition){
		Vector3 move = movePosition.y * Vector3.forward + movePosition.x * Vector3.right;
		TPSCharacter.Move(move, false, false);
	}
}
