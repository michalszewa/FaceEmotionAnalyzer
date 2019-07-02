using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektIPS.Models
{
    public class Image
    {
        public virtual List<Face> Faces { get; set; }

        [Required]
        [Key]
        public int Id { get; set; }
        public string Path { get; set; }
        public int Anger { get; set; }
        public int Contempt { get; set; }
        public int Disgust { get; set; }
        public int Fear { get; set; }
        public int Happiness { get; set; }
        public int Neutral { get; set; }
        public int Sadness { get; set; }
        public int Surprise { get; set; }
        public DateTime PublicationTime { get; set; }
        public bool Publicate { get; set; }

    }
}
