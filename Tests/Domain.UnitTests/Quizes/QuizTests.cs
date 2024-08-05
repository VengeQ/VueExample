using Domain;
using Domain.Quizes;

namespace Domain.UnitTests.Quizes
{
    public class QuizTests
    {
        [Test]
        public void Must_not_create_with_null_title()
        {
            var item = new QuizItem(1u, "test", new Dictionary<int, string> { [1] = "One", [2] = "Two" }, 2);

            Assert.Throws<ArgumentNullException>(() =>
            {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                new Quiz(null, [item]);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            });
        }

        [Test, Sequential]
        public void Must_not_create_with_empty_title([Values("", "  ", "\t\n")] string title)
        {
            var item = new QuizItem(1u, "test", new Dictionary<int, string> { [1] = "One", [2] = "Two" }, 2);

            Assert.Throws<ArgumentException>(() =>
            {
                new Quiz(title, [item]);
            });
        }

        [Test]
        public void Must_not_create_with_null_items()
        {

            Assert.Throws<ArgumentNullException>(() =>
            {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                new Quiz("test", null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            });
        }

        [Test]
        public void Must_not_create_with_empty_items()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new Quiz("test", []);
            });
        }

        [Test]
        public void Must_not_create_with_dublicate_items()
        {
            var answerOptions = new Dictionary<int, string>() { [1] = "First", [2] = "Second", [3] = "Third", [4] = "Fourth", };
            QuizItem[] quizItems = [
                new QuizItem(1, "testQuestion1", answerOptions, 3),
                new QuizItem(1, "testQuestion2", answerOptions, 3),
            ];

            Assert.Throws<ArgumentException>(() =>
            {
                new Quiz("test", quizItems);
            });
        }

        [Test]
        public void Valid_Quiz_Must_Create()
        {
            var item = new QuizItem(1u, "test", new Dictionary<int, string> { [1] = "One", [2] = "Two" }, 2);
            var title = "test";

            var quiz = new Quiz(title, [item]);

            Assert.Multiple(() =>
            {
                Assert.That(quiz.Title, Is.EqualTo(title));
                Assert.That(quiz.Items[0], Is.EqualTo(item));
            });
        }
    }
}
