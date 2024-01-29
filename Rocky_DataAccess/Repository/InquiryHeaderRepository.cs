using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class InquiryHeaderRepository : Repository<InquiryHeader>, IInquiryHeaderRepository
    {
        private readonly RockyDbContext _db;

        public InquiryHeaderRepository(RockyDbContext db) : base(db)
        {
            _db = db;
        }

        void IInquiryHeaderRepository.Update(InquiryHeader obj)
        {
            _db.InquiryHeaders.Update(obj);
        }
    }
}
