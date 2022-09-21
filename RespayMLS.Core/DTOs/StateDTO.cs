using RespayMLS.Core.Models;
using System.Collections.Generic;

namespace RespayMLS.Core.DTOs
{
    public class StateDTO
    {
        public int StateId { get; set; }
        public string StateName { get; set; }

        public int CountryId { get; set; }

       //public IEnumerable<City> Cities { get; set; }
    }
}
