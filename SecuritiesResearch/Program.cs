using System;

public class Program
{
    /*
        In securities research, an analyst will look at a number of attributes for a stock. One analyst would like to
        keep a record of the highest positive spread between a closing price and the closing price on any prior day
        in history. Determine the maximum positive spread for a stock given it's price history. If the stock remains
        flat or declines for the full period, return -1.

        Example 0: `px = [7, 1, 2, 5]`

        - At the first quote, there is no earlier quote to compare to.
        - Ath the second quote, there was no earlier price that was lower
        - Ath the third quote, the price is higher that the second quote: 2 - 1 = 1
        - For the fourth quote, the price is higher than the third and the second quotes: 5 - 2 = 3 and 5 - 1 = 4
        - The max difference is 4

        Example 1 `px = [7, 5, 3, 1]`

        - The price declines each quote, so there is never a difference greated than 0, In this case, return -1

        Complete the function `maxDifference`

        `maxDifference` has the following parameters:
            `int px[n]` a list of stock prices (quotes)
        returesn: int: the maximum difference between two prices as described above

        Constraints:
            1 <= n <=10^5
            -10^5 <= px[i] <= 10^5
     */

    /// <summary>
    /// This problem can be solved by iterating through the array and at each iteration,
    /// keeping track of the minimum price seen so far and the maximum profit
    /// (which is the current price minus the minimum price seen so far). The function
    /// maxDifference should return the maximum profit if it's greater than zero, otherwise,
    /// it should return -1
    /// </summary>
    public static int maxDifference(int[] px)
    {
        if (px.Length < 2)
        {
            return -1;
        }

        int minPrice = px[0];
        int maxProfit = px[1] - px[0];

        for (int i = 1; i < px.Length; i++)
        {
            if (px[i] < minPrice)
            {
                minPrice = px[i];
            }
            else if (px[i] - minPrice > maxProfit)
            {
                maxProfit = px[i] - minPrice;
            }
        }

        return maxProfit > 0 ? maxProfit : -1;
    }

    public static void Main(string[] args)
    {
        var px0 = new[] { 7, 1, 2, 5 };
        Console.WriteLine(maxDifference(px0));

        var px1 = new[] { 7, 5, 3, 1 };
        Console.WriteLine(maxDifference(px1));
        Console.ReadKey(true);
    }
}