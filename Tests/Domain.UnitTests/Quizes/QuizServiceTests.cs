using Domain.Quizes;
using Domain.Services.Quizes;
using FakeItEasy;

namespace Domain.UnitTests.Quizes
{
    public class QuizServiceTests
    {

        [Test, Sequential]
        public async Task get_invoke_only_once([Values(1, 2, 3)] int id)
        {
            var fakeRepository = A.Fake<IQuizRepository>(ro => ro.Strict());
            A.CallTo(() => fakeRepository.Get(id)).Returns<Quiz?>(null);
            var fakeService = new QuizService(fakeRepository);

            var quiz = await fakeService.Get(id);

            A.CallTo(() => fakeRepository.Get(id)).MustHaveHappenedOnceExactly();
            A.CallTo(() => fakeRepository.Get(A<int>._)).MustHaveHappenedOnceExactly();
        }

        [Test, Sequential]
        public async Task get_valid_quiz([Values(1, 1000, 10000)] int id)
        {
            var fakeRepository = A.Fake<IQuizRepository>(ro => ro.Strict());
            var fakeQuiz = CreateQuiz(id);
            A.CallTo(() => fakeRepository.Get(id)).Returns(fakeQuiz);
            var fakeService = new QuizService(fakeRepository);

            var quiz = await fakeService.Get(id);

            Assert.That(quiz, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(quiz.Id, Is.EqualTo(fakeQuiz.Id));
                Assert.That(quiz.Title, Is.EqualTo(fakeQuiz.Title));
            });
        }

        [Test, Sequential]
        public async Task get_not_existed_quiz([Values(1, 2, 3)] int id)
        {
            var fakeRepository = A.Fake<IQuizRepository>(ro => ro.Strict());
            A.CallTo(() => fakeRepository.Get(id)).Returns<Quiz?>(null);
            var fakeService = new QuizService(fakeRepository);

            var quiz = await fakeService.Get(id);

            Assert.That(quiz, Is.Null);
        }

        private Quiz CreateQuiz(int quizId)
        {
            var item = new QuizItem(1, "test", new Dictionary<int, string> { [1] = "One", [2] = "Two" }, 2);
            var quiz = new Quiz(quizId, "test", [item]);

            return quiz;
        }
    }
}
