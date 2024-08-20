using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Repository.Quizes
{
    public class AnswerOptionDto
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(200)")]
        [Required]
        public string Answer { get; set; } = null!;

        public QuizItemDto QuizItemDto { get; set; } = null!;

        public int QuizItemDtoId { get; set; }
    }
}
