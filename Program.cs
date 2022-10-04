using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RomanArabic
{
    internal class Program
    {
        private static readonly Dictionary<int, string> NumbersMap = new()
        {
            { 1000, "M" },
            { 900, "CM" },
            { 500, "D" },
            { 400, "CD" },
            { 100, "C" },
            { 90, "XC" },
            { 50, "L" },
            { 40, "XL" },
            { 10, "X" },
            { 9, "IX" },
            { 5, "V" },
            { 4, "IV" },
            { 1, "I" }
        };

        private static void Main(string[] args)
        {
            Console.WriteLine("Input text:");
            var input = Console.ReadLine();

            var romanNumberRegex = new Regex(@"\b(M{0,3})(C[MD]|D?C{0,3})(X[CL]|L?X{0,3})(I[XV]|V?I{0,3})\b");
            var output = romanNumberRegex.Replace(input, match =>
            {
                var romanNumber = match.Value;
                if (romanNumber == string.Empty)
                    return romanNumber;
                var arabicNumber = ToArabic(romanNumber);
                return arabicNumber.ToString();
            });

            Console.WriteLine();
            Console.WriteLine("Result:");
            Console.WriteLine(output);
            Console.Read();
        }

        private static int ToArabic(string number) => NumbersMap
            .Where(d => number.StartsWith(d.Value))
            .Select(d => d.Key + ToArabic(number[d.Value.Length..]))
            .FirstOrDefault();
    }
}
