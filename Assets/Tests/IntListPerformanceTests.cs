using NUnit.Framework;
using System.Diagnostics;
using System.Collections.Generic;


public class IntListPerformanceTests
{
    private Stopwatch _stopwatch;
    private readonly int iterations = 100000;
    private System.Random random = new System.Random();

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        UnityEngine.Debug.Log($"---> Testing different List<int> operations - {iterations} iterations");
    }

    [SetUp]
    public void Setup()
    {
        _stopwatch = new Stopwatch();
    }

    [Test]
    public void Test_AddingItems()
    {
        _stopwatch.Start();
        var list = new List<int>();
        for (int i = 0; i < iterations; i++)
        {
            list.Add(i);
        }
        _stopwatch.Stop();
        UnityEngine.Debug.Log($"Adding {iterations} items: {_stopwatch.Elapsed.TotalMilliseconds} ms");
    }

    [Test]
    public void Test_AccessingItems()
    {
        var list = new List<int>();
        for (int i = 0; i < iterations; i++)
        {
            list.Add(i);
        }

        _stopwatch.Start();
        for (int i = 0; i < iterations; i++)
        {
            var item = list[i];
        }
        _stopwatch.Stop();
        UnityEngine.Debug.Log($"Accessing {iterations} items: {_stopwatch.Elapsed.TotalMilliseconds} ms");
    }

    [Test]
    public void Test_RemovingItems()
    {
        var list = new List<int>();
        for (int i = 0; i < iterations; i++)
        {
            list.Add(i);
        }

        _stopwatch.Start();
        for (int i = iterations - 1; i >= 0; i--)
        {
            list.RemoveAt(i);
        }
        _stopwatch.Stop();
        UnityEngine.Debug.Log($"Removing {iterations} items: {_stopwatch.Elapsed.TotalMilliseconds} ms");
    }

    
    [Test]
    public void Test_RemovingItemsAtRandom()
    {
        var list = new List<int>();
        for (int i = 0; i < iterations; i++)
        {
            list.Add(i);
        }

        _stopwatch.Start();
        while (list.Count > 0)
        {
            int indexToRemove = random.Next(list.Count);  // Generate a random index
            list.RemoveAt(indexToRemove);
        }
        _stopwatch.Stop();
        UnityEngine.Debug.Log($"Removing items at random positions: {_stopwatch.Elapsed.TotalMilliseconds} ms");
    }

    [Test]
    public void Test_InsertingItemsAtStart()
    {
        var list = new List<int>();
        _stopwatch.Start();
        for (int i = 0; i < iterations; i++)
        {
            list.Insert(0, i); // Inserting at the start of the list
        }
        _stopwatch.Stop();
        UnityEngine.Debug.Log($"Inserting at start {iterations} times: {_stopwatch.Elapsed.TotalMilliseconds} ms");
    }

    [Test]
    public void Test_InsertingItemsAtMiddle()
    {
        var list = new List<int>();
        for (int i = 0; i < iterations; i++)
        {
            list.Add(i);
        }

        _stopwatch.Start();
        for (int i = 0; i < iterations; i++)
        {
            list.Insert(list.Count / 2, i); // Inserting in the middle
        }
        _stopwatch.Stop();
        UnityEngine.Debug.Log($"Inserting in the middle {iterations} times: {_stopwatch.Elapsed.TotalMilliseconds} ms");
    }
}
