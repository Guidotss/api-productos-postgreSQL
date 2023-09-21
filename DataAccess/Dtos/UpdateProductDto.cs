using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos
{
    public class UpdateProductDto
    {
        public string? Name { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public decimal? Price { get; set; } = default!;
        public int? Quantity { get; set; } = default!;
    }
}
