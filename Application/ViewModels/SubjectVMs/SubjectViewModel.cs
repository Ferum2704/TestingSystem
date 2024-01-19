using Domain.Entities;

namespace Application.ViewModels.SubjectVMs
{
    public class SubjectViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IReadOnlyCollection<TopicViewModel> Topics { get; set; }
    }
}
