
using Pspcl.Core.Domain;
using Pspcl.Core;
using Pspcl.Services;
using Microsoft.EntityFrameworkCore;

namespace Pspcl.Web.MapperService
{
    public class StockService:IStockService
    {
        private readonly DbContext _dbcontext;
        public StockService(DbContext context)
        {
            _dbcontext = context;
                        
        }

        
    }
}
