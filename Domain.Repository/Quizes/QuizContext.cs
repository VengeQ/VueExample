using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Domain.Repository.Quizes
{
    public class QuizContext : DbContext
    {
        public QuizContext(IOptions<QuizContextOptions> contextOptions)
        {
            _connectionString = contextOptions.Value.ConnectionString;
        }

        protected readonly string _connectionString;

        public DbSet<QuizDto> Quizes { get; set; }

        public DbSet<QuizItemDto> QuizItems { get; set; }

        public DbSet<QuizStateDto> QuizStates { get; set; }

        public DbSet<GivenAnswerDto> GivenAnswers { get; set; }

        public DbSet<AnswerOptionDto> AnswerOptions { get; set; }

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
                });

            modelBuilder.Entity<AnswerOptionDto>(
                entity =>
                {
                    entity.HasOne(a => a.QuizItemDto)
                        .WithMany(p => p.AnswerOptions)
                        .HasForeignKey("QuizItemDtoId");
                });
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
    }
}
