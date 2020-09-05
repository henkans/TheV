using System;

namespace TheV.Lib.Helpers
{
    public static class TheVExtensions
    {

        public static void  WordCount(this String str)
        {
            var temp = str.Split(new char[] { ' ', '.', '?' },
                StringSplitOptions.RemoveEmptyEntries).Length;
        }

    }
}
