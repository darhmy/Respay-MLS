using RespayMLS.Core.Models;
using System.Collections.Generic;

namespace RespayMLS.Core.DTOs
{
    public class CityDTO
    {
        public int CityId { get; set; }
        public string CityName { get; set; }

        public int StateId { get; set; }

        //public IEnumerable<Area> Areas { get; set; }
    }
}
