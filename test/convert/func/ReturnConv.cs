using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using test.convert.type;

namespace test.convert.func
{
    internal class ReturnConv
    {
        public static string pattern = @"return(?<A>[\s\S]*)";

        public static string convert(ref List<string> listRow, string input = "return A + B")
        {
			string varName = Regex.Replace(input, pattern, match =>
			{
				return match.Groups["A"].Value;			
			});
			string valtypes = String.Join("|", convTypes.TypeMap.Select(x => { return x.Value; }));
			string valType = "";

			string csharppattern = $"(public|private)\\s(?<A>{valtypes})\\s([\\s\\S]*)";

			for(int index = listRow.Count - 1; index >= 0; index--)
			{
				if(Regex.IsMatch(listRow[index], csharppattern)){					
					valType = Regex.Replace(listRow[index], csharppattern, match =>
					{
						return match.Groups["A"].Value;
					});
					break;
				}
			}
			for(int index = listRow.Count - 1; index >= 0; index--)
			{
				if(Regex.IsMatch(listRow[index], $"^public ({valtypes}|void) .*\\(.*\\){{$")){
					listRow[index] = listRow[index].Replace("void", valType);
					break;
				}
			}
			return $"return {varName};";
		}
    }
}
