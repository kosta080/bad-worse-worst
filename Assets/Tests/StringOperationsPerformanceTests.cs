using UnityEngine;
using NUnit.Framework;
using System.Diagnostics;
using System.Text;

public class StringOperationsPerformanceTests
{
    private Stopwatch _stopwatch;
    private const int Iterations = 10000;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        Benchmark.LogTitle(" Testing different methods of converting int to string", Color.yellow );
    }

    [SetUp]
    public void Setup()
    {
        _stopwatch = new Stopwatch();
    }

    [Test]
    public void Int_ToString_Concatenation()
    {
        _stopwatch.Restart();
        var temp = "";
        for (int i = 0; i < Iterations; i++)
        {
            temp += i.ToString() + "_";
        }
        _stopwatch.Stop();
        Benchmark.Log("ToString() + concatenation:", Color.white,_stopwatch, Iterations );
    }

    [Test]
    public void Format_Performence()
    {
        _stopwatch.Restart();
        var temp = string.Empty;
        for (int i = 0; i < Iterations; i++)
        {
            temp += $"{i}_";
        }
        _stopwatch.Stop();
        Benchmark.Log("String interpolation:", Color.white,_stopwatch, Iterations );
    }

    [Test]
    public void Concat_With_String_Constructor()
    {
        _stopwatch.Restart();
        var temp = string.Empty;
        for (int i = 0; i < Iterations; i++)
        {
            temp += new string(i + "_");
        }
        _stopwatch.Stop();
        Benchmark.Log("Concat with new string:", Color.white,_stopwatch, Iterations );
    }

    [Test]
    public void Concat_With_Plus_Operator()
    {
        _stopwatch.Restart();
        var temp = string.Empty;
        for (int i = 0; i < Iterations; i++)
        {
            temp += i + "_";
        }
        _stopwatch.Stop();
        Benchmark.Log("Concat with + operator:", Color.white,_stopwatch, Iterations );
    }

    [Test]
    public void StringBuilder_Append()
    {
        _stopwatch.Restart();
        var sb = new StringBuilder();
        for (int i = 0; i < Iterations; i++)
        {
            sb.Append(i).Append("_");
        }
        _stopwatch.Stop();
        Benchmark.Log("StringBuilder append:", Color.white,_stopwatch, Iterations );
    }

    [Test]
    public void Test_StringBuilder_AppendFormat()
    {
        _stopwatch.Restart();
        var sb = new StringBuilder();
        for (int i = 0; i < Iterations; i++)
        {
            sb.AppendFormat("{0}_", i);
        }
        _stopwatch.Stop();
        Benchmark.Log("StringBuilder AppendFormat:", Color.white,_stopwatch, Iterations );
    }

    [Test]
    public void Test_String_Concat_StaticMethod()
    {
        _stopwatch.Restart();
        var temp = string.Empty;
        var parts = new string[Iterations];
        for (int i = 0; i < Iterations; i++)
        {
            parts[i] = i + "_";
        }
        temp = string.Concat(parts);
        _stopwatch.Stop();
        Benchmark.Log("String.Concat():", Color.white,_stopwatch, Iterations );
    }
}
