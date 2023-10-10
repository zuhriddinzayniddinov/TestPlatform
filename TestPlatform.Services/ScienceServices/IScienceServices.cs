using TestPlatform.Services.DTOs.ScienceDTOs;

namespace TestPlatform.Services.ScienceServices;

public interface IScienceServices
{
    ValueTask<ScienceDto> CreateScienceAsync(ScienceForCreationDto scienceForCreationDto);
    IQueryable<ScienceDto> RetrieveSciences();
    ValueTask<ScienceDto> RetrieveScienceByIdAsync(long id);
    ValueTask<ScienceDto> RemoveScienceAsync(long id);
    ValueTask<ScienceTypeDto> CreateScienceTypeAsync(ScienceTypeForCreationDto scienceTypeForCreationDto);
    IQueryable<ScienceTypeDto> RetrieveScienceTypes();
    ValueTask<ScienceTypeDto> RetrieveScienceTypeByIdAsync(long id);
    ValueTask<ScienceTypeDto> RemoveScienceTypeAsync(long id);
    IQueryable<ScienceDto> RetrieveByCountSciences(int count);
    IQueryable<ScienceTypeDto> RetrieveByNameScienceTypes(string name);
    IQueryable<ScienceDto> RetrieveByNameSciences(string name);
}