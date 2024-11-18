using UnityEngine;
using NUnit.Framework;
using System.Diagnostics;
using System.Collections.Generic;

public class StringListPerformanceTests
{
    private Stopwatch _stopwatch;
    private const int Iterations = 1000;

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
    public void Adding_Items_To_List_Of_String()
    {
        var list = new List<string>();
        _stopwatch.Restart();
        for (int i = 0; i < Iterations; i++)
        {
            list.Add(i.ToString());
        }
        _stopwatch.Stop();
        Benchmark.Log($"Adding {Iterations} items:", Color.white,_stopwatch, 1 );
    }
    
    [Test]
    public void Removing_Items_At_Random_Position()
    {
        System.Random random = new System.Random();
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
        
        Benchmark.Log($"Removing {Iterations} items at random positions:", Color.white,_stopwatch, 1 );
    }

    // Additional tests for accessing and removing items can be similarly defined
}