using Domain.Repository.Quizes;
using Domain.Repository.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Domain.Repository
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(IOptions<ApplicationContextOptions> contextOptions)
        {
            _connectionString = contextOptions.Value.ConnectionString;
        }

        protected readonly string _connectionString;

        public DbSet<QuizDto> Quizes { get; set; }

        public DbSet<QuizItemDto> QuizItems { get; set; }

        public DbSet<QuizStateDto> QuizStates { get; set; }

        public DbSet<GivenAnswerDto> GivenAnswers { get; set; }

        public DbSet<AnswerOptionDto> AnswerOptions { get; set; }

        public DbSet<UserDto> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuizItemDto>(
                entity =>
                {
                    entity.HasOne(d => d.QuizDto)
                        .WithMany(p => p.QuizItemDtos)
                        .HasForeignKey("QuizDtoId");
                }
            );

            modelBuilder.Entity<AnswerOptionDto>(
                entity =>
                {
                    entity.HasOne(a => a.QuizItemDto)
                        .WithMany(p => p.AnswerOptions)
                        .HasForeignKey("QuizItemDtoId");
                }
            );

            modelBuilder.Entity<UserDto>(
                entity => entity.HasIndex(u => u.Name).IsUnique()
            );

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

            modelBuilder.Entity<UserDto>().HasData(new UserDto { Id = 1, Name = "test", Email = "test@test.test", Password ="test", Role = "test" });
        }

        public string GetVersion()
        {
            var t = Database.SqlQueryRaw<string>($"SELECT VERSION()").AsEnumerable().First();
            return t.ToString();
        }

        public async Task<QuizDto?> GetQuiz(int id)
        {
            var quiz = await Quizes.FirstOrDefaultAsync(q => q.Id == id);

            if (quiz == null)
            {
                return null;
            }

            return await Quizes.Include(q => q.QuizItemDtos).ThenInclude(a => a.AnswerOptions).FirstAsync(q => q.Id == id);
        }

        public async Task<QuizDto[]> Get()
        {
            var result = await Quizes.Include(q => q.QuizItemDtos).ThenInclude(a => a.AnswerOptions).ToArrayAsync();//.AsAsyncEnumerable();
            return result;
        }
    }
}
