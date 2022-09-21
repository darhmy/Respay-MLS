using System.Collections.Generic;

namespace RespayMLS.Core.Models
{
    public class Country
    {
        public int CountryId { get; set; }

        public string CountryName { get; set; }


        public ICollection<State> States { get; set; } = new List<State>();
    }
}
