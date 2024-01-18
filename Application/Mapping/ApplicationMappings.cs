using Application.DTOs;
using Application.Features.Questions.Add;
using Application.Features.Subjects.Add;
using Application.Features.Tests.Add;
using Application.Features.Topics.Add;
using Application.ViewModels;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public static class ApplicationMappings
    {
        public static void ConfigureAutoMapper(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Subject, SubjectViewModel>();
            cfg.CreateMap<Subject, SubjectDTO>();
            cfg.CreateMap<AddSubjectCommand, Subject>()
                .ForMember(dest => dest.Id, act => act.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.Teacher, act => act.Ignore())
                .ForMember(dest => dest.Topics, act => act.Ignore());

            cfg.CreateMap<Topic, SubjectTopicViewModel>();
            cfg.CreateMap<Topic, TopicDTO>();
            cfg.CreateMap<Topic, TopicViewModel>();
            cfg.CreateMap<AddTopicCommand, Topic>()
                .ForMember(dest => dest.Id, act => act.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.Subject, act => act.Ignore())
                .ForMember(dest => dest.Questions, act => act.Ignore())
                .ForMember(dest => dest.Tests, act => act.Ignore());

            cfg.CreateMap<Test, TopicTestViewModel>();

            cfg.CreateMap<Question, TopicQuestionViewModel>();
            cfg.CreateMap<Question, QuestionDTO>();
            cfg.CreateMap<AddQuestionToTopicCommand, Question>()
                .ForMember(dest => dest.Id, act => act.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.Topic, act => act.Ignore())
                .ForMember(dest => dest.Results, act => act.Ignore())
                .ForMember(dest => dest.TestQuestions, act => act.Ignore())
                .ForMember(dest => dest.Tests, act => act.Ignore());
            cfg.CreateMap<AddQuestionToTestCommand, TestQuestion>()
                .ForMember(dest => dest.Id, act => act.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.Test, act => act.Ignore())
                .ForMember(dest => dest.Question, act => act.Ignore());

            cfg.CreateMap<Test, TestDTO>();
            cfg.CreateMap<AddTestCommand, Test>()
                .ForMember(dest => dest.Id, act => act.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.Topic, act => act.Ignore())
                .ForMember(dest => dest.Students, act => act.Ignore())
                .ForMember(dest => dest.TestQuestions, act => act.Ignore())
                .ForMember(dest => dest.Questions, act => act.Ignore())
                .ForMember(dest => dest.StudentsAttempts, act => act.Ignore());
        }
    }
}
