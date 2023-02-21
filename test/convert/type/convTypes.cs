using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.convert.type
{
	public static class convTypes
	{
		// マッピングする引数の型
		public static Dictionary<string, string> TypeMap = new Dictionary<string, string>
		{
			{ "整数", "int" },
			{ "真理値", "bool" },
			{ "実数", "double" },
			{ "文字", "char" },
			{ "文字列", "string" },
		};
	}
}
