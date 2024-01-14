using Domain.Entities;

namespace Application.ViewModels
{
    public class SubjectViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<Topic> Topics { get; set; }
    }
}
