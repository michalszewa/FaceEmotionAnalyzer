using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektIPS.Models
{
    public class Face
    {
        [ForeignKey("ImageId")]
        public virtual Image Image { get; set; }

        [Required]
        [Key]
        public int Id { get; set; }
        public int ImageId { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string Gender { get; set; }
        public double Age { get; set; }
        public string Emotion { get; set; }
    }
}
