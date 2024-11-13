
using Microsoft.EntityFrameworkCore;
using ExpenseApp.Models;
using Microsoft.Data.SqlClient;

namespace ExpenseApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<UserExpense> UserExpenses { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<GroupUser>()
                .HasKey(gu => new { gu.UserId, gu.GroupId });

            modelBuilder.Entity<GroupUser>()
                .HasOne(gu => gu.User)
                .WithMany(u => u.GroupUsers)
                .HasForeignKey(gu => gu.UserId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<GroupUser>()
                .HasOne(gu => gu.Group)
                .WithMany(g => g.GroupUsers)
                .HasForeignKey(gu => gu.GroupId)
                .OnDelete(DeleteBehavior.Restrict); 

           
            modelBuilder.Entity<Expense>()
                .HasMany(e => e.SplitAmong)
                .WithOne(ue => ue.Expense)
                .HasForeignKey(ue => ue.ExpenseId)
                .OnDelete(DeleteBehavior.Cascade);

            // UserExpense relationship
            modelBuilder.Entity<UserExpense>()
                .HasOne(ue => ue.User)
                .WithMany()
                .HasForeignKey(ue => ue.UserId)
                .OnDelete(DeleteBehavior.Restrict); 
            modelBuilder.Entity<Group>()
         .HasMany(g => g.GroupUsers)
         .WithOne(gu => gu.Group)
         .HasForeignKey(gu => gu.GroupId)
         .OnDelete(DeleteBehavior.Cascade);

            // Seeding initial data
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Admin", Email = "admin@gmail.com", MobileNumber = "1234567890", UserType = UserType.ADMIN_USER, Password = "admin1234", CreatedOn = DateTime.Now },
                new User { Id = 2, Name = "Sai", Email = "sai@gmail.com", MobileNumber = "1234567890", UserType = UserType.USER, Password = "sai1234", CreatedOn = DateTime.Now },
                new User { Id = 3, Name = "Manu", Email = "manu@gmail.com", MobileNumber = "1234567890", UserType = UserType.USER, Password = "manu4321", CreatedOn = DateTime.Now },
                new User { Id=  4,  Name="Ramu",Email="ramu@gmail.com",MobileNumber ="123456789",UserType=UserType.USER,Password="ramu4321",CreatedOn= DateTime.Now},
                new User { Id = 5, Name = "Raj", Email = "raj@gmail.com", MobileNumber = "1234567890", UserType = UserType.USER, Password = "raj1234", CreatedOn = DateTime.Now },
                new User { Id = 6, Name = "Mice", Email = "mice@gmail.com", MobileNumber = "1234567890", UserType = UserType.USER, Password = "sai1234", CreatedOn = DateTime.Now },
                new User { Id = 7, Name = "Kin", Email = "kin@gmail.com", MobileNumber = "1234567890", UserType = UserType.USER, Password = "kin1234", CreatedOn = DateTime.Now },
                new User { Id = 8, Name = "Kapil", Email = "kapil@gmail.com", MobileNumber = "1234567890", UserType = UserType.USER, Password = "kapil1234", CreatedOn = DateTime.Now },
                new User { Id = 9, Name = "Kesav", Email = "kesav@gmail.com", MobileNumber = "1234567890", UserType = UserType.USER, Password = "kesav1234", CreatedOn = DateTime.Now },
                new User { Id = 10, Name = "Bob", Email = "Bob@gmail.com", MobileNumber = "1234567890", UserType = UserType.USER, Password = "bob1234", CreatedOn = DateTime.Now }
            );

            modelBuilder.Entity<Group>().HasData(
                new Group { Id = 1, Name = "Roommates", Description = "Expenses shared among roommates", CreatedDate = DateTime.Now },
                new Group { Id = 2, Name = "Office Team", Description = "Office team lunch expenses", CreatedDate = DateTime.Now },
                new Group { Id = 3, Name = "Rishikesh Tour", Description = "Tour expenses", CreatedDate = DateTime.Now },
                new Group { Id = 4, Name = "Kasi Tour", Description = "Kasi expenses", CreatedDate = DateTime.Now },
                new Group { Id = 5, Name = "Bangolre Daires", Description = "Bangolore expenses", CreatedDate = DateTime.Now }
            );

            modelBuilder.Entity<GroupUser>().HasData(
                new { Id=1, GroupId = 1, UserId = 2,Email="sai@gmail.com", IsSettled = false },
                new { Id=2,GroupId = 1, UserId = 3 , Email = "manu@gmail.com", IsSettled = false },
                new {Id=3, GroupId = 2, UserId = 3 , Email = "manu@gmail.com", IsSettled = false }
            );

            modelBuilder.Entity<Expense>().HasData(
    new Expense { Id = 1, Description = "Grocery shopping", Amount = 100.00m, PaidById = 2, Date = DateTime.Now, GroupId = 1 },
    new Expense { Id = 2, Description = "Dinner", Amount = 200.00m, PaidById = 3, Date = DateTime.Now, GroupId = 1 },
    new Expense { Id = 3, Description = "Lunch", Amount = 100.00m, PaidById = 4, Date = DateTime.Now, GroupId = 3 },
    new Expense { Id = 4, Description = "Tea", Amount = 100.00m, PaidById = 2, Date = DateTime.Now, GroupId = 3 }
 
  
);



            modelBuilder.Entity<UserExpense>().HasData(
                new UserExpense { UserExpenseId = 1, UserId = 2, ExpenseId = 1, AmountOwed = 50.00m, IsSettled = false },
                new UserExpense { UserExpenseId = 2, UserId = 3, ExpenseId = 1, AmountOwed = 50.00m, IsSettled = false }
            );
        }
    }
}
