using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class Character : MonoBehaviour {
	private ThirdPersonCharacter TPSCharacter;
	Vector3 startPosition = Vector3.zero;
	Vector3 movePosition = Vector3.zero;

	void Start () {
		TPSCharacter = GetComponent<ThirdPersonCharacter>();
	}

	public void Initialize(){
	}

	public void Move(Vector3 movePosition){
		Vector3 move = movePosition.y * Vector3.forward + movePosition.x * Vector3.right;
		TPSCharacter.Move(move, false, false);
	}
}
