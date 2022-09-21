using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RespayMLS.Data
{
    public class RespayDbContext : DbContext
    {
        public RespayDbContext(DbContextOptions<RespayDbContext> options): base(options)
        {

        }
    }
}
