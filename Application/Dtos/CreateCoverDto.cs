using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CreateCoverDto
    {
        public string DesignIdeas { get; set; } = string.Empty;
        public bool DigitalOnly { get; set; }
        public int BookId { get; set; }

        public List<int> ArtistIds { get; set; } = new List<int>();
    }
}
