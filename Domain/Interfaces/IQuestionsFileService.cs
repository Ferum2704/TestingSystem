using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IQuestionsFileService
    {
        void ParseTopicQuestions (string topicName, IReadOnlyCollection<Question> questions);

        void WriteTopicQuestion(string topicName, Question question);
    }
}
