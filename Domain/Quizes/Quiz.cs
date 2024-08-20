using System.Text.Json.Serialization;

namespace Domain.Quizes
{
    /// <summary>
    /// Викторина
    /// </summary>
    public class Quiz
    {
        [JsonConstructor]
        public Quiz() { }

        public int Id { get; init; }

        public Quiz(int id, string title, IEnumerable<QuizItem> items)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(title);
            ArgumentNullException.ThrowIfNull(items);

            if (!items.Any())
            {
                throw new ArgumentException(null, nameof(items));
            }

            if (items.Select(i => i.Id).Distinct().Count() != items.Count())
            {
                throw new ArgumentException("Quiz items should contains unique id", nameof(items));
            }

            Id = id;
            Title = title;
            Items = items.ToList();
        }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Title { get; init; } = null!;

        /// <summary>
        /// Элементы викторины
        /// </summary>
        public IReadOnlyList<QuizItem> Items { get; init; } = null!;

        /// <summary>
        /// Количество элементов
        /// </summary>
        public int QuizLength => Items.Count;
    }
}
