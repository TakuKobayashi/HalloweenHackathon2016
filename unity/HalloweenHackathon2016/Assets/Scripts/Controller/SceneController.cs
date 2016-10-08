using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public abstract class SceneController : MonoBehaviour
{
    public static SceneController Current{ get; private set; }

    public bool Loaded { get; private set; }

	public Func<TouchEvent, bool> OnTouch;

    // Private Member
    protected virtual void OnStartSceneLoad()
    {
    }

    protected virtual void OnSceneLoaded()
    {
    }

	protected virtual void OnTouchEvent(TouchEvent touchEvent){
		if (OnTouch != null) OnTouch(touchEvent);
	}

    //=========================
    // Awake
    //=========================
    protected virtual void Awake()
    {
        Current = this;
        Loaded = false;

        // Init SceneName
        Scene scene = SceneManager.GetActiveScene();

        // Loading Object
        StartCoroutine(StartSceneLoad());
    }

    IEnumerator StartSceneLoad()
    {
        // わざと1フレーム遅らせる
        OnStartSceneLoad();

		yield return null;

		GameController.Instance.AddTouchEventCallback(OnTouchEvent);
        OnSceneLoaded();
    }
}