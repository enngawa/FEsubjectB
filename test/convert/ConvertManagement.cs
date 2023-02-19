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
                    //○関数A（整数：あ，真理値：B，実数：C，文字：D，文字列：E）
                    case var x when Regex.IsMatch(row, FuncConv.pattern):
                        ret.Add(FuncConv.convert(row));
                        level++;
                        break;

                    //整数：A ← 10
                    case var x when Regex.IsMatch(row, VarConv.pattern):

                        ret.Add(new string(' ', 4 * (level)) + replacements.Aggregate(VarConv.convert(row,level), (current, replacement) => current.Replace(replacement.Key, replacement.Value)));
                        break;

                    //A ← B + 10
                    case var x when Regex.IsMatch(row, AssignConv.pattern):
                        ret.Add(new string(' ', 4 * (level)) + replacements.Aggregate(AssignConv.convert(row), (current, replacement) => current.Replace(replacement.Key, replacement.Value)));
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
