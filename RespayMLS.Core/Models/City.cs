using System.Collections.Generic;

namespace RespayMLS.Core.Models
{
    public class City
    {
        public int CityId { get; set; }

        public string CityName { get; set; }

        public int StateId { get; set; }
        public State State { get; set; }

        public ICollection<Area> Areas { get; set; } = new List<Area>();
    }
}
