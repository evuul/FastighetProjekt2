namespace FastighetProjekt.Models.Models.Property;

public class Property
{
    public int PropertyId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Adress { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    
    public ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();
}