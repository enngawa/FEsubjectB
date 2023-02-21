using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using test.convert.type;

namespace test.convert.func
{
    internal class EndConv
    {
        public static string pattern = "endwhile";

        public static string convert(string input = "endwhile")
        {
            return "}"; 
        }
    }
}
