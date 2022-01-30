using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace OddEvenJump
{
    class Program
    {
        public static Int32 OddNumberedJump(List<(Int32 value, Int32 index)> collection, Int32 valToCompare)
        {
            var groups = collection
                .GroupBy(
                    item => item.value,
                    item => item.index,
                    (value, indices) => (value, minIndex: indices.Min())
                );
            var minRes = groups.Min(item => valToCompare <= item.value ? item : (value: Int32.MaxValue, minIndex: -1));

            return minRes.minIndex;
        }

        public static Int32 EvenNumberedJump(List<(Int32 value, Int32 index)> collection, Int32 valToCompare)
        {
            var groups = collection
                .GroupBy(
                    item => item.value,
                    item => item.index,
                    (value, indices) => (value, minIndex: indices.Min())
                );
            var maxRes = groups.Max(item => valToCompare >= item.value ? item : (value: Int32.MinValue, minIndex: -1));

            return maxRes.minIndex;
        }

        /**
         * Correct, but brute force solution
         * O(N^2)
         */
        public static Int32 OddEvenJumpsBruteForce(Int32[] arr)
        {
            var collection = arr
                .Select((value, index) => (value, index))
                .ToList();

            Int32 goodIndicies = 0;
            for (var i = 0; i < arr.Length; i++)
            {
                Int32 currentJumpIdx = i;
                Int32 currentJump = 1;
                while (currentJumpIdx >= 0)
                {
                    currentJumpIdx = currentJump % 2 == 0
                        ? EvenNumberedJump(collection, currentJumpIdx)
                        : OddNumberedJump(collection, currentJumpIdx);

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

        /**
         * Correct solution based on dynamic programming approach
         * Still can be significantly improved
         * The idea is to start from the end of an input array and
         * back track each odd and even jump leading to the end of array
         * It will let to define all "good indicies" we can start from to reach
         * out the end of array
         * O(NlogN)
         */
        public static Int32 OddEvenJumpsDynamicProgramming(Int32[] arr)
        {
            var arrLength = arr.Length;

            var oddJumps = new Boolean[arrLength];
            var evenJumps = new Boolean[arrLength];

            oddJumps[arrLength - 1] = true;
            evenJumps[arrLength - 1] = true;

            var backTrack = new List<(Int32 value, Int32 index)>();
            backTrack.Add((value: arr[arrLength - 1], index: arrLength - 1));

            var goodIndexCount = 1;
            for (var i = arrLength - 2; i >= 0; --i)
            {
                // Both methods can be improved
                var odd = OddNumberedJump(backTrack, arr[i]);
                var even = EvenNumberedJump(backTrack, arr[i]);

                if (odd > -1)
                {
                    oddJumps[i] = evenJumps[odd];
                }

                if (even > -1)
                {
                    evenJumps[i] = oddJumps[even];
                }

                if (oddJumps[i])
                {
                    goodIndexCount++;
                }

                backTrack.Add((value: arr[i], index: i));
            }

            return goodIndexCount;
        }

        static void Main(string[] args)
        {
            // Int32[] arr = new Int32[] { 10, 13, 12, 14, 15 };

            Int32[] arr = new Int32[] { 2, 3, 1, 1, 4 };

            //Int32[] arr = new Int32[] { 5, 1, 3, 4, 2 };

            // Int32[] arr = new Int32[] {1,2,3,2,1,4,4,5};

            // var res = OddNumberedJump(arr, 0);
            // var res = EvenNumberedJump(arr, 1);

            // var content = File.ReadAllText("./OddEvenJump/large_array.txt");

            // var arr = Array.ConvertAll(
            //     content.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries),
            //     Int32.Parse);

            var goodIndicies = OddEvenJumpsDynamicProgramming(arr);

            Console.WriteLine($"Number of good starting indicies {goodIndicies}");
        }
    }
}
