using System;
using UnityEngine;

public static class Vector3Extensions
{
    public static Vector3 WithX(this Vector3 value, float x)
    {
        value.x = x;
        return value;
    }

    public static Vector3 WithY(this Vector3 value, float y)
    {
        value.y = y;
        return value;
    }

    public static Vector3 WithZ(this Vector3 value, float z)
    {
        value.z = z;
        return value;
    }

    public static Vector3 AddX(this Vector3 value, float x)
    {
        value.x += x;
        return value;
    }

    public static Vector3 AddY(this Vector3 value, float y)
    {
        value.y += y;
        return value;
    }

    public static Vector3 AddZ(this Vector3 value, float z)
    {
        value.z += z;
        return value;
    }
    
    public static Vector3 LerpArray(Vector3[] array, float t)
    {
        if (t < 0) throw new ArgumentException("T can't be negative");
        if (array == null) throw new ArgumentNullException(nameof(array), "Array can't be null");
        if (array.Length < 2) throw new ArgumentException("Can't interpolate between less than 2 elements");
    
        if (t > 1) t -= Mathf.Floor(t);

        var fragment = 1f / (array.Length - 1);
        var index = Mathf.FloorToInt(t / fragment);
    
        if (index >= array.Length - 1) 
            index = array.Length - 2;

        var lerpT = (t - index * fragment) / fragment;
        var lerp = Vector3.Lerp(array[index], array[index + 1], lerpT);
        return lerp;
    }

    public static Vector3 ToVector3(this Vector3Int vector3Int)
    {
        return vector3Int;
    }
}