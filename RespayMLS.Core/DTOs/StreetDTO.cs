using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RespayMLS.Core.DTOs
{
    public class StreetDTO
    {
        public int StreetId { get; set; }

        public string StreetName { get; set; }

        public int StreetNumber { get; set; }

        public int EstateId { get; set; }
    }
}
