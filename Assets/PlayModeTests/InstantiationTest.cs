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
    private readonly int iterations = 100;
    
    
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        UnityEngine.Debug.Log($"---> Testing Instantiate Prefab - {iterations} times");
        _prefab = Resources.Load<GameObject>("SamplePrefab");
        _stopwatch = new Stopwatch();
    }

    [UnityTest]
    public IEnumerator InstantiateAndDestroy_GameObjects()
    {
        List<GameObject> instantiatedObjects = new List<GameObject>();
        _stopwatch.Start();
        for (int i = 0; i < iterations; i++)
        {
            var go = Instantiate(_prefab, new Vector3(i * 0.1f, 0, 0), Quaternion.identity);
            instantiatedObjects.Add(go);
            yield return null;
        }
        _stopwatch.Stop();
        UnityEngine.Debug.Log($"Time to instantiate {iterations} GameObjects: {_stopwatch.Elapsed.TotalMilliseconds} ms");

        _stopwatch.Reset();
        
        _stopwatch.Start();
        foreach (var obj in instantiatedObjects)
        {
            Destroy(obj);
        }
        _stopwatch.Stop();
        UnityEngine.Debug.Log($"Time to destroy {iterations} GameObjects: {_stopwatch.Elapsed.TotalMilliseconds} ms");
        
        yield return null;
    }

    [UnityTest]
    public IEnumerator InstantiateAndDestroy_GameObjectsWithNullCheck()
    {
        List<GameObject> gameObjects = new List<GameObject>();
        _stopwatch.Start();
        for (int i = 0; i < iterations; i++)
        {
            var go = Instantiate(_prefab, new Vector3(i * 0.1f, 0, 0), Quaternion.identity);
            gameObjects.Add(go);
        }
        _stopwatch.Stop();

        UnityEngine.Debug.Log($"Time to instantiate and store in list {iterations} GameObjects: {_stopwatch.Elapsed.TotalMilliseconds} ms");
        
        _stopwatch.Reset();
        
        _stopwatch.Start();
        foreach (var obj in gameObjects)
        {
            if (obj == null) continue;
            Destroy(obj);
        }
        _stopwatch.Stop();
        UnityEngine.Debug.Log($"Time to null check and destroy {iterations} GameObjects: {_stopwatch.Elapsed.TotalMilliseconds} ms");
        
        yield return null;
    }
}
