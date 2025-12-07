using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField]private List<ITask> _activeTasks = new();
    [SerializeField]private List<ITask> _completedTasks = new();

    public void AddTask(ITask task)
    {
        _activeTasks.Add(task);
        task.StartTask();
    }

    public void UpdateTasks()
    {
        for (int i = _activeTasks.Count - 1; i >= 0; i--)
        {
            var task = _activeTasks[i];
            task.UpdateTask();

            if (task.IsCompleted)
            {
                _activeTasks.RemoveAt(i);
                _completedTasks.Add(task);
            }
        }
    }
}
