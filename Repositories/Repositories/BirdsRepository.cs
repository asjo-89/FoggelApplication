using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories.Data;
using Repositories.Entities;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Repositories
{
    public class BirdsRepository(AppDbContext dbContext, ILogger<BirdsRepository> logger)
    {
        private readonly AppDbContext _dbContext = dbContext;
        private readonly ILogger<BirdsRepository> _logger = logger;

        public async Task<EntityResult<List<Species>>> GetAllBirdsAsync()
        {
            try
            {
                var birds = await _dbContext.Species
                    //.Include(s => s.Observations)
                    .OrderBy(s => s.SpeciesName)
                    .ToListAsync();
                return new EntityResult<List<Species>>
                {
                    Success = true,
                    Entity = birds,
                    Message = "Birds retrieved successfully."
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving birds.");
                return new EntityResult<List<Species>>
                {
                    Success = false,
                    Message = "Failed to retrieve birds."
                };
            }
        }

    }
}
