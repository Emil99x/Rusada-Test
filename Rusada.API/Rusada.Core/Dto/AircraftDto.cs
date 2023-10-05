namespace Rusada.Core.Dto
{
    public class AircraftDto
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Registration { get; set; }
        public string Location { get; set; }
        public string? ImagePath { get; set; }
        public DateTime DateTime { get; set; }

        public AircraftImageDto AircraftImageDto { get; set; }
    }

    public class AircraftImageDto
    {
        public int Id { get; set; }
        public string Base64Logo { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public Guid Key { get; set; }
    }
}