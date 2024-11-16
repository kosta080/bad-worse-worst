using System;
using System.Diagnostics;
using System.Text;
using UnityEngine;

namespace Tests
{
    public static class Benchmark
    {
        public static void Log(string message, Color color, Stopwatch stopwatch = null, int avgFactor = 1)
        {
            StringBuilder output = new();
            output.Append($"<color=#{ColorUtility.ToHtmlStringRGB(color)}>");
            output.Append(message);
            if (stopwatch != null)
            {
                double milliseconds = stopwatch.Elapsed.TotalMilliseconds / avgFactor;
                string formattedTime = milliseconds.ToString("F20").TrimEnd('0').TrimEnd('.');
                string avgText = avgFactor > 1 ? " avg" : "";
                output.Append($" {formattedTime} ms {avgText} ");
                output.Append("\n").Append(Vis(stopwatch, avgFactor));
            }
            else
            {
                output.Append("\n");
            }
            output.Append("</color>");
            UnityEngine.Debug.Log(output);
        }

        private static string Vis(Stopwatch stopwatch, int avgFactor)
        {
            double time = stopwatch.Elapsed.TotalMilliseconds / avgFactor;
            int n = (int)Math.Floor(time / 0.0001f);
            string v = "";
            for (int i = 0; i < n; i++)
            {
                v += ".";
            }
            return v;
        }
    }
}