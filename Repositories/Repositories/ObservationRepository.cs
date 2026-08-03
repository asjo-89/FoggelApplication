using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories.Context;
using Repositories.Entities;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Repositories.Repositories
{
    public class ObservationRepository(AppDbContext dbContext, ILogger<ObservationRepository> logger)
    {
        private readonly AppDbContext _dbContext = dbContext;
        private readonly ILogger<ObservationRepository> _logger = logger;

        public async Task<EntityResult<Observation>> AddObservationAsync(CreateObservation observation)
        {
            if (observation == null)
            {
                _logger.LogWarning("Observation model is null.");
                return new EntityResult<Observation>
                {
                    Success = false,
                    Message = "Observation model cannot be null."
                };
            }

            var storedProcedure = "exec usp_AddObservation @ObservationYear, @ObservationMonth, @SpeciesName, @Location, @ResultOutput OUTPUT";
            var parameters = new[]
                {
                    new SqlParameter("@ObservationYear", observation.Year),
                    new SqlParameter("@ObservationMonth", observation.Month),
                    new SqlParameter("@SpeciesName", observation.SpeciesName),
                    new SqlParameter("@Location", observation.LocationId),
                    new SqlParameter
                    {
                        ParameterName = "@ResultOutput",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Output
                    }
                };
            await _dbContext.Database.ExecuteSqlRawAsync(storedProcedure, parameters);

            var result = (int)parameters[3].Value == 1;

            return result ?
                new EntityResult<Observation>
                    {
                        Success = true,
                        Message = "Registrering lyckades!"
                    }
                : new EntityResult<Observation>
                    {
                        Success = false,
                        Message = "Fågeln är redan registrerad den perioden."
                    };
        }

        public async Task<EntityResult<List<VwTotalList>>> GetAllObservationsAsync()
        {
            var observations = await _dbContext.VwTotalLists
                .OrderByDescending(o => o.ObservationYear)
                .ThenByDescending(o => o.Månadsnummer)
                .ThenBy(o => o.SpeciesName)
                .ThenBy(o => o.LocationName)
                .AsNoTracking()
                .ToListAsync();

            return observations.Count > 0 
                ? new EntityResult<List<VwTotalList>>
                    {
                        Success = true,
                        Entity = observations
                    } 
                : new EntityResult<List<VwTotalList>>
                    {
                        Success = false,
                        Message = "Det finns inga observationer att visa..."
                    };
        }

        public Observation GetOneObservation(int observationId)
        {
            var observation = _dbContext.Observations.FirstOrDefault(x => x.ObservationId == observationId);
            return observation ?? new Observation();
        }


        public async Task<EntityResult> DeleteObservationAsync(Observation observation)
        {
            if(observation == null)
            {
                _logger.LogWarning("The observation model is null.");
                return new EntityResult
                {
                    Success = false,
                    Message = "Något gick fel. Försök igen."
                };
            }

            _dbContext.Remove(observation!);

            try
            {
                var result = await _dbContext.SaveChangesAsync();

                return result == 1
                    ? new EntityResult
                    {
                        Success = true,
                        Message = "Observationen är borttagen."
                    }
                    : new EntityResult
                    {
                        Success = false,
                        Message = "Det gick inte att ta bort observationen."
                    };
            }
            catch(DBConcurrencyException ex)
            {
                _logger.LogWarning("Unable to delete observation. " + ex);
                return new EntityResult
                {
                    Success = false,
                    Message = "Det gick inte att ta bort observationen."
                };
            }
            catch(Exception ex)
            {
                _logger.LogWarning("Something went wrong when deleting observation." + ex);
                return new EntityResult
                {
                    Success = false,
                    Message = "Det gick inte att ta bort observationen."
                };
            }
        }
    }
}
