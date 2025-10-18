using NetTopologySuite.Geometries;
using System.Drawing;

namespace BlizuTebe.Models
{
    public class LocalCommunity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public Geometry Boundary { get; set; }
        public NetTopologySuite.Geometries.Point CenterPoint { get; set; }
        public int? PresidentId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Facebook { get; set; }

        public LocalCommunity() { }

        public LocalCommunity(int id, string name, string city, Geometry boundary, NetTopologySuite.Geometries.Point centerPoint, int? presidentId, string phoneNumber, string facebook)
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
