using Security;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Repository.Security
{
    public class UserDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; } = null!;

        [Column(TypeName = "varchar(400)")]
        public string Email { get; set; } = null!;

        [Column(TypeName = "varchar(20)")]
        public string Role { get; set; } = null!;

        [Column(TypeName = "varchar(100)")]
        public string Password { get; set; } = null!;

        internal User ToUser()
        {
            var user = new User(Id, Name, Email, Role);
            return user;
        }
    }
}
