namespace ExpenseApp.Models.Dtos
{
    public class ExpenseDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public int PaidById { get; set; }
        public string PaidByName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int GroupId { get; set; }
        public List<UserExpenseDto> SplitAmong { get; set; } = new();
        public bool IsSettled { get; set; }

    }
}
