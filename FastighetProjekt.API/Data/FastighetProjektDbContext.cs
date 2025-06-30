using FastighetProjekt.Models.Models.Announcement;
using FastighetProjekt.Models.Models.Apartment;
using FastighetProjekt.Models.Models.Fee;
using FastighetProjekt.Models.Models.MaintenanceRequest;
using FastighetProjekt.Models.Models.Payment;
using FastighetProjekt.Models.Models.Property;
using FastighetProjekt.Models.Models.User;
using Microsoft.EntityFrameworkCore;

namespace FastighetProjekt.Data;


public class FastighetProjektDbContext : DbContext
{
    public FastighetProjektDbContext(DbContextOptions<FastighetProjektDbContext> options)
        : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<Apartment> Apartments { get; set; }
    public DbSet<Announcement> Announcements { get; set; }
    public DbSet<MaintenanceRequest> MaintenanceRequests { get; set; }
    public DbSet<Fee> Fees { get; set; }
    public DbSet<Payment> Payments { get; set; }
}