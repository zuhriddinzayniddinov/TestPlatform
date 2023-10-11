using TestPlatform.Domain.Entities.Sciences;
using TestPlatform.Infrastructure.Repositories.Sciences;
using TestPlatform.Infrastructure.Repositories.Sciences.Types;
using TestPlatform.Services.DTOs.ScienceDTOs;

namespace TestPlatform.Services.ScienceServices;

public class ScienceServices : IScienceServices
{
    private readonly IScienceRepository _scienceRepository;
    private readonly IScienceTypeRepository _scienceTypeRepository;

    public ScienceServices(
        IScienceRepository scienceRepository,
        IScienceTypeRepository scienceTypeRepository)
    {
        _scienceRepository = scienceRepository;
        _scienceTypeRepository = scienceTypeRepository;
    }

    public async ValueTask<ScienceDto> CreateScienceAsync(ScienceForCreationDto scienceForCreationDto)
    {
        var science = new Science()
        {
            UserId = scienceForCreationDto.userId,
            isPrivate = scienceForCreationDto.isPrivate,
            ScienceTypesId = scienceForCreationDto.scienceTypeId,
            Name = scienceForCreationDto.name
        };

        science = await _scienceRepository.InsertAsync(science);

        return new ScienceDto(
            science.Id,
            science.ScienceTypesId,
            science.UserId,
            science.CountQuizzes,
            science.Name,
            science.PhotoUrl);
    }

    public IQueryable<ScienceDto> RetrieveSciences()
    {
        return _scienceRepository
            .SelectAll()
            .Select(s => 
                new ScienceDto(
                    s.Id,
                    s.ScienceTypesId,
                    s.UserId,
                    s.CountQuizzes,
                    s.Name,
                    s.PhotoUrl));
    }

    public async ValueTask<ScienceDto> RetrieveScienceByIdAsync(long id)
    {
        var science = await _scienceRepository.SelectByIdAsync(id);

        return new ScienceDto(
            science.Id,
            science.ScienceTypesId,
            science.UserId,
            science.CountQuizzes,
            science.Name,
            science.PhotoUrl);
    }

    public async ValueTask<ScienceDto> RemoveScienceAsync(long id)
    {
        var science = await _scienceRepository.SelectByIdAsync(id);
        
        science = await _scienceRepository.DeleteAsync(science);
        
        return new ScienceDto(
            science.Id,
            science.ScienceTypesId,
            science.UserId,
            science.CountQuizzes,
            science.Name,
            science.PhotoUrl);
    }

    public async ValueTask<ScienceTypeDto> CreateScienceTypeAsync(ScienceTypeForCreationDto scienceTypeForCreationDto)
    {
        var scienceType = new ScienceTypes()
        {
            Name = scienceTypeForCreationDto.name
        };

        scienceType = await _scienceTypeRepository.InsertAsync(scienceType);

        return new ScienceTypeDto(
            scienceType.Id,
            scienceType.Name,
            scienceType.PhotoUrl);
    }

    public IQueryable<ScienceTypeDto> RetrieveScienceTypes()
    {
        return _scienceTypeRepository.SelectAll()
            .Select(st => new ScienceTypeDto(st.Id, st.Name, st.PhotoUrl));
    }

    public async ValueTask<ScienceTypeDto> RetrieveScienceTypeByIdAsync(long id)
    {
        var scienceType = await _scienceTypeRepository.SelectByIdAsync(id);

        return new ScienceTypeDto(
            scienceType.Id,
            scienceType.Name,
            scienceType.PhotoUrl);
    }

    public async ValueTask<ScienceTypeDto> RemoveScienceTypeAsync(long id)
    {
        var scienceType = await _scienceTypeRepository.SelectByIdAsync(id);

        scienceType = await _scienceTypeRepository.DeleteAsync(scienceType);

        return new ScienceTypeDto(
            scienceType.Id,
            scienceType.Name,
            scienceType.PhotoUrl);
    }

    public IQueryable<ScienceDto> RetrieveByCountSciences(int count)
    {
        return _scienceRepository
            .SelectAll()
            .Where(s => s.CountQuizzes >= count)
            .Select(s => 
                new ScienceDto(
                    s.Id,
                    s.ScienceTypesId,
                    s.UserId,
                    s.CountQuizzes,
                    s.Name,
                    s.PhotoUrl));
    }

    public IQueryable<ScienceTypeDto> RetrieveByNameScienceTypes(string name)
    {
        return _scienceTypeRepository.SelectAll()
            .Where(st => st.Name.Contains(name))
            .Select(st => new ScienceTypeDto(st.Id, st.Name, st.PhotoUrl));
    }

    public IQueryable<ScienceDto> RetrieveByNameSciences(string name)
    {
        return _scienceRepository
            .SelectAll()
            .Where(s => s.Name.Contains(name))
            .Select(s => 
                new ScienceDto(
                    s.Id,
                    s.ScienceTypesId,
                    s.UserId,
                    s.CountQuizzes,
                    s.Name,
                    s.PhotoUrl));
    }

    public async ValueTask<ScienceTypeDto> AddPhotoScienceAsync(AddPhotoDto addPhotoDto)
    {
        var scienceType = await _scienceTypeRepository.SelectByIdAsync(addPhotoDto.scienceTypeId);

        var filePath = Guid.NewGuid() + Path.GetExtension(addPhotoDto.file.FileName);
        
        var path = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot");
        
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        
        scienceType.PhotoUrl = filePath;

        path = Path.Combine(path, filePath);

        await using (Stream fileStream = new FileStream(path, FileMode.Create))
        {
            await  addPhotoDto.file.CopyToAsync(fileStream);
        }

        scienceType = await _scienceTypeRepository.UpdateAsync(scienceType);
        
        return new ScienceTypeDto(scienceType.Id, scienceType.Name, scienceType.PhotoUrl);
    }
}