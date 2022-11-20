using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using Dal;
using DalApi;

namespace BlImplementation;

internal class Cart : BlApi.ICart
{
    private IDal Dal = new DalList();

}
