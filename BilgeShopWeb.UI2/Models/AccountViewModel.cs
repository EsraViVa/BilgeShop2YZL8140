using System.ComponentModel.DataAnnotations;

namespace BilgeShopWeb.UI2.Models
{
    public class AccountViewModel
    {
        [Display(Name = "Ad")]
        [Required(ErrorMessage = "Bu alanı doldurmak zorunludur.")]
        public string FirstName { get; set; }

        [Display(Name = "Soyad")]
        [Required(ErrorMessage = "Bu alanı doldurmak zorunludur.")]
        public string LastName { get; set; }

        [Display(Name = "Eposta")]
        [Required(ErrorMessage = "Bu alanı dıoldurmak zorunludur.")]

        public string  Email { get; set; }

        [Display(Name = "Eposta (tekrar)")]
        [Required(ErrorMessage = "Bu alanı doldurmak zorunludur.")]
        [Compare(nameof(Email) , ErrorMessage = "Email adresleri eşleşmiyor.")]
        public string EmailConfirm { get; set; }
    }
    
}
