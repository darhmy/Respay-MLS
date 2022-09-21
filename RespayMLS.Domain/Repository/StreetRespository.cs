using RespayMLS.Core.Interface;
using RespayMLS.Core.Models;
using RespayMLS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RespayMLS.Domain.Repository
{
    public class StreetRespository : Repository<Street>, IStreetRepository
    {
        public StreetRespository(RespayDbContext respayDbContext) : base(respayDbContext)
        {
        }
    }
}
