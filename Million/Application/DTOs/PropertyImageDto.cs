using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PropertyImageDto
    {        
        public int PropertyIdProperty { get; set; }
        public string File { get; set; }
        public bool Enabled { get; set; }
    }
}
