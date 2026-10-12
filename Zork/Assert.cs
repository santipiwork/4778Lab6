using System;
using System.Diagnostics;

namespace Zork
{
    public static class Assert
    {
        [Conditional("DEBUG")]
        public static void IsTrue(bool expression, string message = "")
        {
            if (!expression)
            {
                throw new Exception(message);
            }
        }
    }
}
