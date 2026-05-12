
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models
{
    public class ObservationModel
    {
        public int Id { get; set; }
        public int MonthId { get; set; }
        public int YearId { get; set; }
        public DateTime? CreatedDate { get; set; }

        public int SpeciesId { get; set; }
    }
}
