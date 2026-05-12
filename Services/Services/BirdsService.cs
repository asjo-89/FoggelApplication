using Repositories.Repositories;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services
{
    public class BirdsService(BirdsRepository repo)
    {
        private readonly BirdsRepository _repo = repo;

        public async Task<ModelResult<List<SpeciesModel>>> GetAllBirdsAsync()
        {
            var entities = await _repo.GetAllBirdsAsync();
            if (!entities.Success || entities.Entity?.Count == 0)
            {
                return new ModelResult<List<SpeciesModel>>
                {
                    Success = false,
                    Message = $"Failed to retrieve birds: {entities.Message}"
                };
            }

            var speciesList = entities.Entity?.Select(e => new SpeciesModel
            {
                Id = e.SpeciesId,
                Name = e.SpeciesName,
                FileId = e.FileId,
                RelativePath = "",
                Observations = e.Observations.Select(o => new ObservationModel
                {
                    Id = o.ObservationId,
                    MonthId = o.ObservationMonthId,
                    CreatedDate = o.CreatedDate,
                    SpeciesId = o.SpeciesId
                }).ToList()
            }).ToList();

            return new ModelResult<List<SpeciesModel>>
            {
                Success = true,
                Model = speciesList,
                Message = "Birds retrieved successfully."
            };
        }
    }
}
