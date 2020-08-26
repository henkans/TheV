using System;
using System.Collections.Generic;
using System.Text;

namespace TheV.Helpers
{
    public static class OutputExtensions
    {

        public static void  WordCount(this String str)
        {
            var temp = str.Split(new char[] { ' ', '.', '?' },
                StringSplitOptions.RemoveEmptyEntries).Length;
        }

    }
}
