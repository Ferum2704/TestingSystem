using Application.DTOs.Enums;

namespace Application.ViewModels
{
    public class StudentTestResultViewModel
    {
        public Guid Id { get; set; }

        public Guid QuestionId { get; set; }

        public AnswerOptionDTO? Answer { get; set; }

        public bool IsCorrect { get; set; }
    }
}
