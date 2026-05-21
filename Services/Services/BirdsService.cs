using Repositories.Repositories;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services
{
    public class BirdsService(BirdsRepository birdsRepo, ImageService imageService)
    {
        private readonly BirdsRepository _birdsRepo = birdsRepo;
        private readonly ImageService _imageService = imageService;

        public async Task<ModelResult<List<SpeciesModel>>> GetAllBirdsAsync()
        {
            var entities = await _birdsRepo.GetAllBirdsAsync();
            if (!entities.Success || entities.Entity == null || entities.Entity?.Count == 0)
            {
                return new ModelResult<List<SpeciesModel>>
                {
                    Success = false,
                    Message = $"Failed to retrieve birds: {entities?.Message}"
                };
            }

            //var fileIds = entities.Entity!
            //    .Select(e => e.FileId)
            //    .ToList();

            //var images = await _imageService.GetSpeciesImagesAsync(fileIds);
            //var dictionaryLookup = images.ToDictionary(x => x.Id);
            var speciesList = entities.Entity?.Select(e =>             
            {
                //dictionaryLookup.TryGetValue(e.FileId ?? Guid.Empty, out var image);
                
                return new SpeciesModel {
                    Id = e.SpeciesId,
                    Name = e.SpeciesName,
                    FileId = e.FileId,
                    //ImageFileUrl = image?.Thumbnail64Base,
                    //Observations = e.Observations.Select(o => new ObservationModel
                    //{
                    //    Id = o.ObservationId,
                    //    MonthId = o.ObservationMonthId,
                    //    CreatedDate = o.CreatedDate,
                    //    SpeciesId = o.SpeciesId
                    //}).ToList()
                };
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
