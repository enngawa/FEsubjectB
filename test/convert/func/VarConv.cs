using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace test.convert.func
{
    internal class VarConv
    {
        public static string pattern = @"(大域|局所)?：?(整数|真理値|実数|文字|文字列)：(\w+)(?:←(.+))?";

        // マッピングする引数の型
        private static Dictionary<string, string> TypeMap = new Dictionary<string, string>
        {
            { "整数", "int" },
            { "真理値", "bool" },
            { "実数", "double" },
            { "文字", "char" },
            { "文字列", "string" },
        };

        public static string convert(string input = "整数：A←1",int level = 0)
        {
            MatchCollection a = Regex.Matches(input, pattern);

            string result = Regex.Replace(input, pattern, match =>
            {
                string globalType = match.Groups[1].Value.Contains("大域")?"public":"private";
                string typeName = match.Groups[2].Value;
                string varName = match.Groups[3].Value;
                string initValue = match.Groups[4].Value;

                if (String.IsNullOrEmpty(initValue))
                {
                    return $"{TypeMap[typeName]} {varName};";
                }
                else
                {
                    
                    switch (typeName)
                    {
                        case "文字":
                            initValue = initValue.Replace("\"", "\'");
                            break;
                    }
                    if (level == 0)
                    {
                        return $"{globalType} {TypeMap[typeName]} {varName} = {initValue};";
                    }
                    else
                    {
                        return $"{TypeMap[typeName]} {varName} = {initValue};";
                    }

                    
                }
            });


            return result;
        }
    }
}
