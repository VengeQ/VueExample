using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// Викторина
    /// </summary>
    public class Quiz
    {
        public Quiz(string title, IEnumerable<QuizItem> items)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(title);
            ArgumentNullException.ThrowIfNull(items);

            if (!items.Any())
            {
                throw new ArgumentException(null, nameof(items));
            }

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
