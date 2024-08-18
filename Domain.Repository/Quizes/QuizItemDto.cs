using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Repository.Quizes
{
    public class QuizItemDto
    {
        [Key]
        public int QuizItemDtoId { get; set; }

        public string Question { get; set; } = null!;


        public List<AnswerOptionDto> AnswerOptions = null!;

        public int CorrectAnswerId { get; set; }

        public int QuizDtoId { get; set; }


        public QuizDto QuizDto { get; set; } = null!;
    }
}
