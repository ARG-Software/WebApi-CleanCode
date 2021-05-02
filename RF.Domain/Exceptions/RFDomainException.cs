namespace RF.Domain.Exceptions
{
    using System;

    public sealed class RFDomainException : Exception
    {
        public RFDomainException()
        {
        }

        public RFDomainException(string message)
            : base(message)
        {
        }

        public RFDomainException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}