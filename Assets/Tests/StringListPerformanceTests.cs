using UnityEngine;
using NUnit.Framework;
using System.Diagnostics;
using System.Collections.Generic;

public class StringListPerformanceTests
{
    private Stopwatch stopwatch;
    private int iterations = 100000;
    private System.Random random = new System.Random();

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        UnityEngine.Debug.Log($"---> Testing List<string> operations - {iterations} iterations");
    }

    [SetUp]
    public void Setup()
    {
        stopwatch = new Stopwatch();
    }

    [Test]
    public void Test_AddingItems()
    {
        var list = new List<string>();
        stopwatch.Start();
        for (int i = 0; i < iterations; i++)
        {
            list.Add(i.ToString());
        }
        stopwatch.Stop();
        UnityEngine.Debug.Log($"Adding {iterations} strings: {stopwatch.ElapsedMilliseconds} ms");
    }
    
    [Test]
    public void Test_RemovingItemsAtRandom()
    {
        var list = new List<string>();
        for (int i = 0; i < iterations; i++)
        {
            list.Add(i.ToString());
        }

        stopwatch.Start();
        while (list.Count > 0)
        {
            int indexToRemove = random.Next(list.Count);  // Generate a random index
            list.RemoveAt(indexToRemove);
        }
        stopwatch.Stop();
        UnityEngine.Debug.Log($"Removing items at random positions: {stopwatch.ElapsedMilliseconds} ms");
    }

    // Additional tests for accessing and removing items can be similarly defined
}