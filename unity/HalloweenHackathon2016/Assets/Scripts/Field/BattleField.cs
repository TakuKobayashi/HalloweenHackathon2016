using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleField : MonoBehaviour {
	[SerializeField] GameObject skyObj;
	[SerializeField] float skyRotateSpeed = 0.02f;

	public void Initialize(){
	}

	void Update(){
		Vector3 preRotate = skyObj.transform.localRotation.eulerAngles;
		preRotate.y += skyRotateSpeed;
		skyObj.transform.localRotation = Quaternion.Euler(preRotate);
	}
}
