namespace Domain.Quizes
{
    /// <summary>
    /// Хранение состояния викторины
    /// </summary>
    public class QuizState
    {
        public QuizState(Guid id, Quiz quiz)
        {
            Id = id;
            _quiz = quiz;
            IsDone = false;
            _givenAnswers = new Dictionary<int, int>();
            CurrentQuestion = 0;
        }

        public QuizState(Guid id, Quiz quiz, bool isDone, IDictionary<int, int> givenAnswers, int currentQuestion) : this(id, quiz)
        {
            if (currentQuestion != givenAnswers.Count)
            {
                throw new InvalidOperationException("Current question should be equal to given answer counter");
            }

            IsDone = isDone;
            _givenAnswers = givenAnswers;
            CurrentQuestion = currentQuestion;
        }

        /// <summary>
        /// Идентификатор состояния
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Проходимая викторина
        /// </summary>
        private readonly Quiz _quiz;

        /// <summary>
        /// Закончено ли прохождение
        /// </summary>
        public bool IsDone { get; private set; }

        private readonly IDictionary<int, int> _givenAnswers;

        /// <summary>
        /// Данные ответы в викторине
        /// </summary>
        public IReadOnlyDictionary<int, int> GivenAnswers => _givenAnswers.AsReadOnly();

        /// <summary>
        /// Номер текущего вопроса
        /// </summary>
        public int CurrentQuestion { get; private set; }

        /// <summary>
        /// Дать ответа на текущий вопрос
        /// </summary>
        /// <param name="itemAnswerValue">Данный вариант ответа</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public bool MakeAnswer(int itemAnswerValue)
        {
            if (IsDone)
            {
                throw new InvalidOperationException();
            }

            var item = _quiz.Items[CurrentQuestion] ?? throw new InvalidOperationException();

            _givenAnswers.Add(CurrentQuestion, itemAnswerValue);
            CurrentQuestion++;

            if (CurrentQuestion == _givenAnswers.Count)
            {
                IsDone = true;
            }

            return item.CorrectAnswerId == itemAnswerValue;
        }
    }
}
