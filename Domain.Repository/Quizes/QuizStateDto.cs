using System.ComponentModel.DataAnnotations;

namespace Domain.Repository.Quizes
{
    public class QuizStateDto
    {
        [Key]
        public Guid Id { get; set; }

        public GivenAnswerDto[] GivenAnswers = null!;

        public int CurrentQuestion { get; set; }

        public int Quiz { get; set; }
    }
}
