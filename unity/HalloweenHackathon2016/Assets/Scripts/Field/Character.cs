using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class Character : MonoBehaviour {
	private ThirdPersonCharacter TPSCharacter;
	Vector3 startPosition = Vector3.zero;
	Vector3 movePosition = Vector3.zero;
	[SerializeField] private float characterSpeed = 2.0f;
	Animator animator;
	float moveStartTime = 0f;
	float moveMultiper = 0f;

	void Start () {
		TPSCharacter = GetComponent<ThirdPersonCharacter>();
		animator = GetComponent<Animator>();
	}

	public void Initialize(){
	}

	public void Move(Vector3 movePosition){
		if (moveStartTime < 2.0f) {
			animator.SetInteger ("MoveState", 1);
			moveMultiper = 1.0f;
		} else {
			animator.SetInteger ("MoveState", 2);
			moveMultiper = 2.0f;
		}
		moveStartTime += Time.deltaTime;

		Vector3 move = movePosition.y * Vector3.forward + movePosition.x * Vector3.right;
		TPSCharacter.Move(move, false, false);
		Vector3 pos = transform.TransformDirection (Vector3.forward) * characterSpeed * moveMultiper * Time.deltaTime;
		transform.position += pos;
	}

	public void Stop(){
		moveStartTime = 0f;
		moveMultiper = 0f;
		animator.SetInteger ("MoveState", 0);
	}

	public void Attack(){
		animator.SetTrigger("Attack");
	}
}
