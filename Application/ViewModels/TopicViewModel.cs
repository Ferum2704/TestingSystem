using Domain.Entities;

namespace Application.ViewModels
{
    public class TopicViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public IReadOnlyCollection<TopicTestViewModel> Tests { get; set; }

        public IReadOnlyCollection<TopicQuestionViewModel> Questions { get; set; }
    }
}
