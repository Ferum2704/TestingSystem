using Domain.Entities;

namespace Application.Abstractions.IRepository
{
    public interface IQuestionRepository : IGenericRepository<Question>
    {
        void AddQuestionToTest(TestQuestion question);
    }
}
