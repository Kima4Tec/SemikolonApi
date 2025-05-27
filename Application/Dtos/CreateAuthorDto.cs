using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    /// <summary>
    /// For cleaner code, this DTO is used to create an author without the ID.
    /// </summary>
    public class CreateAuthorDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

    }
}
