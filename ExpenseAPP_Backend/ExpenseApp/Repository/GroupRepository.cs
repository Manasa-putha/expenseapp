using ExpenseApp.Data;
using ExpenseApp.Models;
using Microsoft.EntityFrameworkCore;



namespace ExpenseApp.Repository
{
   
    public class GroupRepository : IGroupRepository
    {
        private readonly AppDbContext _context;

        public GroupRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Group> GetGroupByIdAsync(int id)
        {
            return await _context.Groups
                .Include(g => g.Expenses)
                .ThenInclude(e => e.SplitAmong)
                .ThenInclude(s => s.User)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<List<Group>> GetAllGroupsAsync()
        {
            return await _context.Groups
                .Include(g => g.GroupUsers)
                .ThenInclude(gu => gu.User)
                .Include(g => g.Expenses)
                .ToListAsync();
        }

        public async Task AddGroupAsync(Group group)
        {
            await _context.Groups.AddAsync(group);
        }

        public async Task DeleteGroupAsync(Group group)
        {
            _context.Groups.Remove(group);
        }

        public async Task AddExpenseAsync(Expense expense)
        {
            await _context.Expenses.AddAsync(expense);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<GroupUser> GetGroupUserByIdAsync(int userId, int groupId)
        {
            return await _context.GroupUsers
                .FirstOrDefaultAsync(gu => gu.UserId == userId && gu.GroupId == groupId);
        }

        public async Task<List<GroupUser>> GetUsersInGroupAsync(int groupId)
        {
            return await _context.GroupUsers
                .Where(gu => gu.GroupId == groupId)
                .ToListAsync();
        }
    }

}
