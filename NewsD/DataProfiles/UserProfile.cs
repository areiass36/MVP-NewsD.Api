using AutoMapper;
using NewsD.DataContracts;
using NewsD.Model;
using static BCrypt.Net.BCrypt;

namespace NewsD.DataProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<DataContracts.UserRequest, Model.User>()
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => HashPassword(src.Password ?? "")))
            .ForMember(dest => dest.ProfilePhotoUrl, opt => opt.MapFrom(src => ""))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.LastUpdate, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<Model.User, DataContracts.UserResponse>();
    }
}