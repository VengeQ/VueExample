using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Domain.Repository.Quizes
{
    public class QuizContext : DbContext
    {
        public QuizContext(IOptions<QuizContextOptions> contextOptions)
        {
            _connectionString = contextOptions.Value.ConnectionString;
        }

        private readonly string _connectionString;

        public DbSet<QuizDto> Quizes { get; set; }

        public DbSet<QuizItemDto> QuizItems { get; set; }

        public DbSet<QuizStateDto> QuizStates { get; set; }

        public DbSet<GivenAnswerDto> GivenAnswers { get; set; }

        public DbSet<AnswerOptionDto> AnswerOptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                 //@"Host=host.docker.internal;Port=5455;Database=quizes;Username=postgres;Password=postgresPW")
                 //@"Host=localhost;Port=5455;Database=quizes;Username=postgres;Password=postgresPW")
                 _connectionString)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuizDto>().HasData(new QuizDto { Id = 1, Title = "Первая викторина" });

            modelBuilder.Entity<QuizItemDto>(
                entity =>
                {
                    entity.HasOne(d => d.QuizDto)
                        .WithMany(p => p.QuizItemDtos)
                        .HasForeignKey("QuizDtoId");
                });


            modelBuilder.Entity<QuizItemDto>().HasData(
                new QuizItemDto()
                {
                    Id = 1,
                    CorrectAnswerId = 2,
                    Question = "Когда я родился",
                    QuizDtoId = 1,
                });

            modelBuilder.Entity<AnswerOptionDto>(
                entity =>
                {
                    entity.HasOne(a => a.QuizItemDto)
                        .WithMany(p => p.AnswerOptions)
                        .HasForeignKey("QuizItemDtoId");
                });

            AnswerOptionDto[] answerOptions = [
                new AnswerOptionDto() { Id = 1, Answer = "1992", QuizItemDtoId = 1},
                new AnswerOptionDto() { Id = 2, Answer = "1991", QuizItemDtoId = 1 },
                new AnswerOptionDto() { Id = 3, Answer = "1994", QuizItemDtoId = 1 },
                new AnswerOptionDto() { Id = 4, Answer = "1995", QuizItemDtoId = 1 }
            ];


            modelBuilder.Entity<AnswerOptionDto>().HasData(answerOptions);
            // использование Fluent API
            //base.OnModelCreating(modelBuilder);
        }

        public string GetVersion()
        {
            var t = Database.SqlQueryRaw<string>($"SELECT VERSION()").AsEnumerable().First();
            return t.ToString();
        }

        internal async Task<QuizDto?> GetQuiz(int id)
        {
            var quiz = await Quizes.FirstOrDefaultAsync(q => q.Id == id);

            if (quiz == null)
            {
                return null;
            }

            return await Quizes.Include(q => q.QuizItemDtos).ThenInclude(a => a.AnswerOptions).FirstAsync(q => q.Id == id);
        }
    }
}
