using Domain.Quizes;

namespace Domain.Services.Quizes
{
    public class QuizService : IQuizService
    {
        public Quiz? GetQuiz(int id)
        {
            if (id != 1)
            {
                return null;
            }

            var item1 = new QuizItem(1u, "test", new Dictionary<int, string> { [1] = "One", [2] = "Two" }, 2);
            var item2 = new QuizItem(2u, "test", new Dictionary<int, string> { [1] = "One", [2] = "Two" }, 1);
            var item3 = new QuizItem(3u, "test", new Dictionary<int, string> { [1] = "One", [2] = "Two" }, 2);
            var title = "test";
            
            return new Quiz(1, title, [item1, item2, item3]);
        }
    }
}
