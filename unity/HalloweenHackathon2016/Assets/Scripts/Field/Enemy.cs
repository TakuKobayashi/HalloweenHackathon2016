using System;
using UnityEngine;

public class Enemy : MonoBehaviour {
	[SerializeField] private int actionState;
	int currentActionState = 0;
	Animator animator;

	[SerializeField] private EnemyLogic logic;

	void Awake(){
		animator = GetComponent<Animator> ();
	}

	public void Appear(Transform target){
		Vector3 targetPos = target.position;
		Vector3 appearPosition = new Vector3(targetPos.x + LotPosition(), targetPos.y, targetPos.z + LotPosition());
		this.transform.position = appearPosition;
	}

	private float LotPosition(){
		float pos = UnityEngine.Random.Range (logic.appearDistanceMin, logic.appearDistanceMax);
		int num = UnityEngine.Random.Range(0,1);
		if (num == 0) {
			return pos;
		} else {
			return -pos;
		}
	}

	void Update(){
		if (currentActionState != actionState) {
			animator.SetInteger("ActionState", actionState);
			currentActionState = actionState;
		}
	}
}