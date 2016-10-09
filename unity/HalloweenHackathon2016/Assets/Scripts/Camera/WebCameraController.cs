using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class WebCameraController : MonoBehaviour {
	private const int FPS = 30;
	private WebCamTexture webcamTexture;
	private string log = "";
	private string log2 = "";
	private Heartrate heartrate = new Heartrate();

	public Action OnBeat;

	void Start () {
		WebCamDevice[] devices = WebCamTexture.devices;
		// display all cameras
		for (int i = 0; i < devices.Length; i++) {
			log = "name:" + devices [i].name + "\n" + "faceing:" + devices [i].isFrontFacing + "\n";
		}

		webcamTexture = new WebCamTexture(devices[0].name, Screen.width, Screen.height, FPS);
		heartrate.setImageSize(webcamTexture.width, webcamTexture.height);
		webcamTexture.Play();
	}

	void Update () {
		if (webcamTexture.isPlaying) {
			Color[] colors = webcamTexture.GetPixels ();
			heartrate.setTexturePixels (colors);
			string tmplog = log + "w:" + webcamTexture.width + "h:" + webcamTexture.height + "\n" + "length:" + colors.Length + "\n" + "red:" + heartrate.getRedLightCounter() + "light:" + heartrate.getLightFieldCount() + "\n";
			//指が当たっていない
			if (!heartrate.checkBeat ()) {
				BattleController.Instance.AddMessage (tmplog + log2);
				heartrate.reset ();
				return;
			}
			if (heartrate.beat ()) {
				if (OnBeat != null)
					OnBeat ();
			}
//			log2 = "beat:" + heartrate.beat () + "bpm:" + heartrate.getBpm () + "span:" + heartrate.getSpan ();
//			BattleController.Instance.AddMessage (tmplog + log2);
		}
	}

	void OnDestroy(){
		if(webcamTexture != null && webcamTexture.isPlaying){
			webcamTexture.Stop();
			webcamTexture = null;
		}
	}
}
