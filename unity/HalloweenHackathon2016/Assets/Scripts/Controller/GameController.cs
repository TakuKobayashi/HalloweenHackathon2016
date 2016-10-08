using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameController : SingletonBehaviour<GameController>, InputTouchEvent{
	bool isReady = false;

	public static bool IsReady { get { return Instance.isReady; } }

	public override void SingleAwake() {
//		Application.logMessageReceived += OnLogCallback;
//		Application.logMessageReceivedThreaded += OnLogCallback;

		//ThreadChannelManager.Instance.StartWorkers();
		Initialize ();
	}

	void Update(){
		this.OnTouch();
	}

	void OnDestroy(){
		//ThreadChannelManager.Instance.AbortWorkers();
	}

	public static void Initialize(){

	}

	void OnLogCallback(string message, string trace, LogType type) {
//		if(type == LogType.Error) {
//		}
	}

	private List<Action<TouchEvent>> touchEventCallbackList = new List<Action<TouchEvent>>();

	//以下InputTouchEventのための処理
	public void AddTouchEventCallback(Action<TouchEvent> onTouch){
		touchEventCallbackList.Add(onTouch);
	}

	public void RemoveTouchEventCallback(Action<TouchEvent> onTouch){
		touchEventCallbackList.Remove(onTouch);
	}

	public void OnTouch(){
		// 現在この場面でしかタップを使わないため一番軽い方法で実装.
		#if UNITY_EDITOR
		// マウスの判定.
		TouchEvent touchEvent = null;
		if (Input.GetMouseButtonDown(0)) 
		{
			touchEvent = TouchEvent.GenerateTouchEvent(0, TouchPhase.Began, Input.mousePosition);
		}
		else if (Input.GetMouseButton(0))     
		{
			touchEvent = TouchEvent.GenerateTouchEvent(0, TouchPhase.Moved, Input.mousePosition);
		}
		else if (Input.GetMouseButtonUp(0)) 
		{
			touchEvent = TouchEvent.GenerateTouchEvent(0, TouchPhase.Ended, Input.mousePosition);
		}
		if(touchEvent == null) return;
		for(int i = 0;i < touchEventCallbackList.Count;++i){
			touchEventCallbackList[i](touchEvent);
		}
		#else
		// 実機の判定.
		for(int i = 0;i < Input.touchCount;++i){
		Touch touch = Input.GetTouch(i);
		TouchEvent touchEvent = TouchEvent.GenerateTouchEvent(touch.fingerId, touch.phase, touch.position);
		for(int j = 0;j < touchEventCallbackList.Count;++j){
		touchEventCallbackList[j](touchEvent);
		}
		}
		#endif
	}
}
