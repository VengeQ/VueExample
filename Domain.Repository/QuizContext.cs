using Microsoft.EntityFrameworkCore;


namespace Domain.Repository
{
    public class QuizContext : DbContext
    {
        //public DbSet<Quiz> Quizes { get; set; }

        //public DbSet<QuizItem> QuizItems { get; set; }

        //public DbSet<QuizState> QuizStates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                @"Host=host.docker.internal;Port=5455;Database=postgres;Username=postgres;Password=postgresPW");
        }

        public string GetVersion()
        {
            var t = Database.SqlQueryRaw<string>($"SELECT VERSION()").AsEnumerable().First();
            return t.ToString();
        }
    }

    public class Quiz
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public List<QuizItem> Items { get; set; } = null!;
    }

    public class QuizItem
    {
        public uint Id { get; set; }
        public string Question { get; set; } = null!;

        public Dictionary<int, string> AnswerOptions { get; set; } = null!;
        public int CorrectAnswerId { get; set; }
    }

    public class QuizState
    {
        public Guid Id { get; set; }

        public Dictionary<int, int> GivenAnswers { get; set; } = null!;

        public int CurrentQuestion { get; set; }

        public int Quiz { get; set; }
    }
}
