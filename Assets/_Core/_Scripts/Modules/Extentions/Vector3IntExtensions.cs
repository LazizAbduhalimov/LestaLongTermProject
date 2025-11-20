using UnityEngine;

public static class Vector3IntExtensions
{
    public static Vector3Int AddX(this Vector3Int mapCoords, int value)
    {
        mapCoords.x += value;
        return mapCoords;
    }

    public static Vector3Int AddY(this Vector3Int mapCoords, int value)
    {
        mapCoords.y += value;
        return mapCoords;
    }

    public static Vector3Int AddZ(this Vector3Int mapCoords, int value)
    {
        mapCoords.z += value;
        return mapCoords;
    }
}