using System.ComponentModel.DataAnnotations;

namespace Domain.Repository.Quizes
{
    public class QuizDto
    {
        [Key]
        public int QuizDtoId { get; set; }

        public string Title { get; set; } = null!;

        public List<QuizItemDto> QuizItemDtos { get; set; } = new List<QuizItemDto>();
    }
}
