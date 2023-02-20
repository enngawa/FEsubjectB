using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace test.convert.func
{
    internal class EFuncConv
    {
        public static string pattern = @"^((?<A>.*)←)?(?<B>[\w_]+)（(?<C>[\s\S]*)）$";

        public static string convert(string input = "AAA←関数名（true，1，34，\"A\"）")
        {
			string result = Regex.Replace(input, pattern, match =>
			{
				string varName = match.Groups["A"].Value;
				string functionName = match.Groups["B"].Value;
				string argsString = match.Groups["C"].Value.Replace("，", ", ");

				if(String.IsNullOrEmpty(varName)){
					return $"{functionName}({argsString});";
				}
				else
				{
					return $"{varName} = {functionName}({argsString});";
				}
			});

			return result;
		}
    }
}
