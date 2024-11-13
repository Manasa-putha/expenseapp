

using ExpenseApp.Models;
using ExpenseApp.Models.Dtos;


namespace ExpenseApp.Services

{
   
    public interface IGroupService
    {
        Task<GroupDto> GetGroupByIdAsync(int id);
        Task<List<GroupDto>> GetAllGroupsAsync();
        Task CreateGroupAsync(GroupDto groupDto);
        Task DeleteGroupAsync(int groupId);
        Task AddExpenseAsync(ExpenseDto expenseDto);
        Task<List<GroupDto>> GetUserGroupsAsync(int userId);
        Task SettleExpenseAsync(int groupId, int expenseId);
    }

}
