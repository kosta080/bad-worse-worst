using UnityEngine;
using NUnit.Framework;
using System.Diagnostics;
using System.Collections.Generic;

public class StringListPerformanceTests
{
    private Stopwatch _stopwatch;
    private const int Iterations = 100000;
    private readonly System.Random random = new System.Random();

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Benchmark.LogTitle("Testing List<string> operations", Color.yellow );
    }

    [SetUp]
    public void Setup()
    {
        _stopwatch = new Stopwatch();
    }

    [Test]
    public void Test_AddingItems()
    {
        var list = new List<string>();
        _stopwatch.Restart();
        for (int i = 0; i < Iterations; i++)
        {
            list.Add(i.ToString());
        }
        _stopwatch.Stop();
        Benchmark.Log("Adding strings:", Color.white,_stopwatch, Iterations );
    }
    
    [Test]
    public void Test_RemovingItemsAtRandom()
    {
        var list = new List<string>();
        for (int i = 0; i < Iterations; i++)
        {
            list.Add(i.ToString());
        }

        _stopwatch.Restart();
        while (list.Count > 0)
        {
            int indexToRemove = random.Next(list.Count);  // Generate a random index
            list.RemoveAt(indexToRemove);
        }
        _stopwatch.Stop();
        
        Benchmark.Log("Removing items at random positions:", Color.white,_stopwatch, Iterations );
    }

    // Additional tests for accessing and removing items can be similarly defined
}