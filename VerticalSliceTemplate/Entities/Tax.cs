using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VerticalSliceTemplate.Entities
{

    public class Tax
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        public Guid GroupId { get; set; }

        [Required]
        public Guid FiscalFolioUUID { get; set; }

        [Required]
        public short MovementNumber { get; set; }

        public string? LocalTaxName { get; set; }

        [Required]
        public decimal Base { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal Total { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime ModifiedAt { get; set; }
    }

}
