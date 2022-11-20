using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi;

public interface IB1
{
    public IProduct Product { get; }
    public IOrder Order { get; }
    public ICart Cart { get; }

}
