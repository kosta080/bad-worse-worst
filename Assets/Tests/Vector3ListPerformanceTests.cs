using UnityEngine;
using NUnit.Framework;
using System.Diagnostics;
using System.Collections.Generic;
using Tests;

public class Vector3ListPerformanceTests
{
    private Stopwatch _stopwatch;
    private const int Iterations = 10000;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Benchmark.Log("Testing List<Vector3> operations", Color.yellow );
    }

    [SetUp]
    public void Setup()
    {
        _stopwatch = new Stopwatch();
    }

    [Test]
    public void Test_New_AddingItems()
    {
        var list = new List<Vector3>();
        _stopwatch.Restart();
        for (int i = 0; i < Iterations; i++)
        {
            list.Add(new Vector3(1f,2f,3f));
        }
        _stopwatch.Stop();
        Benchmark.Log("Adding new Vector3 items:", Color.white,_stopwatch, Iterations );
    }
    
    [Test]
    public void Test_Cached_AddingItems()
    {
        var list = new List<Vector3>();
        Vector3 item = new Vector3(1f, 2f, 3f);
        _stopwatch.Restart();
        for (int i = 0; i < Iterations; i++)
        {
            list.Add(item);
        }
        _stopwatch.Stop();
        Benchmark.Log("Adding Cached Vector3 items:", Color.white,_stopwatch, Iterations );
    }
}