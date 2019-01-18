using AutoMapper;
using GraduwayExam.Common.Models.ViewModel;
using GraduwayExam.Data.Models;

namespace GraduwayExam.Maps
{
    public static class UserMapper
    {
        public static IMapper Mapper()
        {
            var mapperConfig = new MapperConfiguration(
                configuration =>
                {
                    configuration.CreateMap<User, UserVm>().ForMember(m => m.Password, o => o.Ignore())
                        .ForMember(m => m.ConfirmPassword, o => o.Ignore())
                        .ForMember(m => m.CurrentPassword, o => o.Ignore()).ReverseMap()
                        .ForMember(x => x.Tasks, opt => opt.Ignore());
                });

            mapperConfig.AssertConfigurationIsValid();

            return mapperConfig.CreateMapper();
        }

    }
}
