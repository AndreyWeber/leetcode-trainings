using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace SketchbookProject;

public class Test
{
    public static void miniMaxSum(List<int> arr)
    {
        if (arr == null || !arr.Any())
        {
            return;
        }

        var sortedArr = arr.Select(Convert.ToInt64).ToList();
        sortedArr.Sort();

        long minSum = sortedArr.Take(arr.Count - 1).Sum();
        long maxSum = sortedArr.Skip(1).Sum();

        Console.WriteLine($"{minSum} {maxSum}");
    }

    public static string timeConversion(string s)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            return string.Empty;
        }

        var tokens = s.Split(":").ToArray();
        var hours = Int32.Parse(tokens[0]);
        var minutes = Int32.Parse(tokens[1]);
        var seconds = tokens[2].Substring(0, 2);
        var meridian = tokens[2].Substring(2, 2);
        if (hours == 12 && meridian == "AM")
        {
            hours -= 12;
        }

        if (hours < 12 && meridian == "PM")
        {
            hours += 12;
        }

        return $"{hours:00}:{minutes:00}:{seconds:00}";
    }

    public static int findMedian(List<int> arr)
    {
        if (arr == null || !arr.Any())
        {
            return 0;
        }

        Console.WriteLine(string.Join(", ", arr));
        Console.WriteLine("Count" + arr.Count);

        arr.Sort();
        var medianIndex = arr.Count / 2;

        return arr[medianIndex];
    }





    public static void foo()
    {

    }

    public static void Main(string[] args)
    {
        foo();

        Console.ReadKey(false);
    }
}