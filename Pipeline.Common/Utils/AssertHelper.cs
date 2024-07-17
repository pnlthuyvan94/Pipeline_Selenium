using System;

namespace Pipeline.Common.Utils
{
    /// <summary>
    /// Using AssertHelper to verify and do not want to END the test after facing the error!
    /// </summary>
    public static class AssertHelper
    {

        public static bool AreEqual<T>(T expected, T actual)
        {
            var result = Compare(expected, actual);
            if (!result)
                return false;
            return true;
        }

        public static bool AreNotEqual<T>(T expected, T actual)
        {
            var result = Compare(expected, actual);
            if (result)
                return false;
            return true;
        }

        public static bool IsNull(object value)
        {
            if (value != null)
                return false;
            return true;
        }
   
        public static bool IsNotNull(object value)
        {
            if (value is null)
                return false;
            return true;
        }

        public static bool AreDateTimesEqual(DateTime? expectedDate, DateTime? actualDate, int deltaSeconds)
        {
            throw new NotImplementedException();
        }

        internal static bool Compare<T>(T obj, T another)
        {
            if ((obj == null) || (another == null)) return false;
            if (obj.GetType() != another.GetType()) return false;

            if (obj is string || !obj.GetType().IsClass)
            {
                return obj.Equals(another);
            }
            else // do not support if obj is a class
            {
                throw new NotSupportedException("AssertHelper - Class type is not supported.");
            }
        }

    }
}
