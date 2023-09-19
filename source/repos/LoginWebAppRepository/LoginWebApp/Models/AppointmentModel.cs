using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LoginWebApp.Models
{
    public class AppointmentModel
    {
        public int Id { get; set; }
        [Required (ErrorMessage = "Title is required")]
        [StringLength(100)]
        public string Title { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Date is required")]
        [Display(Name = "Appointment Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [StringLength(200, ErrorMessage = "Location must not exceed 200 characters.")]
        public string? Location { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
