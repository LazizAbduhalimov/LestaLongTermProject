using System;
using UnityEngine;


public class ApplicationSettings : MonoBehaviour
{
    public int TargetFrameRate = 120;

    private void Awake()
    {
        Application.targetFrameRate = TargetFrameRate;
    }
}
