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
    private Rigidbody _cashedMeshRendererComponent;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        UnityEngine.Debug.Log("<color=yellow> GetComponent VS cached component</color>");
        testGameObject = new GameObject("TestGameObject");
        _cashedMeshRendererComponent = testGameObject.AddComponent<Rigidbody>();
    }

    [UnityTest]
    public IEnumerator Test_GetComponent_and_store_in_list()
    {
        _stopwatch = new Stopwatch();
        List<Rigidbody> rigidbodyComponents = new List<Rigidbody>();
        Rigidbody component;
        _stopwatch.Start();
        component = testGameObject.GetComponent<Rigidbody>();
        rigidbodyComponents.Add(component);
        _stopwatch.Stop();
        Assert.NotNull(rigidbodyComponents);
        UnityEngine.Debug.Log($"Time to call GetComponent and store it in a list {_stopwatch.Elapsed.TotalMilliseconds} ms");
        yield return null;
    }
    
    [UnityTest]
    public IEnumerator Test_store_cached_component()
    {
        _stopwatch = new Stopwatch();
        List<Rigidbody> meshRendererComponents = new List<Rigidbody>();
        Rigidbody component;
        _stopwatch.Start();
        component = _cashedMeshRendererComponent;
        meshRendererComponents.Add(component);
        _stopwatch.Stop();
        Assert.NotNull(meshRendererComponents);
        UnityEngine.Debug.Log($"Time to store cached component in a list {_stopwatch.Elapsed.TotalMilliseconds} ms");

        yield return null;
    }

    [TearDown]
    public void Cleanup()
    {
        Destroy(testGameObject);
    }
}