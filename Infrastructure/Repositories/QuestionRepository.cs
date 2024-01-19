using Application.Abstractions.IRepository;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public void AddQuestionToTest(TestQuestion question)
        {
            context.TestsQuestion.Add(question);
        }
    }
}
