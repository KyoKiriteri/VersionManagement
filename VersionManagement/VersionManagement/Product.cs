using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersionManagement
{
    public class Product
    {
        [Key]
        public string? Password { get; set; }
        public string? Username { get; set; }
    }
}
