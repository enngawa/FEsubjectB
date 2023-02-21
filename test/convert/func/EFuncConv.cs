using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using test.convert.type;

namespace test.convert.func
{
    internal class FuncConv
    {
        public static string pattern = @"○(\w+)\（((\w+)：(\w+)(，\w+：\w+)*)*）";

        public static string convert(string input = "○関数A（整数：あ，真理値：B，実数：C，文字：D，文字列：E）")
        {
            string result = Regex.Replace(input, pattern, match =>
            {
                string methodName = match.Groups[1].Value;
                string args = match.Groups[2].Value;
                string[] argsList = args.Split('，');

                if(argsList.Length > 1)
                {
					string arguments = string.Join(",", argsList.Select(x =>
					{
						string[] pair = x.Split('：');
						return $"{convTypes.TypeMap[pair[0]]} {pair[1]}";
					}));
					return $"public void {methodName}({arguments}){{";
                }
                else
                {
                    return $"public void {methodName}(){{";
                }
                
            });

            return result;
        }
    }
}
