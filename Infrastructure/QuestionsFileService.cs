using Application.Abstractions;
using Application.Utilities;
using Domain.Entities;
using System.Reflection;

namespace Infrastructure
{
    public class QuestionsFileService : IQuestionsFileService
    {
        private const string FolderName = "Topics";

        private string ExecutionFolderPath => Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        public void ParseTopicQuestions(string topicName, IReadOnlyCollection<Question> questions)
        {
            topicName.NotNull(nameof(topicName));
            questions.NotNull(nameof(questions));
            var filePath = $"{ExecutionFolderPath}\\{FolderName}\\{topicName}.txt";

            if (File.Exists(filePath))
            {
                var content = File.ReadAllText(filePath);
                var questionOptionsBlocks = content.Split(new string[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var block in questionOptionsBlocks)
                {
                    var lines = block.Split('\n');
                    var title = lines[0];
                    var question = questions.FirstOrDefault(x => x.Text == title);
                    if (question != null)
                    {
                        for (int i = 1; i < lines.Length; i++)
                        {
                            var line = lines[i].Trim();

                            switch (line[..2])
                            {
                                case "а)":
                                    question.OptionA = line[2..].Trim();
                                    break;
                                case "b)":
                                    question.OptionB = line[2..].Trim();
                                    break;
                                case "c)":
                                    question.OptionC = line[2..].Trim();
                                    break;
                            }
                        }
                    }
                }
            }
        }

        public void WriteTopicQuestion(string topicName, Question question)
        {
            var folderPath = Path.Combine(ExecutionFolderPath, FolderName);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var content = FormatQuestion(question);
            var filePath = Path.Combine(folderPath, $"{topicName}.txt");

            File.AppendAllText(filePath, content);
        }

        private string FormatQuestion(Question question)
        {
            return $"{question.Text}\n" +
                   $"а) {question.OptionA}\n" +
                   $"b) {question.OptionB}\n" +
                   $"c) {question.OptionC}\n\n";
        }
    }
}
