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

//[Serializable]
//public class AllreadyExistException : Exception
//{
//    public AllreadyExistException() : base() { }
//    public AllreadyExistException(string message) : base(message) { }
//    public AllreadyExistException(string message, Exception inner) : base(message, inner) { }
//    protected AllreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }

//}


