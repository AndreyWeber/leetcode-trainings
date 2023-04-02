using System;
using System.Linq;
using System.Collections.Generic;

namespace BinarySearch;

public class Solution
{
    public static void Main(string[] args)
    {
         var nums = new[] { -1,0,3,5,9,12 };
        // var nums = new[] { 5 };
        // var nums = new[] { -1,0,5 };
        // var nums = new[] { 2, 5 };
        // var nums = new[] { -1,0,3,5,9,12 };
        const int target = 9;
        // const int target = 2;
        // const int target = -5;
        // const int target = 5;
        // const int target = -1;
        // const int target = 0;

        var result = LoopSearch(nums, target);

        Console.WriteLine(result);
        Console.ReadKey(true);
    }

    /// <summary>
    /// Search using while loop
    /// Runtime complexity: O(log N)
    /// </summary>
    public static int LoopSearch(int[] nums, int target)
    {
        var left = 0;
        var right = nums.Length - 1;
        int middle;

        while (left <= right)
        {
            middle = (left + right) / 2;

            if (nums[middle] == target)
            {
                return middle;
            }

            if (nums[middle] < target)
            {
                left = middle + 1;
                continue;
            }

            if (nums[middle] > target)
            {
                right = middle - 1;
            }
        }

        return -1;
    }

    /// <summary>
    /// Recursion search
    /// Runtime complexity: O(log N)
    /// </summary>
    public static int RecurSearch(int[] nums, int target)
    {
        var innerNums = nums.Select((val, idx) => (idx: idx, val: val)).ToArray();

        (int idx, int val) InnerSearch((int idx, int val)[] arr)
        {
            var right = arr.Length - 1;
            if (right == -1)
            {
                return (-1, -1);
            }

            var middle = right / 2;

            if (arr[middle].val == target)
            {
                return arr[middle];
            }

            if (arr[middle].val > target && middle > 0)
            {
                return InnerSearch(arr[..middle]);
            }

            if (arr[middle].val < target)
            {
                middle++;
                return InnerSearch(arr[middle..]);
            }

            return (-1, -1);
        }
        
        var result = InnerSearch(innerNums);

        return result.idx;
    }
}
