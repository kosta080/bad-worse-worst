using UnityEngine;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine.TestTools;

public class FindGameObjectTest : MonoBehaviour
{
    private const int GameObjectCount = 1000;
    private string _searchName = "GameObject_999";

    private Dictionary<string,GameObject> _cachedGameObjects;

    private Stopwatch _stopwatch = new();

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
        _stopwatch.Restart();
        var gameObject = GameObject.Find(_searchName);
        _stopwatch.Stop();
        Assert.NotNull(gameObject);
        Benchmark.Log("Find by Name:", Color.white, _stopwatch, 1);
    }

    [UnityTest]
    public IEnumerator Find_By_Tag_Performance()
    {
        yield return null;
        _stopwatch.Restart();
        var gameObjects = GameObject.FindGameObjectsWithTag("TestObject");
        _stopwatch.Stop();
        Assert.IsNotEmpty(gameObjects);
        Benchmark.Log("Find by Tag:", Color.white, _stopwatch, 1);
    }

    [UnityTest]
    public IEnumerator Find_By_Type_Performance()
    {
        yield return null;
        _stopwatch.Restart();
        var components = FindObjectOfType<SimpleComponent>();
        _stopwatch.Stop();
        Assert.IsNotNull(components);
        Benchmark.Log("Find Object of Type:", Color.white, _stopwatch, 1);
    }
    
    [UnityTest]
    public IEnumerator Find_Multiple_By_Type_Performance()
    {
        yield return null;
        _stopwatch.Restart();
        var components = FindObjectsOfType<SimpleComponent>();
        _stopwatch.Stop();
        Assert.IsNotEmpty(components);
        Benchmark.Log("Find ObjectS of Type:", Color.white, _stopwatch, 1);
    }
    
    [UnityTest]
    public IEnumerator Try_Find_Specific_Name_GameObject_Performance()
    {
        yield return null;
        GameObject res = null; 
        _stopwatch.Restart();
        _cachedGameObjects.TryGetValue(_searchName, out var go);
        res = go;
        _stopwatch.Stop();
        Assert.IsNotNull(res);
        Benchmark.Log("Try Find Specific Name from cached:", Color.white, _stopwatch, 1);
    }
    
    [UnityTest]
    public IEnumerator Find_Specific_Name_GameObject_Performance()
    {
        yield return null;
        GameObject res = null; 
        _stopwatch.Restart();
        res = _cachedGameObjects[_searchName];
        _stopwatch.Stop();
        Assert.IsNotNull(res);
        Benchmark.Log("Find Specific Name from cached:", Color.white, _stopwatch, 1);
    }

    // ------------------------------
    private List<CustomClassForPerformeceTest> _customClassList = new ();
    
    [Test, Order(1)]
    public void Cteating_List_Of_CustomClass()
    {
        StringBuilder stringBuilder = new();
        _stopwatch.Restart();
        for (int i = 0; i < GameObjectCount; i++)
        {
            stringBuilder.Clear();
            stringBuilder.AppendFormat("itemName{0}", i);
            _customClassList.Add(new CustomClassForPerformeceTest(stringBuilder.ToString()));
        }
        _stopwatch.Stop();
        Assert.IsNotEmpty(_customClassList);
        Benchmark.Log("Cteating List Of CustomClass:", Color.white , _stopwatch,GameObjectCount );
    }

    [OneTimeTearDown]
    public void CleanUp()
    {
        foreach (var go in FindObjectsOfType<GameObject>())
        {
            Destroy(go);
        }
    }
}


public class CustomClassForPerformeceTest
{
    public string Name;
    public float Health = 100f;
    public float MaxHealth = 100f;
    public Vector3 LastPosition;

    public CustomClassForPerformeceTest(string name="")
    {
        Name = name;
    }
}
