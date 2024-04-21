using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    // [Table("Articles")]
    public class Article
    {
        [Key]
        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        [Column(TypeName = "nvarchar")]
        [DisplayName("Tiêu đề")]
        public string Title { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        [DisplayName("Ngày tạo")]
        public DateTime Created { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }
    }
}