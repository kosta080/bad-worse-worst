using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using Tests;
using UnityEngine;
using UnityEngine.TestTools;

public class GetComponentPerformanceTest : MonoBehaviour
{
    private GameObject testGameObject;
    private Stopwatch _stopwatch;
    private Rigidbody _cashedMeshRendererComponent;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Benchmark.Log("GetComponent VS Cached component", Color.green );
        testGameObject = new GameObject("TestGameObject");
        _cashedMeshRendererComponent = testGameObject.AddComponent<Rigidbody>();
    }

    [UnityTest]
    public IEnumerator Test_GetComponent_and_store_in_list()
    {
        _stopwatch = new Stopwatch();
        List<Rigidbody> rigidbodyComponents = new List<Rigidbody>();
        Rigidbody component;
        _stopwatch.Restart();
        component = testGameObject.GetComponent<Rigidbody>();
        rigidbodyComponents.Add(component);
        _stopwatch.Stop();
        Assert.NotNull(rigidbodyComponents);
        Benchmark.Log("Call GetComponent and store it in a list", Color.white,_stopwatch, 1 );
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Test_store_cached_component()
    {
        _stopwatch = new Stopwatch();
        List<Rigidbody> meshRendererComponents = new List<Rigidbody>();
        Rigidbody component;
        _stopwatch.Restart();
        component = _cashedMeshRendererComponent;
        meshRendererComponents.Add(component);
        _stopwatch.Stop();
        Assert.NotNull(meshRendererComponents);
        Benchmark.Log($"Store cached component in a list", Color.white,_stopwatch, 1 );
        yield return null;
    }

    [TearDown]
    public void Cleanup()
    {
        Destroy(testGameObject);
    }
}