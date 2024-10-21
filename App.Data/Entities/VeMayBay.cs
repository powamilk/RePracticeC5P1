using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Entities
{
    public class VeMayBay
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
        public string SoHieuMayBay {  get; set; }   
        public DateTime NgayBay {  get; set; }
        public string DiemKhoiHanh { get; set; }
        public string DiemDen {  get; set; }
        public decimal GiaVe {  get; set; } 
    }
}
