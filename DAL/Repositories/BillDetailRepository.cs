﻿using Dev69Restaurant.DAL.Infrastructure;
using Dev69Restaurant.DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev69Restaurant.DAL.Repositories
{
    public interface IBillDetailRepository : IRepository<BillDetail>
    {

    }
    public class BillDetailRepository : RepositoryBase<BillDetail>, IBillDetailRepository
    {
        public BillDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
