﻿using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;

public interface IDal
{
    Product Product { get; }
    Order Order { get; }

    OrderItem Item { get; } 
}