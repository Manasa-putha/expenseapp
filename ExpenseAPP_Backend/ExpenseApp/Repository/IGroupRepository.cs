using ExpenseApp.Models;

namespace ExpenseApp.Repository
{
   
    public interface IGroupRepository
    {
        Task<Group> GetGroupByIdAsync(int id);
        Task<List<Group>> GetAllGroupsAsync();
        Task AddGroupAsync(Group group);
        Task DeleteGroupAsync(Group group);
        Task AddExpenseAsync(Expense expense);
        Task SaveChangesAsync();
        Task<GroupUser> GetGroupUserByIdAsync(int userId, int groupId);
        Task<List<GroupUser>> GetUsersInGroupAsync(int groupId);
    }

}
