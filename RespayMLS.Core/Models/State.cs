using System.Collections.Generic;

namespace RespayMLS.Core.Models
{
    public class State
    {
        public int StateId { get; set; }

        public string StateName { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public ICollection<City> Cities { get; set; } = new List<City>();
    }
}
