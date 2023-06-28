using System;

namespace com.mobiquity.packer
{
    // Implementation class to handle library level API exceptions

    public class APIException : Exception
    {
        public APIException(string message) : base(message)
        {
        }

    }
}