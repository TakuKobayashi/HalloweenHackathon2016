using System;
using System.Collections.Generic;
using UnityEngine;

public class Heartrate{
	private int frameWidth = 0;
	private int frameHeight = 0;

	//指を当てている間は赤がほとんどをしめるため、指が当たっているかどうかの判定に使う
	private int redLightCounter = 0;
	// 明るい部分を数える
	private int lightFieldCount = 0;
	// 指が当たっている時、直前の明るい部分を記録して比較するのに使う
	private int prevSampling = 0;
	// 明るい部分が前回との差分により増減していくことで、1ループで1ビートとカウントできそう
	private int beatCounter = 0;
	//前回とのビートの時でBPがなんとなく出そう
	private DateTime prevBeatTime;
	private int sumLoopBeatCount;
	private List<int> bpmList = new List<int>();

	public Heartrate() {
		reset();
	}

	public void setImageSize(int width, int height) {
		frameWidth = width;
		frameHeight = height;
	}

	public void setTexturePixels(Color[] colors){
		redLightCounter = 0;
		lightFieldCount = 0;
		for (int i = 0; i < colors.Length; ++i) {
			if (colors [i].r > 0.5f) {
				++redLightCounter;
			}
			if (colors [i].grayscale > 0.5f) {
				++lightFieldCount;
			}
		}
	}

	public int getRedLightCounter(){
		return redLightCounter;
	}

	public int getLightFieldCount(){
		return lightFieldCount;
	}

	public bool checkBeat(){
		return redLightCounter > (frameWidth * frameHeight) * 0.9;
	}

	public void reset() {
		prevSampling = 0;
		beatCounter = 0;
		prevBeatTime = DateTime.Now;
		sumLoopBeatCount = 0;
		bpmList.Clear();
	}

	public bool beat() {
		if (prevSampling == 0) {
			prevSampling = lightFieldCount;
			prevBeatTime = DateTime.Now;
		} else {
			if (prevSampling < lightFieldCount) {
				++beatCounter;
				if (beatCounter > 1) beatCounter = 1;
			} else {
				--beatCounter;
				if (beatCounter < -1) beatCounter = -1;
			}
			prevSampling = lightFieldCount;
			if (beatCounter == 0) {
				++sumLoopBeatCount;
				if (sumLoopBeatCount % 2 == 0) {
					TimeSpan span = getSpan();
					Debug.Log (span.TotalMinutes);
					int bpm = (int) ((float) 1 / span.TotalMinutes);
					prevBeatTime = DateTime.Now;
					bpmList.Add(bpm);
					return true;
				}
			}
		}
		return false;
	}

	public int getBpm() {
		int sum = 0;
		for(int i = 0;i < bpmList.Count;++i) {
			sum += bpmList[i];
		}
		return sum / bpmList.Count;
	}

	public TimeSpan getSpan() {
		return DateTime.Now - prevBeatTime;
	}
}