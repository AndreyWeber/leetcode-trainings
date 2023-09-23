// var nums = new int[] { -7, -3, 2, 3, 11 };
// var nums = new int[] { -5, -3, -2, -1 };
var nums = new int[] { -10000, -9999, -7, -5, 0, 0, 10000 };
var result = SortedSquares(nums);

static int[] SortedSquares(int[] nums)
{
    var left = 0;
    var right = nums.Length - 1;
    var res = new int[nums.Length];

    var val = 0;
    for (var i = right; i >= 0; i--)
    {
        if (Math.Abs(nums[left]) < Math.Abs(nums[right]))
        {
            val = nums[right];
            right--;
        }
        else
        {
            val = nums[left];
            left++;
        }
        res[i] = val * val;
    }

    return res;
}