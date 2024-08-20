using System.Collections.Frozen;
using System.Text.Json.Serialization;

namespace Domain.Quizes
{

    /// <summary>
    /// Элемент викторины
    /// </summary>
    public class QuizItem
    {
        [JsonConstructor]
        public QuizItem() { }

        /// <summary>
        /// Идентификатор элемента
        /// </summary>
        public int Id { get; init; }

        /// <summary>
        /// Вопрос
        /// </summary>
        public string Question { get; init; } = null!;

        /// <summary>
        /// Варианты ответов
        /// </summary>
        public IReadOnlyDictionary<int, string> AnswerOptions { get; init; } = null!;

        /// <summary>
        /// Номер правильного ответа
        /// </summary>
        public int CorrectAnswerId { get; init; }

        /// <summary>
        /// Правильный ответ
        /// </summary>
        public string CorrectAnswer => AnswerOptions[CorrectAnswerId];

        /// <param name="id">Идентификатор элемента</param>
        /// <param name="question">Вопрос</param>
        /// <param name="answerOptions">Варианты ответов</param>
        /// <param name="correctAnswer">Правильный ответ</param>
        public QuizItem(int id, string question, IDictionary<int, string> answerOptions, int correctAnswer)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(question);
            ArgumentNullException.ThrowIfNull(answerOptions);

            if (answerOptions.Count < 2)
            {
                throw new ArgumentException("answerOptions must contains at least 2 elements", nameof(answerOptions));
            }

            if (!answerOptions.ContainsKey(correctAnswer))
            {
                throw new ArgumentException($"answerOptions keys should contains value of correctAnswer: {correctAnswer}", nameof(correctAnswer));
            }

            Id = id;
            AnswerOptions = answerOptions.ToFrozenDictionary();
            Question = question;
            CorrectAnswerId = correctAnswer;
        }
    }
}
