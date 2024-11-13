namespace ExpenseApp.Models.Dtos
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public List<UserDto> GroupUsers { get; set; } = new();
        public List<ExpenseDto> Expenses { get; set; } = new();
    }

}
