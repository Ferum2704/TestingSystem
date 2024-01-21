using Application.Features.Questions.Add;
using Application.Features.Subjects.Add;
using Application.Features.Tests.Add;
using Application.Features.Topics.Add;
using Application.Mapping;
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
            cfg.CreateMap<SubjectModel, AddSubjectCommand>();
            cfg.CreateMap<TopicModel, AddTopicCommand>();
            cfg.CreateMap<QuestionModel, AddQuestionToTopicCommand>();
            cfg.CreateMap<TestModel, AddTestCommand>();
            cfg.CreateMap<TestQuestionModel, AddQuestionToTestCommand>();

            ApplicationMappings.ConfigureAutoMapper(cfg);
        }
    }
}
