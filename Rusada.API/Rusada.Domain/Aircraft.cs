using Rusada.Domain.BaseEntities;

namespace Rusada.Domain
{
    public class Aircraft : BaseEntityWithSoftDelete
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Registration { get; set; }
        public string Location { get; set; }
        public DateTime DateTime { get; set; }
    }
}
