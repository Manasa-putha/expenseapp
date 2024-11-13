

using AutoMapper;
using ExpenseApp.Models;
using ExpenseApp.Models.Dtos;
using ExpenseApp.Repository;
namespace ExpenseApp.Services

{
  
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public GroupService(IGroupRepository groupRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        public async Task<List<GroupDto>> GetAllGroupsAsync()
        {
            var groups = await _groupRepository.GetAllGroupsAsync();
            return _mapper.Map<List<GroupDto>>(groups);
        }

        public async Task<GroupDto> GetGroupByIdAsync(int id)
        {
            var group = await _groupRepository.GetGroupByIdAsync(id);
            return _mapper.Map<GroupDto>(group);
        }

        public async Task CreateGroupAsync(GroupDto groupDto)
        {
            var group = new Group
            {
                Name = groupDto.Name,
                Description = groupDto.Description,
                CreatedDate = groupDto.CreatedDate,
                GroupUsers = new List<GroupUser>()
            };

            await _groupRepository.AddGroupAsync(group);
            await _groupRepository.SaveChangesAsync();

            foreach (var userDto in groupDto.GroupUsers)
            {
                var groupUser = new GroupUser
                {
                    GroupId = group.Id,
                    UserId = userDto.Id
                };

                group.GroupUsers.Add(groupUser);
            }

            if (group.GroupUsers.Any())
            {
                await _groupRepository.SaveChangesAsync();
            }
        }

        public async Task DeleteGroupAsync(int groupId)
        {
            var group = await _groupRepository.GetGroupByIdAsync(groupId);

            if (group != null)
            {
                await _groupRepository.DeleteGroupAsync(group);
                await _groupRepository.SaveChangesAsync();
            }

        }

        public async Task AddExpenseAsync(ExpenseDto expenseDto)
        {
            var expense = new Expense
            {
                Description = expenseDto.Description,
                Amount = expenseDto.Amount,
                Date = expenseDto.Date,
                PaidById = expenseDto.PaidById,
                GroupId = expenseDto.GroupId,
                SplitAmong = expenseDto.SplitAmong.Select(s => new UserExpense
                {
                    UserId = s.MemberId,
                    AmountOwed = s.AmountOwed
                }).ToList()
            };

            await _groupRepository.AddExpenseAsync(expense);
            await _groupRepository.SaveChangesAsync();
        }

        public async Task<List<GroupDto>> GetUserGroupsAsync(int userId)
        {
            var userGroups = await _groupRepository.GetUsersInGroupAsync(userId);

           
            var groupDtos = userGroups.Select(gu => gu.Group).ToList();

            return _mapper.Map<List<GroupDto>>(groupDtos);
        }


        public async Task SettleExpenseAsync(int groupId, int expenseId)
        {
            var userExpenses = await _groupRepository.GetUsersInGroupAsync(expenseId);

            if (userExpenses.Any())
            {
                foreach (var userExpense in userExpenses)
                {
                    userExpense.IsSettled = true;
                }

                var expense = await _groupRepository.GetGroupByIdAsync(expenseId);
                expense.IsSettled = true;
                await _groupRepository.SaveChangesAsync();
            }
        }
    }

}
