namespace Security
{
    /// <summary>
    /// Пользователи
    /// </summary>
    public class User
    {
        public User(int id, string name, string email, string role)
        {
            Id = id;
            Name = name;
            Email = email;
            Role = role;
        }

        public int Id { get; init; }

        public string Name { get; init; } 

        public string Email { get; set; }

        public string Role { get; set; }
    }
}
