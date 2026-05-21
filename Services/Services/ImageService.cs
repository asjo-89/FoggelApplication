using Repositories.Entities;
using Repositories.Repositories;
using Services.Models;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Services.Services
{
    public class ImageService(ImageRepository repo)
    {
        private readonly ImageRepository _repo = repo;

        public async Task<SpeciesFiles> GetImageByIdAsync(Guid? fileId)
        {
            if(fileId == Guid.Empty)
            {
                return null!;
            }

            var result = await _repo.GetImageByIdAsync(fileId);
            //var imageModel = new SpeciesImage
            //{
            //    Id = result.StreamId,
            //    Thumbnail64Base = $"data:image/jpeg;base64,{CreateThumbnailAsync(result.FileData)}",
            //    FileType = result.FileType,
            //    Name = result.FileName
            //};

            return result;
        }

        public async Task<List<SpeciesImage>> GetSpeciesImagesAsync(List<Guid?> fileIds)
        {
            if(!fileIds.Any())
            {
                return new List<SpeciesImage>();
            }
            Console.WriteLine($"Before call: {fileIds?.Count}");
            var images = await _repo.GetSpeciesImagesAsync(fileIds);

            var imagesList = images.Select(i =>
                    new SpeciesImage
                    {
                        Id = i.StreamId,
                        Name = i.FileName,
                        Thumbnail64Base = $"data:image/jpeg;base64,{Convert.ToBase64String(i.FileData)}",
                        //Thumbnail64Base = $"data:image/jpeg;base64,{CreateThumbnailAsync(i.FileData)}",
                        FileType = i.FileType
                    })
                .ToList();

            return imagesList;
        }

        public string CreateThumbnailAsync(byte[] fileData)
        {
            using var bitmap = SKBitmap.Decode(fileData);

            using var resized = bitmap.Resize(
                new SKImageInfo(40, 32),
                new SKSamplingOptions()
            );

            using var image = SKImage.FromBitmap(resized);
            using var data = image.Encode(SKEncodedImageFormat.Jpeg, 75);

            return Convert.ToBase64String(data.ToArray());
        }
    }
}
