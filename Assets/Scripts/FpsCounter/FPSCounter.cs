using System;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private const float FPSMeasurePeriod = 0.5f; // Half a second
    private int _fpsAccumulator;
    private float _fpsNextPeriod;
    private int _currentFps;
    public Action<int> OnFpsUpdated;

    public int GetCurrentFps()
    {
        return _currentFps;
    }
    
    private void Start()
    {
        _fpsNextPeriod = Time.realtimeSinceStartup + FPSMeasurePeriod;
    }
    
    private void Update()
    {
        _fpsAccumulator++;
        if (Time.realtimeSinceStartup > _fpsNextPeriod)
        {
            _currentFps = (int)(_fpsAccumulator / FPSMeasurePeriod);
            _fpsAccumulator = 0;
            _fpsNextPeriod = Time.realtimeSinceStartup + FPSMeasurePeriod;
            OnFpsUpdated?.Invoke(_currentFps);
        }
    }
}