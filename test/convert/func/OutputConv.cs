using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace test.convert.func
{
    internal class OutputConv
    {
        public static string pattern = @"((?:[^\s]+と)*[^\s]+)を出力する";

        public static string convert(string input = "aとbとcとdとeを出力する", int level = 0)
        {
			// 変数名を取得
			List<string> variables = input.Replace("を出力する", "").Split("と").ToList();
            string output = "Console.AppendText($\"" + String.Join("， ", variables.Select(s => { return $"{s}：{{{s}}}"; })) + "\\r\\n\");";

			return output;
        }
    }
}
