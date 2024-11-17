using System;
using System.Diagnostics;
using System.Text;
using UnityEngine;

public static class Benchmark
{
    private const string RatingExplained = "Rating represents the amount of operations needed to hurt performens theoretically";
    public static void LogTitle(string message, Color color)
    {
        StringBuilder output = new StringBuilder();
        output.Append("<color=#").Append(ColorUtility.ToHtmlStringRGB(color)).Append(">")
            .Append(message)
            .Append("</color>\n<color=#")
            .Append(ColorUtility.ToHtmlStringRGB(Color.grey)).Append(">")
            .Append(RatingExplained)
            .Append("</color>");
        UnityEngine.Debug.Log(output.ToString());
    }
    
    public static void Log(string message, Color color, Stopwatch stopwatch = null, int avgFactor = 1)
    {
        StringBuilder output = new StringBuilder();
        output.Append("<color=#")
            .Append(ColorUtility.ToHtmlStringRGB(color))
            .Append(">")
            .Append(message);

        if (stopwatch != null)
        {
            double milliseconds = stopwatch.Elapsed.TotalMilliseconds / avgFactor;
            output.AppendFormat(" {0:F3} ms", milliseconds); // Use format directly in AppendFormat
            if (avgFactor > 1)
            {
                output.Append(" avg");
            }
            output.Append("\nRating: ")
                .Append(Rate(stopwatch, avgFactor));
        }
        else
        {
            output.Append("\n");
        }

        output.Append("</color>");
        UnityEngine.Debug.Log(output.ToString());
    }

    private static int Rate(Stopwatch stopwatch, int avgFactor)
    {
        double operationsForFrameDrop = 0.0f;
        int result = 0;
        double frameRenderTimeMs = 1000/60;
        operationsForFrameDrop = frameRenderTimeMs / (stopwatch.Elapsed.TotalMilliseconds / avgFactor);
        result = (int)Math.Floor(operationsForFrameDrop);
        return result;
    }
}
