using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using test.convert.func;
using static System.Net.Mime.MediaTypeNames;

namespace test.convert
{
    public class ConvertManagement {

        private List<string> textRows { get; set; }

        private Dictionary<string, string> replacements = new Dictionary<string, string>
        {
            { "，", "," },
            { "＋", "+" },
            { "－", "-" },
            { "÷", "/" },
            { "×", "*" },
            { "［", "[" },
            { "］", "]" },
            { "｛", "{" },
            { "｝", "}" },
            { "（", "(" },
            { "）", ")" }
        };

        public ConvertManagement(List<string> text) 
        {
            this.textRows = text.Select(x => { return Regex.Replace(x, @"\s", ""); }).ToList();
        }
        public string Convert()
        {
            List<string> ret = new List<string>();
            int level = 0;


            foreach(string row in textRows)
            {
                switch (row)
                {
					//while（aが0以上）
					case var x when Regex.IsMatch(row, WhileConv.pattern):
						ret.Add(new string(' ', 4 * (level)) + replacements.Aggregate(WhileConv.convert(row), (current, replacement) => current.Replace(replacement.Key, replacement.Value)));
                        level++;
						break;

					//endwhile
					case var x when Regex.IsMatch(row, EndConv.pattern):
                        level--;
						ret.Add(new string(' ', 4 * (level)) + EndConv.convert(row));
						break;

					//○関数A（整数：あ，真理値：B，実数：C，文字：D，文字列：E）
					case var x when Regex.IsMatch(row, FuncConv.pattern):
						for(; level > 0; level--)
						{
							ret.Add(new string(' ', 4 * (level - 1)) + "}");
						}
						ret.Add(FuncConv.convert(row));
                        level++;
                        break;
					//aaa ← 関数A（10，true，10.24，"A"，"ABC"）
					case var x when Regex.IsMatch(row, EFuncConv.pattern):
						ret.Add(new string(' ', 4 * (level)) + replacements.Aggregate(EFuncConv.convert(row), (current, replacement) => current.Replace(replacement.Key, replacement.Value)));
						break;

					//整数：A ← 10
					case var x when Regex.IsMatch(row, VarConv.pattern):
                        ret.Add(new string(' ', 4 * (level)) + replacements.Aggregate(VarConv.convert(row,level), (current, replacement) => current.Replace(replacement.Key, replacement.Value)));
                        break;

					//A ← B + 10
					case var x when Regex.IsMatch(row, AssignConv.pattern):
						ret.Add(new string(' ', 4 * (level)) + replacements.Aggregate(AssignConv.convert(row), (current, replacement) => current.Replace(replacement.Key, replacement.Value)));
						break;

					//AとBを出力する
					case var x when Regex.IsMatch(row, OutputConv.pattern):
						ret.Add(new string(' ', 4 * (level)) + replacements.Aggregate(OutputConv.convert(row), (current, replacement) => current.Replace(replacement.Key, replacement.Value)));
						break;

					//return x + 5
					case var x when Regex.IsMatch(row, ReturnConv.pattern):
						ret.Add(new string(' ', 4 * (level)) + replacements.Aggregate(ReturnConv.convert(ref ret, row), (current, replacement) => current.Replace(replacement.Key, replacement.Value)));
						break;					

					default:
                        ret.Add(row);
                        break;
                }
            }

            for (; level > 0; level--)
            {
                ret.Add(new string(' ', 4 * (level - 1)) + "}");
            }

            return string.Join(Environment.NewLine, ret);
        }
    }
}
