using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace OddEvenJump
{
    class Program
    {
        public static Int32 OddNumberedJump(Int32[] arr, Int32 currentIdx)
        {
            if (currentIdx == arr.Length - 1)
            {
                return currentIdx;
            }

            var valToCompare = arr[currentIdx];

            var minRes = arr
                .Select(((value, index) => (value, index)))
                .Skip(currentIdx + 1)
                .GroupBy(
                    item => item.value,
                    item => item.index,
                    (value, indices) => (value, minIndex: indices.Min())
                )
                .Min(item => valToCompare <= item.value ? item : (value: Int32.MaxValue, minIndex: -1));

            return minRes.minIndex;
        }

        public static Int32 EvenNumberedJump(Int32[] arr, Int32 currentIdx)
        {
            if (currentIdx == arr.Length - 1)
            {
                return currentIdx;
            }

            var valToCompare = arr[currentIdx];

            var maxRes = arr
                .Select(((value, index) => (value, index)))
                .Skip(currentIdx + 1)
                .GroupBy(
                    item => item.value,
                    item => item.index,
                    (value, indices) => (value, minIndex: indices.Min())
                )
                .Max(item => valToCompare >= item.value ? item : (value: Int32.MinValue, minIndex: -1));

            return maxRes.minIndex;
        }

        public static Int32 OddEvenJumps(Int32[] arr)
        {
            // var valIdxGroups = arr
            //     .Select(((value, index) => (value, index)))
            //     .GroupBy(
            //         item => item.value,
            //         item => item.index,
            //         (value, indices) => (value, minIndex: indices.Min(), indices.ToList(), found: false)
            //     )
            //     .OrderBy(item => item.value);

            /**
             * Correct, but not optimal solution
             */

            Int32 goodIndicies = 0;
            for (var i = 0; i < arr.Length; i++)
            {
                Int32 currentJumpIdx = i;
                Int32 currentJump = 1;
                while (currentJumpIdx >= 0)
                {
                    currentJumpIdx = currentJump % 2 == 0
                        ? EvenNumberedJump(arr, currentJumpIdx)
                        : OddNumberedJump(arr, currentJumpIdx);

                    if (currentJumpIdx == arr.Length - 1)
                    {
                        goodIndicies++;
                        break;
                    }

                    currentJump++;
                }
            }

            return goodIndicies;
        }

        static void Main(string[] args)
        {
            // Int32[] arr = new Int32[] { 10, 13, 12, 14, 15 };

            Int32[] arr = new Int32[] { 2, 3, 1, 1, 4 };

            //Int32[] arr = new Int32[] { 5,1,3,4,2 };

            // Int32[] arr = new Int32[] {1,2,3,2,1,4,4,5};

            // var res = OddNumberedJump(arr, 0);
            // var res = EvenNumberedJump(arr, 1);

            // var content = File.ReadAllText("./OddEvenJump/large_array.txt");

            // var arr = Array.ConvertAll(
            //     content.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries),
            //     Int32.Parse);

            var goodIndicies = OddEvenJumps(arr);

            Console.WriteLine($"Number of good starting indicies {goodIndicies}");
        }
    }
}
