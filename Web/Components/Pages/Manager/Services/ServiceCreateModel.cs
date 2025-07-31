namespace Web.Components.Pages.Manager.Services
{
    using System.ComponentModel.DataAnnotations;

    public class ServiceCreateModel
    {
        [Required(ErrorMessage = "Service name is required")]
        public string ServiceName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        public string Duration { get; set; }
    }
} 