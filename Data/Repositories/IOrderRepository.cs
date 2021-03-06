﻿using System;
using System.Collections.Generic;
using Model;

namespace Data.Repositories
{
    public interface IOrderRepository:IRepository<Order,int>
    {
        IEnumerable<Order> GetAllByEmployeeId(int id);
        IEnumerable<Order> GetAllByEmployeeId(int id,DateTime afterDate);
    }
}
