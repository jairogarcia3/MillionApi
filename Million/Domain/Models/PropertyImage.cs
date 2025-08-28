using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PropertyImage
    {
        [Key]
        public int IdPropertyImage { get; set; }
        public int PropertyIdProperty { get; set; }
        public string File { get; set; }
        public bool Enabled { get; set; }

        //public Property Property { get; set; }
    }
}
