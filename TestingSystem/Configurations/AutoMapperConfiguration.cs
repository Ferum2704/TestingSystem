using Application.Identitity;
using Application.Mapping;
using Application.Subjects.Add;
using AutoMapper;
using Presentation.Api.Models;

namespace Presentation.Configurations
{
    internal static class AutoMapperConfiguration
    {
        private static MapperConfiguration configuration;

        public static IMapper ResolveMapper()
        {
            configuration ??= new MapperConfiguration(Configure);

            return configuration.CreateMapper();
        }

        private static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<RegistrationModel, RegisterUser>();
            cfg.CreateMap<LoginModel, LoginUser>();
            cfg.CreateMap<SubjectModel, AddSubjectCommand>();

            ApplicationMappings.ConfigureAutoMapper(cfg);
        }
    }
}
