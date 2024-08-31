using Domain.Repository;
using Domain.Repository.Quizes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Domain.IntegrationTests
{
    internal class TestApplicationContext : ApplicationContext
    {
        public TestApplicationContext(IOptions<ApplicationContextOptions> contextOptions) : base(contextOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuizDto>().HasData(new QuizDto { Id = 1, Title = "Первая викторина" });
            modelBuilder.Entity<QuizDto>().HasData(new QuizDto { Id = 2, Title = "Вторая" });

            modelBuilder.Entity<QuizItemDto>().HasData(
                new QuizItemDto()
                {
                    Id = 1,
                    CorrectAnswerId = 2,
                    Question = "Когда я родился",
                    QuizDtoId = 1,
                });

            modelBuilder.Entity<QuizItemDto>().HasData(
                 new QuizItemDto()
                 {
                     Id = 2,
                     CorrectAnswerId = 6,
                     Question = "Как меня зовут",
                     QuizDtoId = 2,
                 });

            AnswerOptionDto[] answerOptions = [
                new AnswerOptionDto() { Id = 1, Answer = "1992", QuizItemDtoId = 1 },
                new AnswerOptionDto() { Id = 2, Answer = "1991", QuizItemDtoId = 1 },
                new AnswerOptionDto() { Id = 3, Answer = "1994", QuizItemDtoId = 1 },
                new AnswerOptionDto() { Id = 4, Answer = "1995", QuizItemDtoId = 1 }
            ];

            AnswerOptionDto[] answerOptions_2 = [
                new AnswerOptionDto() { Id = 5, Answer = "Вася", QuizItemDtoId = 2 },
                new AnswerOptionDto() { Id = 6, Answer = "Петя", QuizItemDtoId = 2 },
                new AnswerOptionDto() { Id = 7, Answer = "Олег", QuizItemDtoId = 2 },
                new AnswerOptionDto() { Id = 8, Answer = "Иван", QuizItemDtoId = 2 }
            ];

            modelBuilder.Entity<AnswerOptionDto>().HasData(answerOptions);
            modelBuilder.Entity<AnswerOptionDto>().HasData(answerOptions_2);
        }
    }
}
