using System;
using System.Runtime.Serialization;

namespace RadicalGeek.Testing.Smoke.ConfigurationTests
{
    [Serializable]
    public class AssertionException : Exception
    {
        public AssertionException(string message)
            : base(message)
        {
        }
    }

    [Serializable]
    public class AssertionException<T> : AssertionException
    {
        private readonly T expected;
        private readonly T actual;
        private readonly bool messageSpecified;
        private readonly bool valuesSpecified;

        public AssertionException(T expected, T actual, string message = null)
            : base(message)
        {
            this.expected = expected;
            this.actual = actual;
            messageSpecified = message != null;
            valuesSpecified = true;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Expected", expected.ToString());
            info.AddValue("Actual", actual.ToString());
        }

        public override string Message
        {
            get
            {
                if (valuesSpecified)
                {
                    string message = string.Empty;
                    if (messageSpecified)
                        message = string.Format(" ({0})", base.Message);
                    return string.Format("Expected [{0}] Actual [{1}]{2}", expected, actual, message);
                }
                return base.Message;
            }
        }
    }
}