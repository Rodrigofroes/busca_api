namespace BackAppPersonal.Domain.Entities
{
    public class OpenStreetMapResponse
    {
        public long PlaceId { get; set; }
        public string Licence { get; set; }
        public string OsmType { get; set; }
        public long OsmId { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Class { get; set; }
        public string Type { get; set; }
        public int PlaceRank { get; set; }
        public double Importance { get; set; }
        public string Addresstype { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public List<string> BoundingBox { get; set; }
    }
}
