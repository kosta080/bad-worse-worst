using UnityEngine;
using NUnit.Framework;
using System.Diagnostics;
using System.Text;
using Tests;

public class StringOperationsPerformanceTests
{
    private Stopwatch _stopwatch;
    private readonly int iterations = 10000;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Benchmark.Log(" Testing different methods of converting int to string", Color.yellow );
    }

    [SetUp]
    public void Setup()
    {
        _stopwatch = new Stopwatch();
    }

    [Test]
    public void Test_ToString_Concatenation()
    {
        _stopwatch.Restart();
        var temp = "";
        for (int i = 0; i < iterations; i++)
        {
            temp += i.ToString() + "_";
        }
        _stopwatch.Stop();
        //UnityEngine.Debug.Log($"ToString() + concatenation: {_stopwatch.Elapsed.TotalMilliseconds} ms\n" +$"{temp}");
        
        Benchmark.Log("ToString() + concatenation:", Color.white,_stopwatch, iterations );
    }

    [Test]
    public void Test_Interpolation()
    {
        _stopwatch.Restart();
        var temp = string.Empty;
        for (int i = 0; i < iterations; i++)
        {
            temp += $"{i}_";
        }
        _stopwatch.Stop();
        //UnityEngine.Debug.Log($"String interpolation: {_stopwatch.Elapsed.TotalMilliseconds} ms \n" + $"{temp}");
        Benchmark.Log("String interpolation:", Color.white,_stopwatch, iterations );
    }

    [Test]
    public void Test_Concat_WithStringConstructor()
    {
        _stopwatch.Restart();
        var temp = string.Empty;
        for (int i = 0; i < iterations; i++)
        {
            temp += new string(i + "_");
        }
        _stopwatch.Stop();
        //UnityEngine.Debug.Log($"Concat with new string: {_stopwatch.Elapsed.TotalMilliseconds} ms \n" + $"{temp}");
        Benchmark.Log("Concat with new string:", Color.white,_stopwatch, iterations );
    }

    [Test]
    public void Test_Concat_WithPlusOperator()
    {
        _stopwatch.Restart();
        var temp = string.Empty;
        for (int i = 0; i < iterations; i++)
        {
            temp += i + "_";
        }
        _stopwatch.Stop();
        //UnityEngine.Debug.Log($"Concat with + operator: {_stopwatch.Elapsed.TotalMilliseconds} ms \n" + $"{temp}");
        Benchmark.Log("Concat with + operator:", Color.white,_stopwatch, iterations );
    }

    [Test]
    public void Test_StringBuilder_Append()
    {
        _stopwatch.Restart();
        var sb = new StringBuilder();
        for (int i = 0; i < iterations; i++)
        {
            sb.Append(i).Append("_");
        }
        _stopwatch.Stop();
        //UnityEngine.Debug.Log($"StringBuilder append: {_stopwatch.Elapsed.TotalMilliseconds} ms \n" + $"{sb}");
        Benchmark.Log("StringBuilder append:", Color.white,_stopwatch, iterations );
    }

    [Test]
    public void Test_StringBuilder_AppendFormat()
    {
        _stopwatch.Restart();
        var sb = new StringBuilder();
        for (int i = 0; i < iterations; i++)
        {
            sb.AppendFormat("{0}_", i);
        }
        _stopwatch.Stop();
        //UnityEngine.Debug.Log($"StringBuilder AppendFormat: {_stopwatch.Elapsed.TotalMilliseconds} ms \n" + $"{sb}");
        Benchmark.Log("StringBuilder AppendFormat:", Color.white,_stopwatch, iterations );
    }

    [Test]
    public void Test_String_Concat_StaticMethod()
    {
        _stopwatch.Restart();
        var temp = string.Empty;
        var parts = new string[iterations];
        for (int i = 0; i < iterations; i++)
        {
            parts[i] = i + "_";
        }
        temp = string.Concat(parts);
        _stopwatch.Stop();
        //UnityEngine.Debug.Log($"String.Concat(): {_stopwatch.Elapsed.TotalMilliseconds} ms \n" + $"{temp}");
        Benchmark.Log("String.Concat():", Color.white,_stopwatch, iterations );
    }
}
