
using Repositories.Entities;
using Repositories.Models;
using Repositories.Repositories;
using Services.FormModels;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services
{
    public class ObservationService(ObservationRepository repo)
    {
        private readonly ObservationRepository _repo = repo;

        public async Task<ModelResult<ObservationModel>> AddObservationAsync(ObservationFormModel form)
        {
            if (form == null)
            {
                return new ModelResult<ObservationModel>
                {
                    Success = false,
                    Message = "Observation form cannot be null."
                };
            }

            var observation = new CreateObservation
            {
                SpeciesId = form.SpeciesId,
                Year = form.Year,
                Month = form.Month,
                SpeciesName = form.SpeciesName,
                LocationId = form.LocationId
            };

            var result = await _repo.AddObservationAsync(observation);

            return result.Success 
                ? new ModelResult<ObservationModel>
                    {
                        Success = true,
                        Message = result.Message ?? ""
                    }
                : new ModelResult<ObservationModel>
                {
                    Success = false,
                    Message = result.Message ?? ""
                };
        }
    
        public async Task<ModelResult<List<ObservationListItem>>> GetAllObservationsAsync()
        {
            var listResult = await _repo.GetAllObservationsAsync();

            if(!listResult.Success || listResult.Entity == null || !listResult.Entity.Any())
            {
                return new ModelResult<List<ObservationListItem>>
                {
                    Success = false,
                    Message = listResult.Message ?? ""
                };
            }

            var modelList = new List<ObservationListItem>();
            foreach(var entity in listResult.Entity)
            {
                var observation = new ObservationListItem
                {
                    ObservationYear = entity.ObservationYear,
                    MonthName = entity.MonthName,
                    Månadsnummer = entity.Månadsnummer,
                    SpeciesName = entity.SpeciesName,
                    LocationName = entity.LocationName ?? "",
                    CreatedDate = entity.CreatedDate
                };
                modelList.Add(observation);
            }

            return new ModelResult<List<ObservationListItem>>
            {
                Success = true,
                Model = modelList,
                Message = listResult.Message ?? ""
            };
        }
    
        public async Task<ModelResult> DeleteObservationAsync(ObservationModel observation)
        {
            if(observation == null)
            {
                return new ModelResult
                {
                    Success = false,
                    Message = "Det gick inte att ta bort observationen. Försök igen."
                };
            }

            var entity = new Observation
            {
                ObservationId = observation.Id,
                ObservationMonthId = observation.MonthId,
                SpeciesId = observation.SpeciesId,
                CreatedDate = observation.CreatedDate
            };

            var deleted = await _repo.DeleteObservationAsync(entity);

            return new ModelResult { Success = deleted.Success, Message = deleted.Message };
        }
    }
}
