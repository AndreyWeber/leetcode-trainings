// Constraints:

// 1 <= nums.length <= 10^5
// nums[i] is either 0 or 1.
// 0 <= k <= nums.length

// Input: nums = [1,1,1,0,0,0,1,1,1,1,0], k = 2
// Output: 6
// Explanation: [1,1,1,0,0,1,1,1,1,1,1]
// Bolded numbers were flipped from 0 to 1. The longest subarray is underlined.

// Input: nums = [0,0,1,1,0,0,1,1,1,0,1,1,0,0,0,1,1,1,1], k = 3
// Output: 10
// Explanation: [0,0,1,1,1,1,1,1,1,1,1,1,0,0,0,1,1,1,1]
// Bolded numbers were flipped from 0 to 1. The longest subarray is underlined.

// var nums = new[] { 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 0 };
var nums = new[] { 0, 0, 1, 1, 0, 0, 1, 1, 1, 0, 1, 1, 0, 0, 0, 1, 1, 1, 1 };
// var k = 2;
var k = 3;
var result = LongestOnes(nums, k);

Console.WriteLine(result);
Console.ReadKey(true);

static int LongestOnes(int[] nums, int k)
{
    var result = 0;
    var left = 0;
    var curr = 0;
    for (var right = 0; right < nums.Length; right++)
    {
        if (nums[right] == 0)
        {
            curr += 1;
        }

        while (curr > k)
        {
            if (nums[left] == 0)
            {
                curr -= 1;
            }
            left += 1;
        }

        result = Math.Max(result, right - left + 1);
    }

    return result;
}