using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace test.convert.func
{
    internal class AssignConv
    {
        public static string pattern = @"(\w+)←(.+)";

        public static string convert(string input = "A←1")
        {
            string result = Regex.Replace(input, pattern, match =>
            {
                string varName = match.Groups[1].Value;
                string val = match.Groups[2].Value;

                return $"{varName} = {val};";
            });

            return result;
        }
    }
}
