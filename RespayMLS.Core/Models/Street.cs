namespace RespayMLS.Core.Models
{
    public class Street
    {
        public int StreetId { get; set; }

        public string StreetName { get; set; }

        public int StreetNumber { get; set; }


        public int EstateId { get; set; }
        public Estate Estate { get; set; }
    }
}
