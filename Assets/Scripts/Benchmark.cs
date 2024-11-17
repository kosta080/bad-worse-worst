using System;
using System.Diagnostics;
using System.Text;
using UnityEngine;

public static class Benchmark
{
    private const string RatingExplained = "Rating represents the amount of operations needed to hurt performens theoretically";
    public static void LogTitle(string message, Color color)
    {
        var output = new StringBuilder();
        var colorHex = ColorUtility.ToHtmlStringRGB(color);
        var greyHex = ColorUtility.ToHtmlStringRGB(Color.grey);
        output.AppendFormat("<color=#{0}>{1}</color>\n", colorHex, message);
        output.AppendFormat("<color=#{0}>{1}</color>", greyHex, RatingExplained);
        UnityEngine.Debug.Log(output.ToString());
    }
    
    public static void Log(string message, Color color, Stopwatch stopwatch = null, int avgFactor = 1)
    {
        StringBuilder output = new StringBuilder();
        string colorHex = ColorUtility.ToHtmlStringRGB(color);
        output.AppendFormat("<color=#{0}>{1}</color>", colorHex, message);
        if (stopwatch != null)
        {
            double milliseconds = stopwatch.Elapsed.TotalMilliseconds / avgFactor;
            output.AppendFormat(" {0:F6} ms", milliseconds); // Include formatted milliseconds
            if (avgFactor > 1) output.Append(" avg");
            int rating = Rate(stopwatch, avgFactor); // Assume Rate returns an int or similar
            output.AppendFormat("\nRating: {0}", rating);
        }
        output.Append("\n");
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
