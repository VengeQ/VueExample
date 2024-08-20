using Domain.Quizes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;

namespace Domain.Repository.Quizes
{
    public class QuizItemDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Question { get; set; } = null!;

        public List<AnswerOptionDto> AnswerOptions = null!;

        [Required]
        public int CorrectAnswerId { get; set; }

        public int QuizDtoId { get; set; }

        public QuizDto QuizDto { get; set; } = null!;

        internal QuizItem ToQuizItem()
        {
            return new QuizItem(
                QuizDtoId,
                Question,
                ConvertAnswerOptions(AnswerOptions),
                CorrectAnswerId
            );
        }

        private static SortedDictionary<int, string> ConvertAnswerOptions(IEnumerable<AnswerOptionDto> answerOptions)
        {
            var dictionary = new SortedDictionary<int, string>();
            foreach (var answerOptionDto in answerOptions)
            {
                dictionary[answerOptionDto.Id] = answerOptionDto.Answer;
            }
            return dictionary;
        }
    }
}
