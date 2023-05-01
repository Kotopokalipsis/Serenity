using Domain.Entities;
using Domain.Models;
using Profile = AutoMapper.Profile;
using ProfileEntity = Domain.Entities.Profile;

namespace Application.Mapping
{
    public class EntityToModelProfile : Profile
    {
        public EntityToModelProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<ProfileEntity, ProfileModel>();
        }
    }
}
