// Constraints:

// n == nums.length
// 1 <= k <= n <= 105
// -10^4 <= nums[i] <= 10^4

// Input: nums = [1,12,-5,-6,50,3], k = 4
// Output: 12.75000
// Explanation: Maximum average is (12 - 5 - 6 + 50) / 4 = 51 / 4 = 12.75

// Input: nums = [5], k = 1
// Output: 5.00000

// var nums = new[] { 1, 12, -5, -6, 50, 3 };
var nums = new[] { 5 };
// var k = 4;
var k = 1;
var result = FindMaxAverage(nums, k);
Console.WriteLine(result);
Console.ReadKey(true);

// Solution based on the Sliding Window technique
static double FindMaxAverage(int[] nums, int k)
{
    if (k == 0)
    {
        return 0.0;
    }

    var curr = 0.0;
    for (var i = 0; i < k; i++)
    {
        curr += nums[i];
    }

    var result = curr;
    for (var i = k; i < nums.Length; i++)
    {
        curr += nums[i] - nums[i - k];
        result = Math.Max(result, curr);
    }

    return result / k;
}
