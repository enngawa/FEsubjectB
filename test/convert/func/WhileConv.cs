using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using test.convert.type;

namespace test.convert.func
{
    internal class WhileConv
    {
        public static string pattern = $"while（(?<A>[\\s\\S]*)が(?<B>[\\s\\S]*)(?<C>{String.Join("|",relTypes.TypeMap.Keys.ToList())})）";

        public static string convert(string input = "while（aがbより大きい）")
        {
            string result = Regex.Replace(input, pattern, match =>
            {
                string varA = match.Groups["A"].Value;
                string varB = match.Groups["B"].Value;
                string relType = relTypes.TypeMap[match.Groups["C"].Value];

                return $"while({varA} {relType} {varB}){{";
            });

            return result;
        }
    }
}
