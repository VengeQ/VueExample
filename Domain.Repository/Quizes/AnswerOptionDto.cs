using System.ComponentModel.DataAnnotations;

namespace Domain.Repository.Quizes
{
    public class AnswerOptionDto
    {
        [Key]
        public int Id { get; set; }

        public string Answer { get; set; } = null!;

        public QuizItemDto QuizItemDto { get; set; } = null!;

        public int QuizItemDtoId { get; set; }
    }
}
