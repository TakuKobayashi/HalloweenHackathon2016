using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

public class Util{
	public static void Normalize(Transform t)
	{
		t.localPosition = Vector3.zero;
		t.localEulerAngles = Vector3.zero;
		t.localScale = Vector3.one;
	}

	public static GameObject InstantiateTo(GameObject parent, GameObject go)
	{
		GameObject ins = (GameObject)GameObject.Instantiate(
			go, 
			Vector3.zero,
			Quaternion.identity
		);

		ins.transform.SetParent (parent.transform, false);

		//Normalize(ins.transform);
		return ins;
	}

	public static GameObject InstantiateTo(GameObject parent, Prefab prefab)
	{
		return InstantiateTo(parent, prefab.gameObject);
	}

	public static T InstantiateTo<T>(GameObject parent, GameObject go)
		where T : Component
	{
		return InstantiateTo(parent, go).GetComponent<T>();
	}

	public static T InstantiateTo<T>(GameObject parent, Prefab prefab)
		where T : Component
	{
		return InstantiateTo(parent, prefab.gameObject).GetComponent<T>();
	}

	public static void DeleteAllChildren(Transform parent)
	{
		List<Transform> transformList = new List<Transform>();
		foreach (Transform child in parent) transformList.Add(child);
		parent.DetachChildren();
		foreach (Transform child in transformList) GameObject.Destroy(child.gameObject);
	}

	public static AnimationClip LoadAnimationClip(string path){
		AnimationClip clip = Resources.Load<AnimationClip>(path);
		if (clip != null) {
			return AnimationClip.Instantiate<AnimationClip>(clip);
		}
		return null;
	}

	public static float LoadAnimationSecond(Animator animator, string name){
		RuntimeAnimatorController runtimeAnimatorController = animator.runtimeAnimatorController;
		AnimationClip[] clips = runtimeAnimatorController.animationClips;
		// 全てのレイヤーを取り出す
		for(int i = 0; i < clips.Length; ++i) {
			if (clips[i].name == name) {
				return clips [i].length;
			}
		}
		return 0f;
	}

	public static IEnumerator DelayedAction(float delaySecond, Action action){
		yield return new WaitForSeconds (delaySecond);
		action();
	}
}