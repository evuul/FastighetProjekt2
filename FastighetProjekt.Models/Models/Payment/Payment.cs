using System.ComponentModel.DataAnnotations;

namespace FastighetProjekt.Models.Models.Payment
{
    internal class Payment
    {
        public int PaymentId { get; set; }

        [Required]
        public DateTime PaidDate { get; set; }

        [Required]
        public int Amount { get; set; }

        // Navigation property
        [Required]
        public int FeeId { get; set; }

        [Required]
        public Fee.Fee Fee { get; set; }
    }
}
