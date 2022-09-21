using System.Collections.Generic;

namespace RespayMLS.Core.Models
{
    public class Estate
    {
        public int EstateId { get; set; }

        public string EstateName { get; set; }

        public int AreaId { get; set; }
        public Area Area { get; set; }

        public ICollection<Street> Streets { get; set; } = new List<Street>();

    }
}
