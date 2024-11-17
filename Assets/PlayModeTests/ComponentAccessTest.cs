using System.Diagnostics;
using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class ComponentAccessTest
{
    private GameObject _testObject;
    private GameObject _testObjectInstance;
    private SimpleComponent _componentAccess;
    private ReferenceHolder _referenceHolder;
    private Stopwatch _stopwatch;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Benchmark.LogTitle("Testing GetComponent and accessing component reference", Color.green );
        _testObject = Resources.Load<GameObject>("SamplePrefabWithReferenceHolder");
        _testObjectInstance = GameObject.Instantiate(_testObject);
        _referenceHolder = _testObjectInstance.GetComponent<ReferenceHolder>();
        _stopwatch = new Stopwatch();
    }

    [UnityTest]
    public IEnumerator Get_Component_Performance()
    {
        yield return null;
        SimpleComponent component;
        _stopwatch.Restart();
        component = _testObjectInstance.GetComponent<SimpleComponent>();
        _stopwatch.Stop();
        Assert.NotNull(component);
        Benchmark.Log("GetComponent took:", Color.white,_stopwatch, 1 );
        
    }
    
    [UnityTest]
    public IEnumerator Access_Component_Via_ReferenceHolder()
    {
        yield return null;
        SimpleComponent component;
        _stopwatch.Restart();
        component = _referenceHolder.simpleComponentReference;
        _stopwatch.Stop();
        Assert.NotNull(component);
        Benchmark.Log("Accessing component via reference holder:", Color.white,_stopwatch, 1 );
    }        
    
    [UnityTest]
    public IEnumerator Access_Cached_Component_Via_ReferenceHolder()
    {
        yield return null;
        SimpleComponent component;
        _stopwatch.Restart();
        component = _referenceHolder.simpleComponentReferenceCachedOnAwake;
        _stopwatch.Stop();
        Assert.NotNull(component);
        Benchmark.Log("Accessing cached component via reference holder:", Color.white,_stopwatch, 1 );
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        if (_testObjectInstance != null)
        {
            GameObject.Destroy(_testObjectInstance);
        }
    }
}
