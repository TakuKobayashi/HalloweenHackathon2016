using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {
	[SerializeField] Transform target;
	[SerializeField] float distance = 5.0f;
	[SerializeField] float height = 2.0f;

	void Update () {
		transform.position = target.position + (-Vector3.forward * distance) + (Vector3.up * height);
		transform.LookAt(target);
	}
}
