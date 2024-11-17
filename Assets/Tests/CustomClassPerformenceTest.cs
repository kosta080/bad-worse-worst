using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class CustomClassPerformenceTest
{
    private List<CustomClassForPerformeceTest> _customClassList = new ();
    private Stopwatch _stopwatch;
    
    private const int Iterations = 10000;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _stopwatch = new Stopwatch();
    }

    [Test, Order(1)]
    public void Cteating_List_Of_CustomClass()
    {
        StringBuilder stringBuilder = new();
        _stopwatch.Restart();
        for (int i = 0; i < Iterations; i++)
        {
            stringBuilder.Clear();
            stringBuilder.AppendFormat("itemName{0} ", i);
            _customClassList.Add(new CustomClassForPerformeceTest(stringBuilder.ToString()));
        }
        _stopwatch.Stop();
        Assert.IsNotEmpty(_customClassList);
        Benchmark.Log("Cteating List Of CustomClass:", Color.white , _stopwatch,Iterations );
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
        Benchmark.Log("Cteating List Of CustomClass:", Color.white , _stopwatch,Iterations );
    }
}

public class CustomClassForPerformeceTest
{
    public string Name;
    public float Health = 100f;
    public float MaxHealth = 100f;
    public Vector3 LastPosition;

    public CustomClassForPerformeceTest(string name)
    {
        Name = name;
    }
}
