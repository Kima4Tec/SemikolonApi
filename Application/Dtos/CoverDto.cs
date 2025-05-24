using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CoverDto
    {
        public string DesignIdeas { get; set; } = string.Empty;
        public bool DigitalOnly { get; set; }
        public List<ArtistDto> Artists { get; set; } = new();
    }
}
