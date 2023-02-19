using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace test.convert.func
{
    internal class FuncConv
    {
        public static string pattern = @"○(\w+)\（((\w+)：(\w+)(，\w+：\w+)*)*）";

        // マッピングする引数の型
        private static Dictionary<string, string> TypeMap = new Dictionary<string, string>
        {
            { "整数", "int" },
            { "真理値", "bool" },
            { "実数", "double" },
            { "文字", "char" },
            { "文字列", "string" },
        };

        public static string convert(string input = "○関数A（整数：あ，真理値：B，実数：C，文字：D，文字列：E）")
        {
            string result = Regex.Replace(input, pattern, match =>
            {
                string methodName = match.Groups[1].Value;
                string args = match.Groups[2].Value;
                string[] argsList = args.Split('，');
                string arguments = string.Join(",", argsList.Select(x =>
                {
                    string[] pair = x.Split('：');
                    return $"{TypeMap[pair[0]]} {pair[1]}";
                }));
                return $"public void {methodName}({arguments}){{";
            });

            return result;
        }
    }
}
