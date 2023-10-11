using Microsoft.AspNetCore.Http;

namespace TestPlatform.Services.DTOs.ScienceDTOs;

public record AddPhotoDto(
    long scienceTypeId,
    IFormFile file);