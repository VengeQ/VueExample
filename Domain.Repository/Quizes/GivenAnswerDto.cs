using System.ComponentModel.DataAnnotations;

namespace Domain.Repository.Quizes
{
    public class GivenAnswerDto
    {
        [Key]
        public int QuestionId { get; set; }

        [Required]
        public int AnswerId { get; set; }
    }
}
