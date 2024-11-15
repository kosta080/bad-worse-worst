using System.Diagnostics;
using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class ComponentAccessTest
{
    private GameObject testObject;
    private GameObject testObjectInstance;
    private SimpleComponent componentAccess;
    private ReferenceHalder referenceHalder;
    private Stopwatch stopwatch;

    [OneTimeSetUp]
    public void Setup()
    {
        UnityEngine.Debug.Log($"<color=orange> Testing GetComponent and accessing component reference </color>");
        testObject = Resources.Load<GameObject>("SamplePrefabWithReferenceHolder");
        testObjectInstance = GameObject.Instantiate(testObject);
        referenceHalder = testObjectInstance.GetComponent<ReferenceHalder>();
    }

    [UnityTest]
    public IEnumerator TestGetComponentPerformance()
    {
        yield return null;
        SimpleComponent component;
        stopwatch = Stopwatch.StartNew();
        component = testObjectInstance.GetComponent<SimpleComponent>();
        stopwatch.Stop();
        Assert.NotNull(component);
        UnityEngine.Debug.Log($"GetComponent took: {stopwatch.Elapsed.TotalMilliseconds} ms");
        
    }
    
    [UnityTest]
    public IEnumerator TestAccessComponentViaReferenceHolder()
    {
        yield return null;
        SimpleComponent component;
        stopwatch = Stopwatch.StartNew();
        component = referenceHalder.simpleComponentReference;
        stopwatch.Stop();
        Assert.NotNull(component);
        UnityEngine.Debug.Log($"Accessing component via reference holder took: {stopwatch.Elapsed.TotalMilliseconds} ms");
    }        
    
    [UnityTest]
    public IEnumerator TestAccessCachedComponentViaReferenceHolder()
    {
        yield return null;
        SimpleComponent component;
        stopwatch = Stopwatch.StartNew();
        component = referenceHalder.simpleComponentReferenceCachedOnAwake;
        stopwatch.Stop();
        Assert.NotNull(component);
        UnityEngine.Debug.Log($"Accessing cached component via reference holder took: {stopwatch.Elapsed.TotalMilliseconds} ms");
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        if (testObjectInstance != null)
        {
            GameObject.Destroy(testObjectInstance);
        }
    }
}
