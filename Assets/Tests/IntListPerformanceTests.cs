﻿using NUnit.Framework;
using System.Diagnostics;
using System.Collections.Generic;
using Tests;
using UnityEngine;


public class IntListPerformanceTests
{
    private Stopwatch _stopwatch;
    private readonly int iterations = 100000;
    private System.Random random = new System.Random();

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Benchmark.Log("Testing different List<int> operations", Color.yellow );
    }

    [SetUp]
    public void Setup()
    {
        _stopwatch = new Stopwatch();
    }

    [Test]
    public void Test_AddingItems()
    {
        _stopwatch.Restart();
        var list = new List<int>();
        for (int i = 0; i < iterations; i++)
        {
            list.Add(i);
        }
        _stopwatch.Stop();
        //UnityEngine.Debug.Log($"Adding {iterations} items: {_stopwatch.Elapsed.TotalMilliseconds} ms");
        Benchmark.Log("Adding items:", Color.white,_stopwatch, iterations );
    }

    [Test]
    public void Test_AccessingItems()
    {
        var list = new List<int>();
        for (int i = 0; i < iterations; i++)
        {
            list.Add(i);
        }

        _stopwatch.Restart();
        for (int i = 0; i < iterations; i++)
        {
            var item = list[i];
        }
        _stopwatch.Stop();
        //UnityEngine.Debug.Log($"Accessing {iterations} items: {_stopwatch.Elapsed.TotalMilliseconds} ms");
        Benchmark.Log("Accessing items:", Color.white,_stopwatch, iterations );
    }

    [Test]
    public void Test_RemovingItems()
    {
        var list = new List<int>();
        for (int i = 0; i < iterations; i++)
        {
            list.Add(i);
        }

        _stopwatch.Restart();
        for (int i = iterations - 1; i >= 0; i--)
        {
            list.RemoveAt(i);
        }
        _stopwatch.Stop();
        //UnityEngine.Debug.Log($"Removing {iterations} items: {_stopwatch.Elapsed.TotalMilliseconds} ms");
        Benchmark.Log("Removing items:", Color.white, _stopwatch, iterations );
    }

    
    [Test]
    public void Test_RemovingItemsAtRandom()
    {
        var list = new List<int>();
        for (int i = 0; i < iterations; i++)
        {
            list.Add(i);
        }

        _stopwatch.Restart();
        while (list.Count > 0)
        {
            int indexToRemove = random.Next(list.Count);  // Generate a random index
            list.RemoveAt(indexToRemove);
        }
        _stopwatch.Stop();
        //Debug.Log($"Removing items at random positions: {_stopwatch.Elapsed.TotalMilliseconds} ms");
        Benchmark.Log("Removing items at random positions:", Color.white, _stopwatch, iterations );
    }

    [Test]
    public void Test_InsertingItemsAtStart()
    {
        var list = new List<int>();
        _stopwatch.Restart();
        for (int i = 0; i < iterations; i++)
        {
            list.Insert(0, i); // Inserting at the start of the list
        }
        _stopwatch.Stop();
        //UnityEngine.Debug.Log($"Inserting at start {iterations} times: {_stopwatch.Elapsed.TotalMilliseconds} ms");
        Benchmark.Log("Inserting at start:", Color.white, _stopwatch, iterations );
    }

    [Test]
    public void Test_InsertingItemsAtMiddle()
    {
        var list = new List<int>();
        for (int i = 0; i < iterations; i++)
        {
            list.Add(i);
        }

        _stopwatch.Restart();
        for (int i = 0; i < iterations; i++)
        {
            list.Insert(list.Count / 2, i); // Inserting in the middle
        }
        _stopwatch.Stop();
        //UnityEngine.Debug.Log($"Inserting in the middle {iterations} times: {_stopwatch.Elapsed.TotalMilliseconds} ms");
        Benchmark.Log("Inserting in the middle:", Color.white, _stopwatch, iterations );
    }
}
