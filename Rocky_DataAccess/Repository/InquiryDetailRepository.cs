﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Rocky;
using Rocky_DataAccess.Repository.IRepository;
using Rocky_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rocky_DataAccess.Repository
{
    public class InquiryDetailRepository : Repository<InquiryDetail>, IInquiryDetailRepository
    {
        private readonly RockyDbContext _db;

        public InquiryDetailRepository(RockyDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(InquiryDetail obj)
        {
            _db.InquiryDetail.Update(obj);
        }

      
    }
}
