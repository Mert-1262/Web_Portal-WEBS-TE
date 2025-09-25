using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_Portal.Models
{
    public class Employee
    {
        [Key]
        public int Employee_ID { get; set; }

        [Required(ErrorMessage = "Ad Soyad boş olamaz.")]
        [StringLength(50, ErrorMessage = "Ad Soyad en fazla 50 karakter olabilir.")]
        public string Full_Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email boş olamaz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email girin.")]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefon numarası boş olamaz.")]
        [RegularExpression(@"^5\d{9}$", ErrorMessage = "Telefon numarası 5 ile başlamalı ve 10 haneli olmalıdır.")]
        public string Phone { get; set; } = string.Empty;


        public string Address { get; set; } = string.Empty;

        [ForeignKey("Company")]
        public int Company_ID { get; set; }  // 🚨 EKLENDİ: Şirket ID'si bağlantılı hale getirildi

        public Company? Company { get; set; }  // 🚀 Şirket ile ilişkilendirildi
    }
}
