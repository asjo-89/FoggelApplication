using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Services.Models
{
    public class ObservationListItem
    {
        public int ObservationYear { get; set; }
        public string MonthName { get; set; } = null!;
        public byte Månadsnummer { get; set; }
        public string SpeciesName { get; set; } = null!;
        public string LocationName { get; set; } = null!;
        public DateTime? CreatedDate { get; set; }
    }
}
