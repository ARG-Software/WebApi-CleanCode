namespace RF.Infrastructure.Parser.Exceptions
{
    using System;

    public sealed class RFParserException : Exception
    {
        public RFParserException(string message)
            : base(message)
        {
        }

        public RFParserException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}