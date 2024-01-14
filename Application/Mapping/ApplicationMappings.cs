using Application.DTOs;
using Application.Features.Subjects.Add;
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

            cfg.CreateMap<Topic, TopicViewModel>();
            cfg.CreateMap<Topic, TopicDTO>();
            cfg.CreateMap<AddTopicCommand, Topic>()
                .ForMember(dest => dest.Id, act => act.MapFrom(_ => Guid.NewGuid()))
                .ForMember(dest => dest.Subject, act => act.Ignore())
                .ForMember(dest => dest.Questions, act => act.Ignore())
                .ForMember(dest => dest.Tests, act => act.Ignore());
        }
    }
}
