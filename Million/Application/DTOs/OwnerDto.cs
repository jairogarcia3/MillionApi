using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class OwnerDto
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; }
        public string? Photo { get; set; }
        public DateTime? Birthday { get; set; }
        
    }
}
