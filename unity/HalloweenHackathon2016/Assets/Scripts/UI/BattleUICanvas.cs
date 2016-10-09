using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleUICanvas : MonoBehaviour {
	[SerializeField] Text debugText;
	[SerializeField] Prefab WebCameraControllerPrefab;
	[SerializeField] Image hpBar;
	[SerializeField] Image bloodBar;
	private Vector2 maxHpBarSize = Vector2.zero;
	private Vector2 maxBloodBarSize = Vector2.zero;
	public float hp = 1000;
	public float blood = 1000;

	WebCameraController webCamera;

	void Start(){
		maxHpBarSize = bloodBar.GetComponent<RectTransform> ().sizeDelta;
		maxBloodBarSize = bloodBar.GetComponent<RectTransform> ().sizeDelta;
	}

	public void Initialize(){
	}

	void Update(){
		float pos = maxBloodBarSize.x * blood / 1000f;
		Vector2 size = new Vector2 (pos, maxBloodBarSize.y);
		bloodBar.GetComponent<RectTransform> ().sizeDelta = size;
		blood -= 1;
	}

	public void PutDebugText(string text){
		if (debugText.IsActive ()) {
			debugText.text = text;
		}
	}

	public void OnCameraToggleChange(bool change){
		if (change) {
			webCamera = WebCameraControllerPrefab.InstantiateTo<WebCameraController> (this.transform);
			webCamera.OnBeat = () => {
				blood += 10;
				blood = Mathf.Min(1000f, blood);
			};
		} else {
			if (webCamera != null) {
				Destroy (webCamera.gameObject);
			}
		}
	}
}
