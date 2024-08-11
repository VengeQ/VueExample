namespace Domain.Quizes
{
    /// <summary>
    /// Хранение состояния викторины
    /// </summary>
    public class QuizState
    {
        /// <summary>
        /// Запуск викторины с новым состоянием
        /// </summary>
        /// <param name="id">Идентификатор создаваемого викторин с состоянием</param>
        /// <param name="quiz">Викторина</param>
        public QuizState(Guid id, Quiz quiz)
        {
            Id = id;
            _quiz = quiz;
            IsDone = false;
            _givenAnswers = new Dictionary<int, int>();
            CurrentQuestion = 0;
        }

        private QuizState(Guid id, Quiz quiz, IDictionary<int, int> givenAnswers, int currentQuestion) : this(id, quiz)
        {
            if (givenAnswers.Count >= quiz.QuizLength)
            {
                throw new InvalidOperationException("Given answer count should be less than quiz length");
            }

            if (currentQuestion != givenAnswers.Count)
            {
                throw new InvalidOperationException("Current question should be equal to given answer counter");
            }

            IsDone = false;
            _givenAnswers = givenAnswers.ToDictionary();
            CurrentQuestion = currentQuestion;
        }

        /// <summary>
        /// Продолжить викторину
        /// </summary>
        /// <param name="id">Идентификатор викторины</param>
        /// <param name="quiz">Викторина</param>
        /// <param name="givenAnswers">Данные уже ответы</param>
        /// <param name="currentQuestion">текущий вопрос</param>
        public static QuizState ResumeQuiz(Guid id, Quiz quiz, IDictionary<int, int> givenAnswers, int currentQuestion)
        {
            return new QuizState(id, quiz, givenAnswers, currentQuestion);
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
