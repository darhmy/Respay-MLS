using System.Collections.Generic;

namespace RespayMLS.Core.Models
{
    public class Area
    {
        public int AreaId { get; set; }

        public string AreaName { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public ICollection<Estate> Estates { get; set; } = new List<Estate>();
    }
}
