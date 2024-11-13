using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseApp.Models
{
    public class UserExpense
    {
        public int UserExpenseId { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User? User { get; set; }
        public int ExpenseId { get; set; }
        public Expense? Expense { get; set; }
        public decimal AmountOwed { get; set; }
        public bool IsSettled { get; set; }
    }
}
