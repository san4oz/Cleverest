using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cleverest
{
    public static class StringExtensions
    {
        public static bool IsEmpty(this string src)
        {
            return string.IsNullOrEmpty(src);
        }
    }
}
