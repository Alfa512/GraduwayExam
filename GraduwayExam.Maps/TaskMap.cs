using AutoMapper;
using GraduwayExam.Common.Models.ViewModel;
using GraduwayExam.Data.Models;

namespace GraduwayExam.Maps
{
    public static class TaskMapper
    {
        public static IMapper Mapper()
        {
            var mapperConfig = new MapperConfiguration(
                configuration =>
                {
                    configuration.CreateMap<Task, TaskVm>().ReverseMap()
                        .ForMember(m => m.User, o => o.UseDestinationValue())
                        .ForMember(m => m.Creator, o => o.UseDestinationValue());
                });

            mapperConfig.AssertConfigurationIsValid();

            return mapperConfig.CreateMapper();
        }

    }
}
