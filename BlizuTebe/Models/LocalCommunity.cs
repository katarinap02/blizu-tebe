using NetTopologySuite.Geometries;
using System.Drawing;

namespace BlizuTebe.Models
{
    public class LocalCommunity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public Geometry Boundary { get; set; }
        public NetTopologySuite.Geometries.Point CenterPoint { get; set; }
        public long? PresidentId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Facebook { get; set; }

        public LocalCommunity() { }

        public LocalCommunity(long id, string name, string city, Geometry boundary, NetTopologySuite.Geometries.Point centerPoint, long? presidentId, string phoneNumber, string facebook)
        {
            Id = id;
            Name = name;
            City = city;
            Boundary = boundary;
            CenterPoint = centerPoint;
            PresidentId = presidentId;
            PhoneNumber = phoneNumber;
            Facebook = facebook;
        }
    }
}
