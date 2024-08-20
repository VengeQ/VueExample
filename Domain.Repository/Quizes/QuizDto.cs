using Domain.Quizes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Repository.Quizes
{
    public class QuizDto
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Title { get; set; } = null!;

        public List<QuizItemDto> QuizItemDtos { get; set; } = [];

        internal Quiz ToQuiz() => new Quiz(Id, Title, QuizItemDtos.Select(item => item.ToQuizItem()));
    }
}
