using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace BO;


[Serializable]
public class WorngProductException : Exception
{
    public WorngProductException() : base() { }
    public WorngProductException(string message) : base(message) { }
    public WorngProductException(string message, Exception inner) : base(message, inner) { }
    protected WorngProductException(SerializationInfo info, StreamingContext context) : base(info, context) { }


}



[Serializable]
public class InputUnvalidException : Exception
{
    public InputUnvalidException() : base() { }
    public InputUnvalidException(string message) : base(message) { }
    public InputUnvalidException(string message, Exception inner) : base(message, inner) { }
    protected InputUnvalidException(SerializationInfo info, StreamingContext context) : base(info, context) { }

}

[Serializable]
public class WorngOrderException : Exception
{
    public WorngOrderException() : base() { }
    public WorngOrderException(string message) : base(message) { }
    public WorngOrderException(string message, Exception inner) : base(message, inner) { }
    protected WorngOrderException(SerializationInfo info, StreamingContext context) : base(info, context) { }


}

