using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;

sealed public class Bl : IBl
{
    public IProduct Product { get; } = new BlImplementation.Product();

    public IOrder Order { get; } = new BlImplementation.Order();

    public ICart Cart { get; } = new BlImplementation.Cart();

    
}
