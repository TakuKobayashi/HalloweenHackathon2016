using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WorkerThread{
	bool isRunning = true;
	int workerNumber;
	Thread backgroundThread;

	public WorkerThread(int number){
		backgroundThread = new Thread(Run);
		workerNumber = number;
		isRunning = false;
		Debug.Log("Init");
	}

	public void Start(){
		Debug.Log("Start");
		isRunning = true;
		backgroundThread.Start ();
	}

	void Run(){
		while(isRunning){
			JobRequest request = ThreadChannelManager.Instance.PopRequest();
			if (request != null) {
				Debug.Log("Prepare" + workerNumber.ToString());
				request.Prepare(workerNumber);
				Debug.Log("Execute" + workerNumber.ToString());
				request.Execute(workerNumber);
				Debug.Log("Finish" + workerNumber.ToString());
				request.Finish(workerNumber);
			}
		}
	}

	public new void Abort(){
		backgroundThread.Abort ();
		isRunning = false;
		Debug.Log("Abort");
	}
}