using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektIPS.ViewModels
{
    public class FaceViewModel
    {
        public int Top { get; set; }
        public int Left { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string Gender { get; set; }
        public double Age { get; set; }
        public string Emotion { get; set; }
    }
}
