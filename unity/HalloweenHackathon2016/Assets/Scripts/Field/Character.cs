using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class Character : MonoBehaviour {
	private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
	private Transform m_Cam;                  // A reference to the main camera in the scenes transform
	private Vector3 m_CamForward;             // The current forward direction of the camera
	private Vector3 m_Move;
	private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.

	Vector3 prevPosition = Vector3.zero;

	void Start () {
		m_Character = GetComponent<ThirdPersonCharacter>();
		BattleController.Current.OnTouch = MoveControl;
	}

	bool MoveControl(TouchEvent touchEvent){
		if (touchEvent.phase == TouchPhase.Began) {
			prevPosition = touchEvent.position;
		} else if (touchEvent.phase == TouchPhase.Moved) {
			Vector3 movedPosition = touchEvent.position - prevPosition;
			move (movedPosition);
//			this.transform.position = transform.position + movedPosition;
			prevPosition = touchEvent.position;
		} else {
			move (Vector3.zero);
		}
		return true;
	}

	private void move(Vector3 movePosition){
		Vector3 move = movePosition.y * Vector3.forward + movePosition.x * Vector3.right;
		m_Character.Move(move, false, false);
	}
}
