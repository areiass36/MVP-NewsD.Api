using AutoMapper;

namespace NewsD.DataProfiles;

public class NewsProfile : Profile
{
    public NewsProfile()
    {
        CreateMap<DataContracts.NewsRequest, Model.News>();

        CreateMap<Model.News, DataContracts.NewsResponse>();
    }
}