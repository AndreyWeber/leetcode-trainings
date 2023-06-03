using System;

public class Program
{
    /*
        Start with an infinite two dimensional grid filled with zeros, indexed from (1,1) at he bottom left
        corner with coordinates increasing toward the top and right. Given a series of coordinates (r,x) where
        r is the ending row and c is the ending column, add 1 to each element in the range from (1,1) to (r,c,)
        inclusive. Once all coordinates are precessed, determine how many cells contain the maximal value in the grid
        input looks like `upRight = ["1 4", "2 3", "4 1"]`, where each string represent `r` and `c` respectively

        Complete the function countMax:
            return `long`: the number of occurences of the final grid's maximal element

        Contraints:
            1 <= n <= 100
            1 <= number of rows, number columns <= 10^6
     */

    /// <summary>
    /// The key idea to solve this problem efficiently is to realize that it's not necessary to keep track of
    /// the entire grid. We only need to keep track of the minimum value of r and c. The maximum value on the
    /// grid will always be at the (1,1) position and any cells in the range (1,1) to (minR, minC) will contain
    /// the maximal value, other cells in the grid will have less value.
    ///
    /// This solution has a time complexity of O(n), where n is the number of (r, c) pairs in the upRight array,
    /// because we need to loop over each pair once.
    ///
    /// Its space complexity is O(1), because it uses a constant amount of space to store minR and minC,
    /// regardless of the size of the input.
    /// </summary>
    static long countMax(string[] upRight)
    {
        int minR = int.MaxValue;
        int minC = int.MaxValue;

        foreach(var s in upRight)
        {
            var parts = s.Split(" ");
            minR = Math.Min(minR, int.Parse(parts[0]));
            minC = Math.Min(minC, int.Parse(parts[1]));
        }

        return (long)minR * minC;
    }

    public static void Main(string[] args)
    {
        string[] upRight = {"1 4", "2 3", "4 1"};
        Console.WriteLine(countMax(upRight));  // Output: 1
    }
}
