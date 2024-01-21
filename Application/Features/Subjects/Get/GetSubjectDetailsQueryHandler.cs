using Application.Abstractions;
using Application.Identitity;
using Application.Utilities;
using Application.ViewModels.SubjectVMs;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Subjects.Get
{
    public class GetSubjectDetailsQueryHandler : IRequestHandler<GetSubjectDetailsQuery, SubjectViewModel>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ICurrentUserService currentUserService;
        private readonly IQuestionsFileService questionFileService;

        public GetSubjectDetailsQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ICurrentUserService currentUserService,
            IQuestionsFileService questionFileService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.currentUserService = currentUserService;
            this.questionFileService = questionFileService;
        }

        public async Task<SubjectViewModel?> Handle(GetSubjectDetailsQuery request, CancellationToken cancellationToken)
        {
            request.NotNull(nameof(request));

            var subject = await unitOfWork.SubjectRepository.GetByIdAsync(request.Id);

            if (subject == null)
            {
                return null;
            }

            await subject.Topics.ForEachAsync(MapTestsAndQuestionsAsync);

            var subjectViewModel = mapper.Map<SubjectViewModel>(subject);

            return subjectViewModel;
        }

        private async Task MapTestsAndQuestionsAsync(Topic topic)
        {

            await MapQuestions(topic);
            await MapTests(topic);
        }

        private async Task MapQuestions(Topic topic)
        {
            if (currentUserService.IsInRole(ApplicationUserRole.Teacher))
            {
                topic.Questions = await unitOfWork.QuestionRepository.GetAsync(x => x.TopicId == topic.Id);
                questionFileService.ParseTopicQuestions(topic.Title, topic.Questions);
            }
        }

        private async Task MapTests(Topic topic)
        {
            if (currentUserService.IsInRole(ApplicationUserRole.Student))
            {
                var domainUser = await currentUserService.GetCurrentDomainUserAsync();
                var student = await unitOfWork.StudentRepository.GetByIdAsync(domainUser.Id);
                var studentTestAttemptsIds = student.TestsAttempts.Select(x => x.TestId).Distinct().ToList();

                topic.Tests = await unitOfWork.TestRepository.GetAsync(x => x.TopicId == topic.Id && studentTestAttemptsIds.Contains(x.Id));
            }
            else
            {
                topic.Tests = await unitOfWork.TestRepository.GetAsync(x => x.TopicId == topic.Id);
            }
        }
    }
}
