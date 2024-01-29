using Rocky_DataAccess.Repository.IRepository;
using Rocky_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocky_DataAccess.Repository
{
    public class ApplicationRepository : Repository<ApplicationType>, IApplicationTypeRepository
    {
        private readonly RockyDbContext _db;

        public ApplicationRepository(RockyDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ApplicationType obj)
        {
            var objFromDb = _db.Category.FirstOrDefault(u=>u.Id==obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = obj.Name;
               
            }
        }
    }
}
