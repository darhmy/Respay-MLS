using RespayMLS.Core.Models;
using System.Collections.Generic;

namespace RespayMLS.Core.DTOs
{
    public class AreaDTO
    {
        public int AreaId { get; set; }
        public string AreaName { get; set; }

        public int CityId { get; set; }

       // public IEnumerable<Estate> Estates { get; set; } 
    }
}
