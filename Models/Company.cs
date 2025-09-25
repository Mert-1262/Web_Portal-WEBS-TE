using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_Portal.Models
{
    [Table("Company_List")] // Veritabanındaki tablo adı
    public class Company
    {
        [Key]
        public int Company_ID { get; set; } // Şirket ID

        [Required(ErrorMessage = "Şirket adı zorunludur.")]
        [StringLength(100)]
        public string company_name { get; set; } = string.Empty; // 🔥 Varsayılan değer atandı

        [StringLength(255)]
        public string? address { get; set; } // 🔥 Nullable olarak işaretlendi (opsiyonel)
    }
}
