using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
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

        public string Title { get; }

        public IReadOnlyList<QuizItem> Items { get; }

        public int QuizLength => Items.Count;
    }
}
