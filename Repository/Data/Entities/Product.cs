using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Data.Entities
{
    public class Product
    {
        [Key] // PRIMARY KEY
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // AUTO_INCREMENT
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
