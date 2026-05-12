using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Models
{
    public class CreateObservation
    {
        public int SpeciesId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string SpeciesName { get; set; } = null!;
    }
}
