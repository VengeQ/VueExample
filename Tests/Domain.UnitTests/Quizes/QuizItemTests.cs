using Domain;
using Domain.Quizes;

namespace Domain.UnitTests.Quizes
{
    public class QuizItemTests
    {
        [Test]
        public void Must_not_create_null_QuizItem_question()
        {
            var answerOptions = new Dictionary<int, string>() { [1] = "First", [2] = "Second" };

            Assert.Throws<ArgumentNullException>(() =>
            {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                new QuizItem(1, null, answerOptions, 1);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            });
        }

        [Test, Sequential]
        public void Must_not_create_invalid_QuizItem_question([Values("", "  ", "\t\n")] string question)
        {
            var answerOptions = new Dictionary<int, string>() { [1] = "First", [2] = "Second" };

            Assert.Throws<ArgumentException>(() =>
            {
                new QuizItem(1, question, answerOptions, 1);
            });
        }

        [Test]
        public void Must_not_create_null_QuizItem_AnswerOptions()
        {
            Dictionary<int, string>? answerOptions = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
#pragma warning disable CS8604 // Possible null reference argument.
                new QuizItem(1, "my Questions", answerOptions, 1);
#pragma warning restore CS8604 // Possible null reference argument.
            });
        }

        [Test]
        public void Must_not_create_empty_QuizItem_AnswerOptions()
        {
            var answerOptions = new Dictionary<int, string>();

            Assert.Throws<ArgumentException>(() =>
            {
                new QuizItem(1, "my Questions", answerOptions, 1);
            });
        }

        [Test]
        public void Must_not_create_QuizItem_less_than_two_AnswerOptions()
        {
            var answerOptions = new Dictionary<int, string>() { [1] = "First" };

            Assert.Throws<ArgumentException>(() =>
            {
                new QuizItem(1, "my Questions", answerOptions, 1);
            });
        }

        [Test]
        public void Must_not_create_QuizItem_with_no_correct_answer()
        {
            var answerOptions = new Dictionary<int, string>() { [1] = "First" };

            Assert.Throws<ArgumentException>(() =>
            {
                new QuizItem(1, "my Questions", answerOptions, 2);
            });
        }

        [TestCase(1, "my Question", 3)]
        public void Valid_QuizItem_Must_create(int id, string question, int correctAnswer)
        {
            var answerOptions = new Dictionary<int, string>() { [1] = "First", [2] = "Second", [3] = "Third", [4] = "Fourth", };

            var quizItem = new QuizItem(id, question, answerOptions, correctAnswer);

            Assert.Multiple(() =>
            {
                Assert.That(quizItem.Id, Is.EqualTo(1));
                Assert.That(quizItem.Question, Is.EqualTo("my Question"));
                Assert.That(quizItem.CorrectAnswerId, Is.EqualTo(correctAnswer));
                Assert.That(quizItem.AnswerOptions.Count, Is.EqualTo(4));
                Assert.That(quizItem.CorrectAnswer, Is.EqualTo("Third"));
            });
        }
    }
}