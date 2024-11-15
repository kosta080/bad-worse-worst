using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GetComponentPerformanceTest : MonoBehaviour
{
    private GameObject testGameObject;
    private Stopwatch _stopwatch;
    private int iterations = 100;
    private MeshRenderer _cashedMeshRendererComponent;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        UnityEngine.Debug.Log("---> Testing GetComponent performance");
        testGameObject = new GameObject("TestGameObject");
        testGameObject.AddComponent<Rigidbody>();
        _cashedMeshRendererComponent = testGameObject.AddComponent<MeshRenderer>();
        testGameObject.AddComponent<BoxCollider>();
    }

    [UnityTest]
    public IEnumerator Test_GetComponent_and_store_in_list()
    {
        _stopwatch = new Stopwatch();
        List<Rigidbody> rigidbodyComponents = new List<Rigidbody>();
        _stopwatch.Start();
        for (int i = 0; i < iterations; i++)
        {
            var component = testGameObject.GetComponent<Rigidbody>();
            rigidbodyComponents.Add(component);
        }
        _stopwatch.Stop();
        Assert.NotNull(rigidbodyComponents);
        UnityEngine.Debug.Log($"Time to call GetComponent<Rigidbody>() {iterations} times: {_stopwatch.Elapsed.TotalMilliseconds} ms");
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Test_store_cached_component()
    {
        _stopwatch = new Stopwatch();
        List<MeshRenderer> meshRendererComponents = new List<MeshRenderer>();
        _stopwatch.Start();
        for (int i = 0; i < iterations; i++)
        {
            var component = _cashedMeshRendererComponent;
            meshRendererComponents.Add(component);
        }
        _stopwatch.Stop();
        Assert.NotNull(meshRendererComponents);
        UnityEngine.Debug.Log($"Time to store cached component to a list {iterations} times: {_stopwatch.Elapsed.TotalMilliseconds} ms");

        yield return null;
    }

    [TearDown]
    public void Cleanup()
    {
        Destroy(testGameObject);
    }
}