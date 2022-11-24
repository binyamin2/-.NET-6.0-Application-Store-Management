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

[Serializable]
public class NotInStockException : Exception
{
    public NotInStockException() : base() { }
    public NotInStockException(string message) : base(message) { }
    public NotInStockException(string message, Exception inner) : base(message, inner) { }
    protected NotInStockException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}

public class WrongCartDeteilsException : Exception
{
    public WrongCartDeteilsException() : base() { }
    public WrongCartDeteilsException(string message) : base(message) { }
    public WrongCartDeteilsException(string message, Exception inner) : base(message, inner) { }
    protected WrongCartDeteilsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}

