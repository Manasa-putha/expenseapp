namespace ExpenseApp.Models
{

    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public ICollection<GroupUser> GroupUsers { get; set; }
        public ICollection<Expense>? Expenses { get; set; }
        public bool IsSettled { get; set; } = false;
    }

}
