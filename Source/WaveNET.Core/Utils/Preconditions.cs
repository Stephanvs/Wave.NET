using System;

namespace WaveNET.Core.Utils
{
    public static class Preconditions
    {
        public static void CheckNotNull(object obj, string msg = "")
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj", msg ?? "Argument cannot be null");
            }
        }
    }
}