using Domain.Entities;

namespace Application.Abstractions
{
    public interface IQuestionsFileService
    {
        void ParseTopicQuestions(string topicName, IReadOnlyCollection<Question> questions);

        void WriteTopicQuestion(string topicName, Question question);
    }
}
