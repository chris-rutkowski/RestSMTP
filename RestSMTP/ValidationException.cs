using System.Runtime.Serialization;

namespace RestSMTP
{
    [Serializable()]
    public class ValidationException : Exception, ISerializable
    {
    }
}
