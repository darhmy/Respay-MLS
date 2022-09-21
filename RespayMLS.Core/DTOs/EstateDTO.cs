using RespayMLS.Core.Models;
using System.Collections.Generic;

namespace RespayMLS.Core.DTOs
{
    public class EstateDTO
    {
        public int EstateId { get; set; }
        public string EstateName { get; set; }

        public int AreaId { get; set; }

        //public IEnumerable<Street> Streets { get; set; }

    }
}
