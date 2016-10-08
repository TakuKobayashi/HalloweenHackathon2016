using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleUICanvas : MonoBehaviour {
	[SerializeField] Text debugText;

	public void Initialize(){
	}

	public void PutDebugText(string text){
		if (debugText.IsActive ()) {
			debugText.text = text;
		}
	}
}
