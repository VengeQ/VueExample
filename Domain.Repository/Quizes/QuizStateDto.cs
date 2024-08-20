using System.ComponentModel.DataAnnotations;

namespace Domain.Repository.Quizes
{
    public class QuizStateDto
    {
        [Key]
        public Guid Id { get; set; }

        public GivenAnswerDto[] GivenAnswers = null!;

        [Required]
        public int CurrentQuestion { get; set; }

        [Required]
        public int Quiz { get; set; }
    }
}
