namespace FastighetProjekt.Models.Models.MaintenanceRequest;

public enum MaintenanceStatus
{
    Submitted,
    InProgress,
    Completed,
    Rejected
}

public class MaintenanceRequest
{
    public int MaintenanceRequestId { get; set; }

    public int ApartmentId { get; set; }
    public Apartment.Apartment Apartment { get; set; } = null!;

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public MaintenanceStatus Status { get; set; } = MaintenanceStatus.Submitted;

    // Möjlighet att bifoga t.ex. foto eller PDF relaterad till ärendet
    public string? AttachmentUrl { get; set; }
}