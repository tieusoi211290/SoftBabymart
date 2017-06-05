using SoftBabymartVn.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SoftBabymartVn.Repository
{
    public interface IChannel_ProductPepository : IRepository<soft_Channel_Product_Price>
    {
        //IEnumerable<Customer> GetRecentCustomers();
        //Customer GetByName(string customerName);
    }
    public class Channel_ProductPepository : BaseRepository<soft_Channel_Product_Price>, IChannel_ProductPepository
    {
        private readonly babymart_vnEntities _dbContext;

        public Channel_ProductPepository(babymart_vnEntities dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        
    }

    //public class Channel_ProductPepository : EFRepository<soft_Channel_Product>
    //{
    //    private readonly babymart_vnEntities _db;
    //    public Channel_ProductPepository(babymart_vnEntities db) : base(db)
    //    {
    //        _db = db;
    //    }
    //    public List<soft_Channel_Product> GetList()
    //    {
    //        return _db.soft_Channel_Product.ToList();
    //    }
    //}
}