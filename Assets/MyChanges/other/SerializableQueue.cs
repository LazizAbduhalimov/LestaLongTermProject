using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableQueue<T>
{
    [SerializeField]
    private List<T> items = new List<T>();

    public int Count => items.Count;

    public void Enqueue(T item)
    {
        items.Add(item);
    }

    public T Dequeue()
    {
        if (items.Count == 0) return default;
        T value = items[0];
        items.RemoveAt(0);
        return value;
    }

    public T Peek()
    {
        return items.Count > 0 ? items[0] : default;
    }

    public List<T> GetList() => items;
}