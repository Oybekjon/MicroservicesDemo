using System;
using System.Runtime.Serialization;
namespace MicroservicesDemo.Errors
{
    [Serializable]
    public class RemoteServerProcessingException : FatalException
    {
        public RemoteServerProcessingException() { }
        public RemoteServerProcessingException(string message) : base(message) { }
        public RemoteServerProcessingException(string message, Exception inner) : base(message, inner) { }
        protected RemoteServerProcessingException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}