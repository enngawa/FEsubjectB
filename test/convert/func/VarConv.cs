using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using test.convert.type;

namespace test.convert.func
{
    internal class VarConv
    {
        public static string pattern = $"^((?<A>大域|局所)：)?(?<B>{String.Join("|",convTypes.TypeMap.Keys.ToList())})：(?<C>[^←]+)(←(?<D>.*))?$";

        public static string convert(string input = "整数：A←1",int level = 0)
        {
            string result = Regex.Replace(input, pattern, match =>
            {
                string globalType = match.Groups["A"].Value.Contains("大域")?"public":"private";
                string typeName = match.Groups["B"].Value;
                string[] varName = match.Groups["C"].Value.Split('，');
                string initValue = match.Groups["D"].Value;

                if (String.IsNullOrEmpty(initValue))
                {
					if(level == 0)
					{
						return $"{globalType} {convTypes.TypeMap[typeName]} {String.Join(", ", varName)};";
					}
					else
					{
						return $"{convTypes.TypeMap[typeName]} {String.Join(", ", varName)};";
					}
                }
                else if(varName.Length == 1)
                {                    
                    switch (typeName)
                    {
                        case "文字":
                            initValue = initValue.Replace("\"", "\'");
                            break;
                    }

                    if (level == 0)
                    {
                        return $"{globalType} {convTypes.TypeMap[typeName]} {varName[0]} = {initValue};";
                    }
                    else
                    {
                        return $"{convTypes.TypeMap[typeName]} {varName[0]} = {initValue};";
                    }
                }
                else
                {
					return $"{convTypes.TypeMap[typeName]} {String.Join(", ", varName)}　= {initValue};";
				}
            });

            return result;
        }
    }
}
