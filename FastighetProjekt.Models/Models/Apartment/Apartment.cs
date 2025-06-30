namespace FastighetProjekt.Models.Models.Apartment;

using System.ComponentModel.DataAnnotations;

public class Apartment
{
    public int ApartmentId { get; set; }

    [Required]
    [StringLength(10)]
    public string ApartmentNumber { get; set; } = string.Empty;

    [Range(0, 100)]
    public int Floor { get; set; }

    [Range(1, 10)]
    public int Rooms { get; set; }

    [Range(10, 1000)]
    public double Area { get; set; }

    [Range(0, int.MaxValue)]
    public int Rent { get; set; }

    // Navigation property
    public int PropertyId { get; set; }
    public Property.Property Property { get; set; } = null!;
}