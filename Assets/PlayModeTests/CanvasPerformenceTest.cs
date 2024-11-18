using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CanvasPerformanceTest
{
    private GameObject _canvasGo;
    private const int SpriteCount = 1000;
    private const int FramesToMeasure = 10000; // 1000 is ~3 seconds
    
    private List<GameObject> _sprites = new List<GameObject>();
    
    private FPSCounter _fpsCounter;

    private void SetFpsCounter()
    {
        var fpsCounter = new GameObject("FPSCounter");
        _fpsCounter = fpsCounter.AddComponent<FPSCounter>();
    }
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        SetFpsCounter();
        _canvasGo = new GameObject("Canvas");
        _canvasGo.AddComponent<Canvas>();
        
        for (int i = 0; i < SpriteCount; i++)
        {
            var spriteGo = new GameObject("Sprite " + i);
            var sprite = spriteGo.AddComponent<SpriteRenderer>();
            sprite.sprite = Resources.Load<Sprite>("Sprites/icon_flag_skull");
            spriteGo.transform.SetParent(_canvasGo.transform, false);
            //spriteGo.transform.localScale = Vector3.one;
            spriteGo.transform.position = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50));
            _sprites.Add(spriteGo);
        }
    }

    [UnityTest, Order(1)]
    public IEnumerator Create_Canvas_With_Sprites()
    {
        yield return null;
        
        yield return new WaitForSeconds(3.0f);
        int FPS = _fpsCounter.GetCurrentFps();
        Debug.Log($"{SpriteCount} GameObjects with Update loop: {FPS} FPS (counter component)");
        
        
        Assert.IsNotNull(_canvasGo, "Canvas instance should not be null.");
        Assert.AreEqual(SpriteCount, _sprites.Count, "Should have the correct number of sprites.");

        // Optionally perform cleanup
        
    }

    [UnityTest, Order(2)]
    public IEnumerator Rotating_Sprites()
    {

        float timePasser = 0.0f;
        while (timePasser < 3.0f)
        {
            yield return null;
            timePasser += Time.deltaTime;
            foreach (var sprite in _sprites)
            {
                sprite.transform.Rotate(new Vector3(30f,30f,130f) * Time.deltaTime);
            }
        }
        int FPS = _fpsCounter.GetCurrentFps();
        Debug.Log($"{SpriteCount} GameObjects with Update loop: {FPS} FPS (counter component)");
    }
    
/*
    [TearDown]
    private void TearDown()
    {
        foreach (var sprite in _sprites)
        {
            Object.Destroy(sprite);
        }
    }*/
}