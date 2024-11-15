using UnityEngine;
using NUnit.Framework;
using System.Diagnostics;
using System.Collections.Generic;

public class Vector3ListPerformanceTests
{
    private Stopwatch stopwatch;
    private int iterations = 10000;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        UnityEngine.Debug.Log($"---> Testing List<Vector3> operations - {iterations} iterations");
    }

    [SetUp]
    public void Setup()
    {
        stopwatch = new Stopwatch();
    }

    [Test]
    public void Test_AddingItems()
    {
        var list = new List<Vector3>();
        stopwatch.Start();
        for (int i = 0; i < iterations; i++)
        {
            list.Add(new Vector3(i, i, i));
        }
        stopwatch.Stop();
        UnityEngine.Debug.Log($"Adding {iterations} Vector3 items: {stopwatch.ElapsedMilliseconds} ms");
    }

    // Additional tests for accessing and removing items can be similarly defined
}