
using ExpenseApp.Data;
using ExpenseApp.Models;
using ExpenseApp.Models.Dtos;
using ExpenseApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class GroupController : ControllerBase
{
    private readonly IGroupService _groupService;
    private readonly AppDbContext _context;
    public GroupController(IGroupService groupService,AppDbContext context)
    {
        _groupService = groupService;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGroups()
    {
        try
        {
            var groups = await _groupService.GetAllGroupsAsync();
            return Ok(groups);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("group/{id}")]
    public async Task<IActionResult> GetGroup(int id)
    {
        try
        {
            var group = await _groupService.GetGroupByIdAsync(id);
            if (group == null)
                return NotFound("Group not found");


            return Ok(group);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    //[HttpPost]
    //public async Task<IActionResult> CreateGroup([FromBody] GroupDto groupDto)
    //{
    //    if (groupDto == null)
    //        return BadRequest("Invalid data");

    //    await _groupService.CreateGroupAsync(groupDto);
    //    return Ok(groupDto);
    //}
    [HttpPost]
    public async Task<IActionResult> CreateGroup([FromBody] GroupDto groupDto)
    {
        if (groupDto == null)
        {
            return BadRequest("Invalid group data.");
        }
        try
        {

            var group = new Group
        {
            Name = groupDto.Name,
            Description = groupDto.Description,
            CreatedDate = groupDto.CreatedDate,
            GroupUsers = new List<GroupUser>()
        };

 
        await _context.Groups.AddAsync(group);
        await _context.SaveChangesAsync(); 

        foreach (var userDto in groupDto.GroupUsers)
        {
           
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userDto.Email);
            if (user == null)
            {
                return BadRequest($"User with email {userDto.Email} not found.");
            }

           
            var groupUser = new GroupUser
            {
                UserId = user.Id, 
                Email = user.Email, 
                GroupId = group.Id 
            };

            // Check if this GroupUser entry already exists
            var existingGroupUser = await _context.GroupUsers
                .AnyAsync(gu => gu.UserId == groupUser.UserId && gu.GroupId == groupUser.GroupId);

            // Only add if the user is not already in the group
            if (!existingGroupUser)
            {
                group.GroupUsers.Add(groupUser); // Add the new GroupUser to the group's collection
            }
        }

       
        if (group.GroupUsers.Any())
        {
            await _context.GroupUsers.AddRangeAsync(group.GroupUsers);
            await _context.SaveChangesAsync(); 
        }

        return Ok(groupDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGroup(int id)
    {
        try
        {
            await _groupService.DeleteGroupAsync(id);
        return Ok("Group deleted");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("{groupId}/add-expense")]
    public async Task<IActionResult> AddExpense([FromBody] ExpenseDto expenseDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            await _groupService.AddExpenseAsync(expenseDto);
        return Ok(expenseDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }

    }

    //[HttpPatch("{groupId}/settle-expense/{expenseId}")]
    //public async Task<IActionResult> SettleExpense(int groupId, int expenseId)
    //{
    //    await _groupService.SettleExpenseAsync(groupId, expenseId);
    //    return Ok("Expense settled");
    //}
    [HttpPatch("{groupId}/settle-expense/{expenseId}")]
    public async Task<IActionResult> SettleExpense(int groupId, int expenseId)
    {
        try
        {
            // Fetch the user expense records
            var userExpenses = await _context.UserExpenses
            .Where(ue => ue.ExpenseId == expenseId && ue.Expense.GroupId == groupId)
            .ToListAsync();

            if (userExpenses == null || !userExpenses.Any())
                return NotFound("Expense or group not found.");

            // Fetch the expense record
            var expense = await _context.Expenses
                .FirstOrDefaultAsync(e => e.Id == expenseId && e.GroupId == groupId);

            if (expense == null)
                return NotFound("Expense not found.");

            // Mark each user expense as settled
            foreach (var userExpense in userExpenses)
            {
                userExpense.IsSettled = true;
            }

            // Mark the expense itself as settled
            expense.IsSettled = true;

            // Save changes to the database
            await _context.SaveChangesAsync();

            //return Ok("Expense settled for all users and the expense itself.");
            return Ok(new { message = "Expense settled for all users and the expense itself." });

        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    //[HttpGet("group/{userId}/groups")]
    //public async Task<IActionResult> GetUserGroups(int userId)
    //{
    //    var userGroups = await _groupService.GetUserGroupsAsync(userId);
    //    if (userGroups == null || !userGroups.Any())
    //        return NotFound("No groups found for this user");

    //    return Ok(userGroups);
    //}
    // GET: api/users/{userId}/groups
    [HttpGet("group/{userId}/groups")]
    public async Task<IActionResult> GetUserGroups(int userId)
    {
        try
        {
            // Fetch groups associated with the user
            var userGroups = await _context.GroupUsers
            .Where(gu => gu.UserId == userId)
            .Select(gu => new GroupDto
            {
                Id = gu.GroupId,
                Name = gu.Group.Name,
                Description = gu.Group.Description,
                Email = gu.Group.Email,
                CreatedDate = gu.Group.CreatedDate,
                GroupUsers = gu.Group.GroupUsers.Select(g => new UserDto
                {
                    Id = g.User.Id,
                    Name = g.User.Name,
                    Email = g.User.Email,
                    MobileNumber = g.User.MobileNumber
                }).ToList(),
                Expenses = gu.Group.Expenses.Select(e => new ExpenseDto
                {
                    Id = e.Id,
                    Description = e.Description,
                    Amount = e.Amount,
                    PaidById = e.PaidById,
                    PaidByName = e.PaidBy.Name,
                    Date = e.Date,
                    GroupId = e.GroupId,
                    IsSettled = e.IsSettled,
                    SplitAmong = e.SplitAmong.Select(se => new UserExpenseDto
                    {
                        MemberId = se.UserId,
                        AmountOwed = se.AmountOwed
                    }).ToList()
                }).ToList()
            })
            .ToListAsync();

        if (userGroups == null || !userGroups.Any())
            return NotFound("No groups found for this user.");

        return Ok(userGroups);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
   

}
