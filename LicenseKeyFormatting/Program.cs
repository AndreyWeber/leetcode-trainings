using System;
using System.Collections.Generic;

namespace LicenseKeyFormatting
{
    class Program
    {
        public static String LicenseKeyFormatting(String s, Int32 k)
        {
            if (String.IsNullOrWhiteSpace(s))
            {
                return String.Empty;
            }

            if (k <= 0)
            {
                return s;
            }

            if (s.Length == 1)
            {
                return s.Equals("-") ? String.Empty : s.ToUpper();
            }

            var chars = new List<Char>();

            Int32 charCounter = 0;
            for(var i = 0; i < s.Length; i++)
            {
                var c = s[i];

                if (c == '-')
                {
                    continue;
                }

                if (c is >= 'a' and <= 'z')
                {
                    chars.Add(Char.ToUpper(c));
                }
                else
                {
                    chars.Add(c);
                }

                charCounter++;
            }

            var shortGroupLength = charCounter % k;

            var resultChars = new List<Char>();
            if (shortGroupLength > 0)
            {
                resultChars.AddRange(chars.GetRange(0, shortGroupLength));
                resultChars.Add('-');
            }

            var groupSize = 0;
            for (var i = shortGroupLength; i < charCounter; i++)
            {
                if (groupSize == k)
                {
                    resultChars.Add('-');
                    groupSize = 1;
                }
                else
                {
                    groupSize++;
                }

                resultChars.Add(chars[i]);
            }

            return new String(resultChars.ToArray());
        }

        static void Main(string[] args)
        {
            var result = LicenseKeyFormatting("5F3Z-2e-9-w", 4);

            // var result = LicenseKeyFormatting("2-5g-3-J", 2);

            Console.WriteLine(result);
        }
    }
}
