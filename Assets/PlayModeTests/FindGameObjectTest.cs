using UnityEngine;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.TestTools;

public class FindGameObjectTest : MonoBehaviour
{
    private const int GameObjectCount = 1000;

    private Dictionary<string,GameObject> _cachedGameObjects;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Benchmark.LogTitle("Find GameObject Test", Color.green);
        _cachedGameObjects = new Dictionary<string, GameObject>();
        for (int i = 0; i < GameObjectCount; i++)
        {
            var goName = $"GameObject_{i}";
            var go = new GameObject(goName)
            {
                tag = "TestObject"
            };
            go.AddComponent<SimpleComponent>();
            _cachedGameObjects.Add(goName, go);
        }
    }

    [UnityTest]
    public IEnumerator Find_By_Name_Performance()
    {
        yield return null;
        var stopwatch = Stopwatch.StartNew();
        var gameObject = GameObject.Find("GameObject_999");
        stopwatch.Stop();
        Assert.NotNull(gameObject);
        Benchmark.Log("Find by Name:", Color.white, stopwatch, 1);
    }

    [UnityTest]
    public IEnumerator Find_By_Tag_Performance()
    {
        yield return null;
        var stopwatch = Stopwatch.StartNew();
        var gameObjects = GameObject.FindGameObjectsWithTag("TestObject");
        stopwatch.Stop();
        Assert.IsNotEmpty(gameObjects);
        Benchmark.Log("Find by Tag:", Color.white, stopwatch, 1);
    }

    [UnityTest]
    public IEnumerator Find_By_Type_Performance()
    {
        yield return null;
        var stopwatch = Stopwatch.StartNew();
        var components = FindObjectOfType<SimpleComponent>();
        stopwatch.Stop();
        Assert.IsNotNull(components);
        Benchmark.Log("Find Object of Type:", Color.white, stopwatch, 1);
    }
    
    [UnityTest]
    public IEnumerator Find_Multiple_By_Type_Performance()
    {
        yield return null;
        var stopwatch = Stopwatch.StartNew();
        var components = FindObjectsOfType<SimpleComponent>();
        stopwatch.Stop();
        Assert.IsNotEmpty(components);
        Benchmark.Log("Find ObjectS of Type:", Color.white, stopwatch, 1);
    }
    
    [UnityTest]
    public IEnumerator Find_Specific_Name_GameObject_Performance()
    {
        yield return null;
        var stopwatch = Stopwatch.StartNew();
        _cachedGameObjects.TryGetValue("GameObject_999", out var go);
        stopwatch.Stop();
        Assert.IsNotNull(go);
        Benchmark.Log("Find Specific Name from cached:", Color.white, stopwatch, 1);
    }

    // Add more tests as needed for other finding methods

    [OneTimeTearDown]
    public void CleanUp()
    {
        foreach (var go in FindObjectsOfType<GameObject>())
        {
            Destroy(go);
        }
    }
}

