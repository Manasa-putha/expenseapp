using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseApp.Models
{
    public class GroupUser
    {
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User? User { get; set; }
        [ForeignKey("GroupId")]
        public int GroupId { get; set; }

        public Group? Group { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool IsSettled { get; set; } = false;
    }
}
