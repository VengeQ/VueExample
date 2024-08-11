using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Quizes
{
    /// <summary>
    /// Викторина
    /// </summary>
    public class Quiz
    {
        public int Id { get; }

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
        public string Title { get; }

        /// <summary>
        /// Элементы викторины
        /// </summary>
        public IReadOnlyList<QuizItem> Items { get; }

        /// <summary>
        /// Количество элементов
        /// </summary>
        public int QuizLength => Items.Count;
    }
}
