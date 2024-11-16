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
    public void Test_AddingItems()
    {
        var list = new List<Vector3>();
        _stopwatch.Restart();
        for (int i = 0; i < Iterations; i++)
        {
            list.Add(new Vector3(i, i, i));
        }
        _stopwatch.Stop();
        Benchmark.Log("Adding Vector3 items:", Color.white,_stopwatch, Iterations );
    }
}