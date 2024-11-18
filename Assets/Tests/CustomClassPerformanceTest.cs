using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;
using System.Linq;

public class CustomClassPerformanceTest
{
    private List<CustomClassForPerformeceTest> _customClassList = new();
    private Stopwatch _stopwatch;

    private const int Iterations = 1000;
    private string itemNamePrefix = "itemName";
    private string searchName = "itemName999";

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Benchmark.LogTitle("Custom Class Performance Test", Color.yellow);
        _stopwatch = new Stopwatch();
    }

    [Test, Order(1)]
    public void Cteating_List_Of_CustomClass()
    {
       _stopwatch.Restart();
        for (int i = 0; i < Iterations; i++)
        {
            _customClassList.Add(new CustomClassForPerformeceTest($"{itemNamePrefix}{i}".ToString()));
        }

        _stopwatch.Stop();
        Assert.IsNotEmpty(_customClassList);
        Benchmark.Log($"Creating List Of {Iterations} CustomClass:", Color.white, _stopwatch, 1);
    }

    [Test, Order(2)]
    public void Reading_CustomClass_property()
    {
        Assert.IsNotEmpty(_customClassList);
        string str = "";
        _stopwatch.Restart();
        for (int i = 0; i < Iterations; i++)
        {
            str = _customClassList[i].Name;
        }

        _stopwatch.Stop();
        Assert.True(str != "");
        Benchmark.Log($"Reading property from {Iterations} items:", Color.white, _stopwatch, 1);
    }

    [Test, Order(3)]
    public void Searching_CustomClass_property()
    {
        Assert.IsNotEmpty(_customClassList);
        CustomClassForPerformeceTest res = new CustomClassForPerformeceTest();
        _stopwatch.Restart();
        for (int i = 0; i < Iterations; i++)
        {
            if (_customClassList[i].Name == searchName)
            {
                res = _customClassList[i];
            }
        }

        _stopwatch.Stop();
        Assert.True(res.Name == searchName);
        Benchmark.Log($"For loop search on {Iterations} items:", Color.white, _stopwatch, 1);
    }
    
    /*
     * the above failed with output:
     * Searching_CustomClass_property (0.000s)
---
Expected: True
  But was:  False
---
at CustomClassPerformanceTest.Searching_CustomClass_property () [0x00075] in E:\unity projects\bad worse worst\Assets\Tests\CustomClassPerformanceTest.cs:69
     */

    [Test, Order(4)]
    public void Searching_CustomClass_LINQ_property()
    {
        Assert.IsNotEmpty(_customClassList, "111");
        CustomClassForPerformeceTest res = new CustomClassForPerformeceTest();
        _stopwatch.Restart();
        res = _customClassList.Where(c => c.Name == searchName).First();
        _stopwatch.Stop();
        Assert.True(res.Name == searchName,"222");
        Benchmark.Log($"Linq Where search on {Iterations} items:", Color.white, _stopwatch, 1);
    }
    /*
     *atha above failed with this output :
     * Searching_CustomClass_LINQ_property (0.000s)
---
System.InvalidOperationException : Sequence contains no elements
---
at System.Linq.Enumerable.First[TSource] (System.Collections.Generic.IEnumerable`1[T] source) [0x00010] in <a314714511a14f84b853c03efd8682b8>:0 
  at CustomClassPerformanceTest.Searching_CustomClass_LINQ_property () [0x00024] in E:\unity projects\bad worse worst\Assets\Tests\CustomClassPerformanceTest.cs:79 
  at (wrapper managed-to-native) System.Reflection.RuntimeMethodInfo.InternalInvoke(System.Reflection.RuntimeMethodInfo,object,object[],System.Exception&)
  at System.Reflection.RuntimeMethodInfo.Invoke (System.Object obj, System.Reflection.BindingFlags invokeAttr, System.Reflection.Binder binder, System.Object[] parameters, System.Globalization.CultureInfo culture) [0x0006a] in <8ce0bd04a7a04b4b9395538239d3fdd8>:0
     *
     */
}

public class CustomClassForPerformeceTest
{
    public string Name;
    public float Health = 100f;
    public float MaxHealth = 100f;
    public Vector3 LastPosition;

    public CustomClassForPerformeceTest(string name = "")
    {
        Name = name;
    }
}