using AutoMapper;
using NewsD.DataContracts;
using NewsD.Model;

namespace NewsD.DataProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<DataContracts.UserRequest, Model.User>()
            .ForMember(dest => dest.ProfilePhotoUrl, opt => opt.MapFrom(src => ""))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.LastUpdate, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => Model.UserRole.Normal));

        CreateMap<Model.User, DataContracts.UserResponse>();
    }
}