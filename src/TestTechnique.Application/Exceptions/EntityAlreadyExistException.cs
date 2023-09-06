using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TestTechnique.Application.Exceptions;

internal class EntityAlreadyExistException : Exception
{
    public EntityAlreadyExistException()
    {
    }

    public EntityAlreadyExistException(string message) : base(message)
    {
    }

    public EntityAlreadyExistException(string message, Exception innerException) : base(message, innerException) 
    {
    }

    public EntityAlreadyExistException(SerializationInfo serializationInfo, StreamingContext streamingContext) :
        base(serializationInfo, streamingContext)
    {
    }
}
