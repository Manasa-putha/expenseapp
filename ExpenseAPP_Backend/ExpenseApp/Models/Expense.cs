namespace ExpenseApp.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public int PaidById { get; set; }
        public User PaidBy { get; set; }
        public DateTime Date { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public List<UserExpense> SplitAmong { get; set; } = new();
        public bool IsSettled { get; set; }
    }
}
