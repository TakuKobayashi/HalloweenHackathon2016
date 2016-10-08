using System;
using System.Collections.Generic;

public class ThreadChannelManager{
	// 何個のThreadを裏で動かしておくかは後でどうするか考える
	const int WorkerCount = 2;

	private static ThreadChannelManager instance;

	public static ThreadChannelManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new ThreadChannelManager();
			}
			return instance;
		}
	}

	int currentQueueIndex = 0;
	List<JobRequest> requestQueue = new List<JobRequest>();
	WorkerThread[] threadPool;

	private ThreadChannelManager(){
		threadPool = new WorkerThread[WorkerCount];
		for (int i = 0; i<threadPool.Length; ++i) {
			threadPool[i] = new WorkerThread(i);
		}
	}

	public void StartWorkers() {
		for (int i = 0;i < threadPool.Length;++i) {
			threadPool[i].Start();
		}
	}

	public void PutRequest(JobRequest request){
		lock (requestQueue) {
			requestQueue.Add (request);
			requestQueue.Sort ((a, b) => b.Priority - a.Priority);
		}
	}

	public JobRequest PopRequest(){
		if (requestQueue.Count <= 0) return null;
		JobRequest request;
		lock (requestQueue) {
			request = requestQueue [0];
			requestQueue.RemoveAt (0);
		}
		return request;
	}

	public void AbortWorkers(){
		for (int i = 0;i < threadPool.Length;++i) {
			threadPool[i].Abort();
		}
	}
}