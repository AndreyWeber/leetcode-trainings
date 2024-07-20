var nums = new int[] { -7, -3, 2, 3, 11 };
// var nums = new int[] { -5, -3, -2, -1 };
// var nums = new int[] { -10000, -9999, -7, -5, 0, 0, 10000 };
var result = SortedSquares(nums);

static int Abs(int value)
{
    int mask = value >> 31;
    return (value ^ mask) - mask;
}

static int[] SortedSquares(int[] nums)
{
    var left = 0;
    var right = nums.Length - 1;

    var result = new int[nums.Length];
    for (var i = right; i >= 0; i--)
    {
        if (Abs(nums[left]) >= Abs(nums[right]))
        {
            result[i] = nums[left] * nums[left];
            left++;
        }
        else
        {
            result[i] = nums[right] * nums[right];
            right--;
        }
    }

    return result;
}