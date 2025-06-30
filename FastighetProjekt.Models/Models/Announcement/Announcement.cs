namespace FastighetProjekt.Models.Models.Announcement;

public class Announcement
{
    public int NoticeId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime PublishedDate { get; set; } = DateTime.Now;
    public string TargetGroup { get; set; } = string.Empty;

    public string? AttachmentUrl { get; set; }
}