using System;
using UnityEngine;

public interface InputTouchEvent{
	void AddTouchEventCallback(Action<TouchEvent> onTouch);
	void RemoveTouchEventCallback(Action<TouchEvent> onTouch);
	void OnTouch();
}