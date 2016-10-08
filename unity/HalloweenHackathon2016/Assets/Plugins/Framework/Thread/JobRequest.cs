using System;

public class JobRequest{
	public Action<int> OnPrepare;
	public Action<int> OnExecute;
	public Action<int> OnFinish;

	private int priority;

	public int Priority{
		set{
			priority = value;
		}
		get{
			return priority;
		}
	}

	public void Prepare(int workerNumber){
		if (OnPrepare != null) OnPrepare(workerNumber);
	}

	public void Execute(int workerNumber){
		if (OnExecute != null) OnExecute(workerNumber);
	}

	public void Finish(int workerNumber){
		if (OnFinish != null) OnFinish(workerNumber);
	}
}