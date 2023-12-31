﻿using Rusada.Core.Common.Interfaces;
using Rusada.Core.Dto;
using Rusada.Core.Interface;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Rusada.Core.Common;
using Rusada.Domain;
using Rusada.Domain.BaseEntities;

namespace Rusada.Core.Services
{
    public class AircraftSightingService : IAircraftSightingService
    {
        private readonly IRusadaDbContext _rusadaDbContext;
        private readonly IConfiguration _configuration;

        public AircraftSightingService(IRusadaDbContext rusadaDbContext, IConfiguration configuration)
        {
            _rusadaDbContext = rusadaDbContext;
            _configuration = configuration;
        }

        public async Task<IResponse<AircraftDto>> AddSightingAsync(AircraftDto aircraftDto)
        {
            var aircraft = new Aircraft()
            {
                Location = aircraftDto.Location,
                Model = aircraftDto.Model,
                Make = aircraftDto.Make,
                Registration = aircraftDto.Registration,
                DateTime = aircraftDto.DateTime,
            };
            _rusadaDbContext.Aircrafts.Add(aircraft);
            aircraftDto.Id = aircraft.Id;
            await _rusadaDbContext.SaveChangesAsync();
            return ResponseFactory.Success(aircraftDto);
        }

        public async Task<IResponse<AircraftDto>> UpdateAsync(AircraftDto aircraftDto, IFormFile? image)
        {
            var existing = await _rusadaDbContext.Aircrafts.FirstOrDefaultAsync(x => x.Id == aircraftDto.Id);
            if (existing is null)
            {
                return ResponseFactory.Error<AircraftDto>("Invalid record for update", HttpStatusCode.BadRequest);
            }

            if (image != null)
            {
                var existingImage = await _rusadaDbContext.AircraftImages.FirstOrDefaultAsync(x => x.AircraftId == aircraftDto.Id);
                existing.ImageUrl = await UploadImageAsync(image, aircraftDto.Model, aircraftDto.Id, existingImage);
            }
            else
            {
                if (string.IsNullOrEmpty(aircraftDto.ImagePath))
                {
                    var existingImage = await _rusadaDbContext.AircraftImages.FirstOrDefaultAsync(x => x.AircraftId == aircraftDto.Id);
                    if (existingImage is not null)
                    {
                        _rusadaDbContext.AircraftImages.Remove(existingImage);
                    }

                    existing.ImageUrl = string.Empty;
                }
            }

            existing.Location = aircraftDto.Location;
            existing.Model = aircraftDto.Model;
            existing.Make = aircraftDto.Make;
            existing.Registration = aircraftDto.Registration;
            existing.DateTime = aircraftDto.DateTime;


            _rusadaDbContext.Aircrafts.Update(existing);
            await _rusadaDbContext.SaveChangesAsync();
            return ResponseFactory.Success(aircraftDto);
        }

        public async Task<IResponse<AircraftImageDto>> GetAircraftImageAsync(Guid key, string filename)
        {
            var image = await _rusadaDbContext.AircraftImages.FirstOrDefaultAsync(x => x.Key == key && x.FileName == filename);

            if (image == null)
            {
                return ResponseFactory.Error<AircraftImageDto>("Invalid image", HttpStatusCode.BadRequest);
            }

            return ResponseFactory.Success(new AircraftImageDto()
            {
                Key = image.Key,
                Path = image.Path,
                ContentType = image.ContentType,
                FileName = image.FileName,
                Base64Logo = image.Base64Logo
            });
        }

        public async Task<IResponse<List<AircraftDto>>> GetAllAsync()
        {
            var result = await _rusadaDbContext.Aircrafts.Where(x => !x.Deleted).Select(x => new AircraftDto()
            {
                Id = x.Id,
                Location = x.Location,
                Model = x.Model,
                Make = x.Make,
                Registration = x.Registration,
                DateTime = x.DateTime,
                ImagePath = x.ImageUrl
            }).ToListAsync();

            return ResponseFactory.Success(result);
        }

        public async Task<IResponse<bool>> DeleteAircraftAsync(int id)
        {
            var recordToDelete = await _rusadaDbContext.Aircrafts.FirstOrDefaultAsync(x => x.Id == id && !x.Deleted);
            if (recordToDelete == null) return ResponseFactory.Success(false);
            _rusadaDbContext.Aircrafts.Remove(recordToDelete);

            var existingImage = await _rusadaDbContext.AircraftImages.FirstOrDefaultAsync(x => x.AircraftId == id);
            if (existingImage != null)
            {
                _rusadaDbContext.AircraftImages.Remove(existingImage);
            }

            await _rusadaDbContext.SaveChangesAsync();
            return ResponseFactory.Success(true);
        }

        public async Task<IResponse<AircraftDto>> GetByIdAsync(int id)
        {
            var result = await _rusadaDbContext.Aircrafts
                .Where(x => !x.Deleted && x.Id == id)
                .Select(x => new AircraftDto()
                {
                    Id = x.Id,
                    Location = x.Location,
                    Model = x.Model,
                    Make = x.Make,
                    Registration = x.Registration,
                    DateTime = x.DateTime,
                    ImagePath = x.ImageUrl,
                }).FirstOrDefaultAsync();

            return ResponseFactory.Success<AircraftDto>(result);
        }

        #region Private

        private async Task<string> UploadImageAsync(IFormFile file, string model, int aircraftId, AircraftImage? existingImage)
        {
            var errorMessage = string.Empty;

            var validContentTypes = new[] { "image/jpeg", "image/png", "image/svg+xml" };
            if (!validContentTypes.Contains(file.ContentType))
            {
                errorMessage = $"{file.ContentType} is not allowed";
            }

            var maxLength = 2500000; // 2.5MB
            if (file.Length > maxLength)
            {
                errorMessage = "File size should be less than 2.5MB";
            }

            if (!string.IsNullOrWhiteSpace(errorMessage))
                throw new RusdaException($"{file.ContentType} is not allowed", HttpStatusCode.BadRequest);


            await using var memStream = new MemoryStream();
            await file.CopyToAsync(memStream);
            var base64Logo = Convert.ToBase64String(memStream.ToArray());


            var logoPath = _configuration["BaseUrl"]?.TrimEnd('/');
            var normalizedFileName = NormalizeFileName(model, file.FileName, file.ContentType);
            var key = Guid.NewGuid();
            var uriBuilder = new UriBuilder(logoPath)
            {
                Path = $"api/aircraftLogo/image/{key}/{normalizedFileName}"
            };

            var image = existingImage ?? new AircraftImage();

            if (existingImage is not null)
            {
                image.Base64Logo = base64Logo;
                image.ContentType = file.ContentType;
                image.Path = uriBuilder.ToString();
                image.FileName = normalizedFileName;
                image.Key = key;


                _rusadaDbContext.AircraftImages.Update(image);
            }
            else
            {
                image = new AircraftImage()
                {
                    Base64Logo = base64Logo,
                    ContentType = file.ContentType,
                    Path = uriBuilder.ToString(),
                    FileName = normalizedFileName,
                    Key = key,
                    AircraftId = aircraftId
                };
                _rusadaDbContext.AircraftImages.Add(image);
            }

            await _rusadaDbContext.SaveChangesAsync();
            return image.Path;
        }

        private string NormalizeFileName(string model, string currentFileName, string contentType)
        {
            var validExtension = new string[] { ".jpg", ".jpeg", ".png", ".svg" };

            var ext = currentFileName.Substring(currentFileName.LastIndexOf(".", StringComparison.Ordinal));

            if (!validExtension.Contains(ext.ToLower()))
                throw new RusdaException("invalid file extension", System.Net.HttpStatusCode.BadRequest);

            var fileName = $"{model}{ext}";
            return fileName;
        }

        #endregion
    }
}