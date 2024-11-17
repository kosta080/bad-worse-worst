using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GetComponentPerformanceTest : MonoBehaviour
{
    private GameObject _testGameObject;
    private Stopwatch _stopwatch;
    private Rigidbody _cachedMeshRendererComponent;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Benchmark.LogTitle("GetComponent VS Cached component", Color.green );
        _testGameObject = new GameObject("TestGameObject");
        _cachedMeshRendererComponent = _testGameObject.AddComponent<Rigidbody>();
    }

    [UnityTest]
    public IEnumerator GetComponent_And_Store_In_List()
    {
        _stopwatch = new Stopwatch();
        List<Rigidbody> rigidbodyComponents = new List<Rigidbody>();
        Rigidbody component;
        _stopwatch.Restart();
        component = _testGameObject.GetComponent<Rigidbody>();
        rigidbodyComponents.Add(component);
        _stopwatch.Stop();
        Assert.NotNull(rigidbodyComponents);
        Benchmark.Log("Call GetComponent and store it in a list", Color.white,_stopwatch, 1 );
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Store_Sached_Somponent()
    {
        _stopwatch = new Stopwatch();
        List<Rigidbody> meshRendererComponents = new List<Rigidbody>();
        Rigidbody component;
        _stopwatch.Restart();
        component = _cachedMeshRendererComponent;
        meshRendererComponents.Add(component);
        _stopwatch.Stop();
        Assert.NotNull(meshRendererComponents);
        Benchmark.Log($"Store cached component in a list", Color.white,_stopwatch, 1 );
        yield return null;
    }

    [TearDown]
    public void Cleanup()
    {
        Destroy(_testGameObject);
    }
}