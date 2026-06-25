using Microsoft.EntityFrameworkCore;
using Repositories.Context;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Repositories
{
    public class ImageRepository(AppDbContext dbContext)
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<List<SpeciesFiles>> GetSpeciesImagesAsync(List<Guid?> fileIds)
        {
            Console.WriteLine($"After call: {fileIds?.Count}");

            var validIds = fileIds
                .Where(x => x.HasValue)
                .Select(x => x.Value)
                .ToList();

            var files = await _dbContext.SpeciesFiles
                .Where(sf => validIds.Contains(sf.StreamId))
                .ToListAsync();

            var totalBytes = files.Sum(x => x.FileData.Length);

            return files;
        }

        public async Task<SpeciesFiles> GetImageByIdAsync(Guid? fileId)
        {
            if(fileId == Guid.Empty)
            {
                return null!;
            }

            var image = await _dbContext.SpeciesFiles
                .Where(sf => sf.StreamId == fileId)
                .FirstOrDefaultAsync();

            return image ?? null!;
        }
    }
}
