var s = new[] { 'H', 'a', 'n', 'n', 'a', 'h' };

Console.WriteLine(new string(s));

ReverseString(s);

Console.WriteLine(new string(s));
Console.ReadKey(false);

static void ReverseString(char[] s)
{
    if (s == null || !s.Any())
    {
        return;
    }

    var left = 0;
    var right = s.Length - 1;
    while (left <= right)
    {
        (s[right], s[left]) = (s[left], s[right]);
        left++;
        right--;
    }
}