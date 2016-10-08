using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleUICanvas : MonoBehaviour {
	[SerializeField] Text debugText;
	[SerializeField] Prefab WebCameraControllerPrefab;

	WebCameraController webCamera;

	public void Initialize(){
	}

	public void PutDebugText(string text){
		if (debugText.IsActive ()) {
			debugText.text = text;
		}
	}

	public void OnCameraToggleChange(bool change){
		if (change) {
			webCamera = WebCameraControllerPrefab.InstantiateTo<WebCameraController> (this.transform);
		} else {
			if (webCamera != null) {
				Destroy (webCamera.gameObject);
			}
		}
	}
}
