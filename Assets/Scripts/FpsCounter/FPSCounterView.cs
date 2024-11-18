using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(Text))]
[RequireComponent(typeof(FPSCounter))]
public class FPSCounterView : MonoBehaviour
{
    private const string DisplayFormat = "{0} FPS";
    private int _fpsAccumulator;
    private int _currentFps;
    private Canvas _canvas;
    private Text _text;
    private FPSCounter _fpsCounter;
    
    private void Start()
    {
        _canvas = GetComponent<Canvas>();
        _text = GetComponent<Text>();
        _fpsCounter = GetComponent<FPSCounter>();
        SetupCanvas();
        SetTextup();

        _fpsCounter.OnFpsUpdated += OnFpsUpdated;
    }

    private void OnDestroy()
    {
        _fpsCounter.OnFpsUpdated -= OnFpsUpdated;
    }

    private void OnFpsUpdated(int fps)
    {
        _text.text = string.Format(DisplayFormat, fps);
    }


    private void SetupCanvas()
    {
        _canvas.renderMode = RenderMode.ScreenSpaceCamera;
        _canvas.worldCamera = Camera.main;
    }
    
    private void SetTextup()
    {
        RectTransform rect = _text.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0, 1);
        rect.anchorMax = new Vector2(0, 1);
        rect.pivot = new Vector2(0, 1);
        rect.anchoredPosition = new Vector3(10, -10, 0);
        
        _text.color = Color.red;
    }
}