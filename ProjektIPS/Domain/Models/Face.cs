using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektIPS.Models
{
    public class Face
    {
        [ForeignKey("PhotoId")]
        public Photo Photo { get; set; }

        [Required]
        [Key]
        public int Id { get; set; }
        public int PhotoId { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string Gender { get; set; }
        public double Age { get; set; }
        public string Emotion { get; set; }
    }
}
