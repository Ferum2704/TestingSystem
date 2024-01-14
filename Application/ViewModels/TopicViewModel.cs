using Domain.Entities;

namespace Application.ViewModels
{
    public class TopicViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public ICollection<TopicTestViewModel> Tests { get; set; }

        public ICollection<TopicQuestionViewModel> Questions { get; set; }
    }
}
