namespace Rusada.Domain.BaseEntities;

public class AircraftImage : BaseEntityWithAudit
{
    public int AircraftId { get; set; }
    public string Base64Logo { get; set; }
    public string ContentType { get; set; }
    public string FileName { get; set; }
    public string Path { get; set; }
    public Guid Key { get; set; }

    public virtual Aircraft Aircraft { get; set; }
}