using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class QuizState
    {
        private Quiz _quiz;

        private IDictionary<int, int> _givenAnswers;


        public bool IsDone { get; private set; }

        public IReadOnlyDictionary<int, int> GivenAnswers => _givenAnswers.AsReadOnly();

        public int CurrentQuestion { get; private set; }

        public bool MakeAnswer(int itemAnswerValue)
        {
            var item = _quiz.Items[CurrentQuestion] ?? throw new InvalidOperationException();

            _givenAnswers.Add(CurrentQuestion, itemAnswerValue);
            CurrentQuestion++;

            if (CurrentQuestion == _givenAnswers.Count)
            {
                this.IsDone = true;
            }

            return item.CorrectAnswerId == itemAnswerValue;

        }
    }
}
