using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.convert.type
{
	public static class relTypes
	{
		// マッピングする引数の型
		public static Dictionary<string, string> TypeMap = new Dictionary<string, string>
		{
			{ "より大きい", ">" },
			{ "以上", ">=" },
			{ "より小さい", "<" },
			{ "以下", "<=" },
			{ "等しい", "==" },
			{"等しくない", "!=" }
		};
	}
}
