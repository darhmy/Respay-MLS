using RespayMLS.Core.Models;
using System.Collections.Generic;

namespace RespayMLS.Core.DTOs
{
    public class CountryDTO
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }

        //public IEnumerable<State> States { get; set; }
    }
}
