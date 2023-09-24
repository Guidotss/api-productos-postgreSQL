using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos
{
    public class UpdateUserDto
    {
        public string? Name { get; set; } = default!;
        public string? Email { get; set; } = default!;
        public string? Password { get; set; } = default!;
    }
}
