using System;
using System.Linq;
using System.Collections.Generic;

public static class Program
{
    /*
     Each day a quarry-worker is given a pile of stones and told to reduce the larger stones onto smaller ones.
     The worker must smash the stones together to reduce them, and is told to always pick up the largest two stones and smash them together.
     If the stones are of equal weight, they both disentegrate entirely. If one is larger, the smaller one is disentegrated and
     larger one is reduced by the weight of the smaller one. Eventually, there is either one stone left, that cannot be broken,
     or all of the stones have been smashed. Determine the weight of the last stone, or return 0 if there is none.

     Example: weights = [1,2,3,6,7,7]

     There worker always starts with the two largest stones.

     Complete the functions `lastStoneWeight`. The function must return and int. `lastStoneWeight` hase in following parameter
     `int weights[n]` the array of integers indicating the weights of each stone

     Constraints:
     1 <= n <= 10^5
     1 <= weight[i] <=10^9
     */
    public static int lastStoneWeight(int[] weights)
    {
        // Create a max heap
        var heap = new MaxHeap(weights.Length);

        // Add all weights to the heap
        foreach (var weight in weights)
        {
            heap.Insert(weight);
        }

        while (heap.Size > 1)
        {
            // Remove the two heaviest stones
            int stone1 = heap.RemoveMax();
            int stone2 = heap.RemoveMax();

            // If they're not the same, add the remainder to the heap
            if (stone1 != stone2)
            {
                heap.Insert(stone1 - stone2);
            }
        }

        // Return the last stone, or 0 if there are no stones left
        return heap.IsEmpty() ? 0 : heap.RemoveMax();
    }

    public static void Main(string[] args)
    {
        int[] weights = { 1, 2, 3, 6, 7, 7, 11 };
        Console.WriteLine(lastStoneWeight(weights));
        Console.ReadKey(true);
    }
}