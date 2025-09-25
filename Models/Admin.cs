using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_Portal.Models
{
    [Table("Admin_List")]
    public class Admin
    {
        [Key]
        public int Admin_ID { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public int Company_ID { get; set; }

        public DateTime Added_Date { get; set; }
    }
}
