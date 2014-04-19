using System;

namespace WaveNET.Core.Utils
{
    public static class Preconditions
    {
        public static void CheckNotNull(object obj, string msg = "")
        {
            if (obj == null)
                throw new ArgumentNullException("obj", msg ?? "Argument cannot be null");
        }

        public static void CheckNotNullOrEmpty(string val, string msg = "")
        {
            if (string.IsNullOrEmpty(val))
                throw new ArgumentNullException("val", msg ?? "Argument cannot be null or empty");
        }

        public static void CheckArgument(bool condition, string errorMessage)
        {
            if (!condition)
            {
                throw new ArgumentException(errorMessage);
            }
        }

        public static void CheckArgument(bool condition, string messageFormat, params object[] errorMessages)
        {
            if (!condition)
            {
                throw new ArgumentException(string.Format(messageFormat, errorMessages));
            }
        }
    }
}