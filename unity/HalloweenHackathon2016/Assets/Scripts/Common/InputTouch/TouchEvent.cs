using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchEvent{
	public int fingerId{ get; private set;}
	public TouchPhase phase{ get; private set;}
	public Vector3 position{ get; private set;}

	private TouchEvent(){}

	public static TouchEvent GenerateTouchEvent(int fingerId, TouchPhase phase, Vector3 position){
		TouchEvent tEvent = new TouchEvent ();
		tEvent.fingerId = fingerId;
		tEvent.phase = phase;
		tEvent.position = position;
		return tEvent;
	}

	public List<RaycastResult> hitUiObjects{
		get{
			PointerEventData pointer = new PointerEventData (EventSystem.current);
			pointer.position = this.position;
			List<RaycastResult> result = new List<RaycastResult> ();
			EventSystem.current.RaycastAll (pointer, result);
			return result;
		}
	}
}