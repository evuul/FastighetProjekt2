using System.ComponentModel.DataAnnotations;

namespace FastighetProjekt.Models.Models.Fee
{
    public class Fee
    {
        public int FeeId { get; set; }

        [Required]
        public DateTime Month { get; set; }

        public DateTime DueDate { get; set; }

        [Required]
        public int Amount { get; set; }
        
        [Required]
        public int ApartmentId { get; set; }

        [Required]
        public Apartment.Apartment Apartment { get; set; }
    }
}