using System;
using System.Linq;
using System.Collections.Generic;

namespace UniqueEmailAddresses
{
    class Program
    {
        public static int NumUniqueEmailsUsingCharAnalysis(string[] emails)
        {
            var uniqueEmails = new HashSet<String>();
            foreach (var email in emails)
            {
                var localName = new List<Char>();
                foreach (var @char in email)
                {
                    if (@char == '@' || @char == '+')
                    {
                        break;
                    }

                    if (@char == '.')
                    {
                        continue;
                    }

                    localName.Add(@char);
                }

                var domainName = new List<Char>();
                for (var i = email.Length - 1; i >= 0; i--)
                {
                    if (email[i] == '@')
                    {
                        break;
                    }

                    domainName.Add(email[i]);
                }

                var parsedEmail = $"{new String(localName.ToArray())}@{new String(domainName.Reverse<Char>().ToArray())}";

                if (uniqueEmails.Contains(parsedEmail))
                {
                    continue;
                }

                uniqueEmails.Add(parsedEmail);
            }

            return uniqueEmails.Count;
        }

        public static int NumUniqueEmailsUsingSplit(string[] emails)
        {
            var uniqueEmails = new HashSet<String>();
            foreach (var email in emails)
            {
                var tokens = email
                    .Split(new[] {'@'}, StringSplitOptions.RemoveEmptyEntries);

                var localName = tokens
                    .First()
                    .Split(new[] {'+'}, StringSplitOptions.RemoveEmptyEntries)
                    .First()
                    .Replace(".", "");
                var domainName = tokens.Last();

                var parsedEmail = $"{localName}@{domainName}";

                if (uniqueEmails.Contains(parsedEmail))
                {
                    continue;
                }

                uniqueEmails.Add(parsedEmail);
            }

            return uniqueEmails.Count;
        }

        static void Main(string[] args)
        {
            //var emails = new[] { "test.email+alex@leetcode.com","test.e.mail+bob.cathy@leetcode.com","testemail+david@lee.tcode.com" };
            var emails = new[] { "linqmafia@leet+code.com", "linqmafia@code.com" };
            //var emails = new[] { "a@leetcode.com","b@leetcode.com","c@leetcode.com" };

            /**
             * Time Complexity: O(N*M)
             * Space Complexity: O(N*M)
             * N - Mumber of emails
             * M - number of chars in email
             */

            var result = NumUniqueEmailsUsingCharAnalysis(emails);

            Console.WriteLine(result);
        }
    }
}
