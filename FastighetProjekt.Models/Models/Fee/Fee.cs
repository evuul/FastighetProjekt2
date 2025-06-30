using System.ComponentModel.DataAnnotations;

namespace FastighetProjekt.Models.Models.Fee
{
    internal class Fee
    {
        public int FeeId { get; set; }

        [Required]
        public DateTime Month { get; set; }

        public DateTime DueDate { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public bool IsPaid = false;

        // Navigation property
        [Required]
        public int ApartmentId { get; set; }

        [Required]
        public Apartment.Apartment Apartment { get; set; }
    }
}
