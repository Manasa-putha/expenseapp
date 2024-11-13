
using AutoMapper;
using ExpenseApp.Models.Dtos;
using ExpenseApp.Models;


public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Map User to UserDto
        CreateMap<User, UserDto>();

        // Map Group to GroupDto, including Members (GroupUsers -> Users)
        CreateMap<Group, GroupDto>()
            .ForMember(dest => dest.GroupUsers, opt => opt.MapFrom(src => src.GroupUsers.Select(gu => gu.User)));

        CreateMap<User, MemberDto>();
        CreateMap<GroupUser, UserDto>();


        // Map Expense to ExpenseDto
        CreateMap<Expense, ExpenseDto>()
            .ForMember(dest => dest.PaidById, opt => opt.MapFrom(src => src.PaidById))
            .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.GroupId)) 
            .ForMember(dest => dest.SplitAmong, opt => opt.MapFrom(src => src.SplitAmong)) 
            .ForMember(dest => dest.PaidByName, opt => opt.MapFrom(src => src.PaidBy.Name)); 


        
        CreateMap<UserExpense, UserExpenseDto>()
            .ForMember(dest => dest.MemberId, opt => opt.MapFrom(src => src.UserId)) 
            .ForMember(dest => dest.AmountOwed, opt => opt.MapFrom(src => src.AmountOwed)); 
        CreateMap<Expense, ExpenseDto>()
    .ForMember(dest => dest.PaidById, opt => opt.MapFrom(src => src.PaidById))
    .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.GroupId))
    .ForMember(dest => dest.SplitAmong, opt => opt.MapFrom(src => src.SplitAmong.Select(se => new UserExpenseDto
    {
        MemberId = se.UserId, 
        AmountOwed = se.AmountOwed
    }).ToList())) 
    .ForMember(dest => dest.PaidByName, opt => opt.MapFrom(src => src.PaidBy.Name));
        CreateMap<Group, GroupDto>()
            .ForMember(dest => dest.GroupUsers, opt => opt.MapFrom(src => src.GroupUsers.Select(gu => gu.User)))
            .ForMember(dest => dest.Expenses, opt => opt.MapFrom(src => src.Expenses)); 



    }
}

