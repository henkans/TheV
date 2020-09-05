using System;
using System.Collections.Generic;
using System.Text;

namespace TheV.Lib.Helpers
{

    public class CheckerException : Exception
    {
        public CheckerException()
        {
        }

        public CheckerException(string message)
            : base(message)
        {
        }

        public CheckerException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

}
