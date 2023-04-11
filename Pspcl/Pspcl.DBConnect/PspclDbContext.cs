using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Pspcl.DBConnect
{
    public class PspclDbContext : DbContext
    {
        public PspclDbContext(DbContextOptions<PspclDbContext> options) : base(options)
        {

        }
    }
}
