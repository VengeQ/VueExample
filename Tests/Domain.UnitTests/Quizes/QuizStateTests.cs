using Domain.Quizes;

namespace Domain.UnitTests.Quizes
{
    public class QuizStateTests
    {
        [Test]
        public void Must_create_new_QuizState()
        {
            var answerOptions = new Dictionary<int, string>() { [1] = "First", [2] = "Second", [3] = "Third", [4] = "Fourth", };
            QuizItem[] quizItems = [new QuizItem(1, "testQuestion", answerOptions, 3)];
            var quiz = new Quiz(1, "test", quizItems)!;

            var state = new QuizState(Guid.Empty, quiz);

            Assert.Multiple(() =>
            {
                Assert.That(state.CurrentQuestion, Is.Zero);
                Assert.That(state.GivenAnswers, Is.Empty);
                Assert.That(state.IsDone, Is.False);
            });
        }

        [Test]
        public void Must_not_resume_invalid_QuizState()
        {
            var answerOptions = new Dictionary<int, string>() { [1] = "First", [2] = "Second", [3] = "Third", [4] = "Fourth", };
            QuizItem[] quizItems = [
                new QuizItem(1, "testQuestion1", answerOptions, 3),
                new QuizItem(2, "testQuestion2", answerOptions, 1),
                new QuizItem(3, "testQuestion3", answerOptions, 2),
            ];
            var quiz = new Quiz(1, "test", quizItems)!;

            Assert.Throws<InvalidOperationException>(() =>
            {
                QuizState.ResumeQuiz(Guid.Empty, quiz, new Dictionary<int, int>() { [1] = 2 }, 2);
            });
        }

        [Test]
        public void Must_not_resume_invalid_QuizState_2()
        {
            var answerOptions = new Dictionary<int, string>() { [1] = "First", [2] = "Second", [3] = "Third", [4] = "Fourth", };
            QuizItem[] quizItems = [
                new QuizItem(1, "testQuestion1", answerOptions, 3)
            ];
            var quiz = new Quiz(1, "test", quizItems)!;

            Assert.Throws<InvalidOperationException>(() =>
            {
                QuizState.ResumeQuiz(Guid.Empty, quiz, new Dictionary<int, int>() { [1] = 2, [2] = 3 }, 2);
            });
        }


        [Test]
        public void Must_resume_valid_QuizState()
        {
            var answerOptions = new Dictionary<int, string>() { [1] = "First", [2] = "Second", [3] = "Third", [4] = "Fourth", };
            QuizItem[] quizItems = [
                new QuizItem(1, "testQuestion1", answerOptions, 3),
                new QuizItem(2, "testQuestion2", answerOptions, 1),
            ];
            var quiz = new Quiz(1, "test", quizItems)!;

            var state = QuizState.ResumeQuiz(Guid.Empty, quiz, new Dictionary<int, int>() { [1] = 2 }, 1);

            Assert.Multiple(() =>
            {
                Assert.That(state.CurrentQuestion, Is.EqualTo(1));
                Assert.That(state.GivenAnswers, Is.EquivalentTo(new Dictionary<int, int>() { [1] = 2 }));
                Assert.That(state.IsDone, Is.False);
            });
        }
    }
}
