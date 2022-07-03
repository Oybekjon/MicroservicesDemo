using System;
using System.Runtime.Serialization;
namespace MicroservicesDemo.Errors
{
    [Serializable]
    public class MailSendingException : ErrorException
    {
        public MailSendingException() { }
        public MailSendingException(String message) : base(message) { }
        public MailSendingException(String message, Exception inner) : base(message, inner) { }
        protected MailSendingException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}