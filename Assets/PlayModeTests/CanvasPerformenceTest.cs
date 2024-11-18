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
    }

    [UnityTest]
    public IEnumerator Create_Canvas_With_Sprites()
    {
        yield return null;
        
        List<GameObject> sprites = new List<GameObject>();
        for (int i = 0; i < SpriteCount; i++)
        {
            var spriteGo = new GameObject("Sprite " + i);
            var sprite = spriteGo.AddComponent<SpriteRenderer>();
            sprite.sprite = Resources.Load<Sprite>("Sprites/icon_flag_skull");
            spriteGo.transform.SetParent(_canvasGo.transform, false);
            //spriteGo.transform.localScale = Vector3.one;
            spriteGo.transform.position = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50));
            sprites.Add(spriteGo);
        }
        
        yield return new WaitForSeconds(3.0f);
        int FPS = _fpsCounter.GetCurrentFps();
        Debug.Log($"{SpriteCount} GameObjects with Update loop: {FPS} FPS (counter component)");
        
        
        Assert.IsNotNull(_canvasGo, "Canvas instance should not be null.");
        Assert.AreEqual(SpriteCount, sprites.Count, "Should have the correct number of sprites.");

        // Optionally perform cleanup
        foreach (var sprite in sprites)
        {
            Object.Destroy(sprite);
        }
    }
}