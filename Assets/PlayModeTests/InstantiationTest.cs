using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class InstantiationTest : MonoBehaviour
{
    private GameObject _prefab;
    private Stopwatch _stopwatch;
    private const int Iterations = 100;
    
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Benchmark.LogTitle("Testing Instantiate Prefab", Color.green );
        _prefab = Resources.Load<GameObject>("SamplePrefab");
        _stopwatch = new Stopwatch();
    }

    [UnityTest]
    public IEnumerator Instantiate_And_Destroy_GameObjects()
    {
        List<GameObject> gameObjects = new List<GameObject>();
        GameObject go;
        _stopwatch.Restart();
        for (int i = 0; i < Iterations; i++)
        {
            go = Instantiate(_prefab, Vector3.zero, Quaternion.identity);
            gameObjects.Add(go);
        }
        _stopwatch.Stop();
        Benchmark.Log("Time to instantiate GameObject:", Color.white,_stopwatch, Iterations );
        
        _stopwatch.Reset();
        yield return null;
        _stopwatch.Restart();
        foreach (var obj in gameObjects)
        {
            Destroy(obj);
        }
        _stopwatch.Stop();
        Benchmark.Log("Time to destroy GameObject:",Color.white,_stopwatch, Iterations );
    
        yield return null;
    }

    [UnityTest]
    public IEnumerator Instantiate_And_Destroy_GameObjects_With_NullChecks()
    {
        List<GameObject> gameObjects = new List<GameObject>();
        GameObject go;
        _stopwatch.Restart();
        for (int i = 0; i < Iterations; i++)
        {
            go = Instantiate(_prefab, Vector3.zero, Quaternion.identity);
            if (go == null) Assert.Fail();
            gameObjects.Add(go);
        }
        _stopwatch.Stop();
        Benchmark.Log("Time to instantiate and and null check GameObject:",Color.white,_stopwatch, Iterations );
        
        _stopwatch.Reset();
        
        _stopwatch.Restart();
        for (int i = 0; i < Iterations; i++)
        {
            if (gameObjects[i] == null) continue;
            Destroy(gameObjects[i]);
        }
        _stopwatch.Stop();
        Benchmark.Log("Time to null check and destroy GameObject:",Color.white,_stopwatch, Iterations );
       
        yield return null;
    }
}
