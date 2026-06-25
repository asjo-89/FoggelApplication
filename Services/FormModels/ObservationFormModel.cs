namespace Services.FormModels
{
    public class ObservationFormModel
    {
        public int SpeciesId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string SpeciesName { get; set; } = null!;
        public int LocationId { get; set; }
        public string? LocationName { get; set; }
        //public string Location { get; set; } = null!;
        //public int MonthId { get; set; }
        //public DateTime Date { get; set; }
        //public int SpeciesId { get; set; } 
    }
}
