using System;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests.Tests
{
    public static class AssertState
    {
        public static void Equal<T>(T expected, T actual, string message = null)
        {
            if ((typeof(T).IsValueType || expected != null) && !expected.Equals(actual))
                throw new AssertionException<T>(expected, actual, message);
        }

        public static void Equal(string expected, string actual, bool caseSensitive, string message = null)
        {
            if (expected != null && !expected.Equals(actual, caseSensitive ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase))
                throw new AssertionException<string>(expected, actual, message);
        }

        public static void NotNull<T>(T item, string message = null) where T : class
        {
            if (item == null)
                throw new AssertionException<string>("not null", "null", message);
        }
    }
}