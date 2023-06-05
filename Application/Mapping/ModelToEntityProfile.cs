using Domain.Entities;
using Domain.Models;
using Profile = AutoMapper.Profile;
using ProfileEntity = Domain.Entities.Profile;

namespace Application.Mapping
{
    public class ModelToEntityProfile : Profile
    {
        public ModelToEntityProfile()
        {
            CreateMap<UserModel, User>();
            CreateMap<ProfileModel, ProfileEntity>();
            CreateMap<MedicalCardModel, MedicalCard>();
            CreateMap<RecordModel, Record>();
        }
    }
}
