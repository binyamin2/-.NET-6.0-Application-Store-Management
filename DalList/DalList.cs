using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;
namespace Dal;

sealed public class DalList : IDal
{
    public IProduct Product => new DalProduct();
    public IOrderItem OrderItem=> new DalOrderItem();
    public IOrder Order => new DalOrder();

}
