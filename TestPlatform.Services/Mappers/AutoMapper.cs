using AutoMapper;

namespace TestPlatform.Services.Mappers;

public static class AutoMapper
{
    private static IMapper _mapper;

    public static IMapper Mapper
    {
        get
        {
            if (_mapper == null)
            {
                var config = new MapperConfiguration(cfg => cfg.AddProfile<MapperConfigure>());
                _mapper = config.CreateMapper();
            }
            return _mapper;
        }
    }
}