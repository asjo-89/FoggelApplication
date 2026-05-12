using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories.Data;
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

            var allreadyExists = await _dbContext.Observations.FirstOrDefaultAsync(o => o.SpeciesId == observation.SpeciesId);
            if(allreadyExists != null)
            {
                return new EntityResult<Observation>
                {
                    Success = false,
                    Message = $"{observation.SpeciesName ?? "Fågeln"} är redan registrerad den månaden."
                };
            }

            var storedProcedure = "exec usp_AddObservation @ObservationYear, @ObservationMonth, @SpeciesName, @ResultOutput OUTPUT";
            var parameters = new[]
                {
                    new SqlParameter("@ObservationYear", observation.Year),
                    new SqlParameter("@ObservationMonth", observation.Month),
                    new SqlParameter("@SpeciesName", observation.SpeciesName),
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
                        Message = "Registrering misslyckades..."
                    };
        }

        public async Task<EntityResult<List<Vw_total_list>>> GetAllObservationsAsync()
        {
            var observations = await _dbContext.ViewTotalObservations
                .OrderByDescending(o => o.ObservationYear)
                .ThenByDescending(o => o.Månadsnummer)
                .AsNoTracking()
                .ToListAsync();

            return observations.Count > 0 
                ? new EntityResult<List<Vw_total_list>>
                    {
                        Success = true,
                        Entity = observations
                    } 
                : new EntityResult<List<Vw_total_list>>
                    {
                        Success = false,
                        Message = "Det finns inga observationer att visa..."
                    };
        }
    }
}
