using UnityEngine;

public interface ITask
{
    string taskName { get; }
    bool IsCompleted { get; }
    void StartTask();
    void UpdateTask();
    void CompleteTask();
}
public abstract class BaseTask : ITask
{
    public string taskName { get; protected set; }

    public bool IsCompleted { get; protected set;}
    public BaseTask(string taskName)
    {
        this.taskName = taskName;
    }


    public virtual void CompleteTask()
    {
        IsCompleted = true;
        //здесь идет отпись от событий и дополнительная логика при исполнении 
    }

    public abstract void StartTask();//подпись на событие если оно нужно

    public abstract void UpdateTask(); //проверка на активность
}
