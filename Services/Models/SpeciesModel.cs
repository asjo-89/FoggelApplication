using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models
{
    public class SpeciesModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid? FileId { get; set; }
        public string RelativePath { get; set; } = null!;

        public List<ObservationModel> Observations { get; set; } = new List<ObservationModel>();
    }
}
