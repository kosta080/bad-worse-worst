using NUnit.Framework;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;


public class IntListPerformanceTests
{
    private Stopwatch _stopwatch;
    private const int Iterations = 100000;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Benchmark.LogTitle("Testing different List<int> operations", Color.yellow );
    }

    [SetUp]
    public void Setup()
    {
        _stopwatch = new Stopwatch();
    }

    [Test]
    public void Adding_Items_To_List_Of_Int()
    {
        _stopwatch.Restart();
        var list = new List<int>();
        for (int i = 0; i < Iterations; i++)
        {
            list.Add(i);
        }
        _stopwatch.Stop();
        Benchmark.Log("Adding items:", Color.white,_stopwatch, Iterations );
    }

    [Test]
    public void Accessing_Items_From_List_Of_Int()
    {
        var list = new List<int>();
        for (int i = 0; i < Iterations; i++)
        {
            list.Add(i);
        }

        _stopwatch.Restart();
        for (int i = 0; i < Iterations; i++)
        {
            var item = list[i];
        }
        _stopwatch.Stop();
        Benchmark.Log("Accessing items:", Color.white,_stopwatch, Iterations );
    }

    [Test]
    public void Removing_Items_From_List_Of_Int()
    {
        var list = new List<int>();
        for (int i = 0; i < Iterations; i++)
        {
            list.Add(i);
        }

        _stopwatch.Restart();
        for (int i = Iterations - 1; i >= 0; i--)
        {
            list.RemoveAt(i);
        }
        _stopwatch.Stop();
        Benchmark.Log("Removing items:", Color.white, _stopwatch, Iterations );
    }

    
    [Test]
    public void Removing_Items_At_Random_List_Position()
    {
        System.Random random = new();
        var list = new List<int>();
        for (int i = 0; i < Iterations; i++)
        {
            list.Add(i);
        }
        _stopwatch.Restart();
        while (list.Count > 0)
        {
            int indexToRemove = random.Next(list.Count); 
            list.RemoveAt(indexToRemove);
        }
        _stopwatch.Stop();
        Benchmark.Log("Removing items at random positions:", Color.white, _stopwatch, Iterations );
    }

    [Test]
    public void Inserting_Items_At_Beginning_Of_List_Of_Int()
    {
        var list = new List<int>();
        _stopwatch.Restart();
        for (int i = 0; i < Iterations; i++)
        {
            list.Insert(0, i); 
        }
        _stopwatch.Stop();
        Benchmark.Log("Inserting at start:", Color.white, _stopwatch, Iterations );
    }

    [Test]
    public void Inserting_Items_At_Middle_Of_List_Of_Int()
    {
        var list = new List<int>();
        for (int i = 0; i < Iterations; i++)
        {
            list.Add(i);
        }
        _stopwatch.Restart();
        for (int i = 0; i < Iterations; i++)
        {
            list.Insert(list.Count / 2, i); // Inserting in the middle
        }
        _stopwatch.Stop();
        Benchmark.Log("Inserting in the middle:", Color.white, _stopwatch, Iterations );
    }
}
