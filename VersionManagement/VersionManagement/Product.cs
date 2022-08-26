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
        public int? Id { get; set; }
        public string? ProductName { get; set; }
        public int? ProductPrice { get; set; }
        public int? ProductInventory { get; set; }
    }
}
