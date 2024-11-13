using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseApp.Migrations
{
    public partial class dataaa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaidById = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    IsSettled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expenses_Users_PaidById",
                        column: x => x.PaidById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUsers", x => new { x.UserId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_GroupUsers_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserExpenses",
                columns: table => new
                {
                    UserExpenseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ExpenseId = table.Column<int>(type: "int", nullable: false),
                    AmountOwed = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsSettled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExpenses", x => x.UserExpenseId);
                    table.ForeignKey(
                        name: "FK_UserExpenses_Expenses_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserExpenses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "CreatedDate", "Description", "Email", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2636), "Expenses shared among roommates", "", "Roommates" },
                    { 2, new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2637), "Office team lunch expenses", "", "Office Team" },
                    { 3, new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2638), "Tour expenses", "", "Rishikesh Tour" },
                    { 4, new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2639), "Kasi expenses", "", "Kasi Tour" },
                    { 5, new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2640), "Bangolore expenses", "", "Bangolre Daires" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedOn", "Email", "MobileNumber", "Name", "Password", "RefreshToken", "RefreshTokenExpiryTime", "Token", "UserType" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2578), "admin@gmail.com", "1234567890", "Admin", "admin1234", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1 },
                    { 2, new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2597), "sai@gmail.com", "1234567890", "Sai", "sai1234", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { 3, new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2598), "manu@gmail.com", "1234567890", "Manu", "manu4321", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { 4, new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2599), "ramu@gmail.com", "123456789", "Ramu", "ramu4321", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { 5, new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2600), "raj@gmail.com", "1234567890", "Raj", "raj1234", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { 6, new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2601), "mice@gmail.com", "1234567890", "Mice", "sai1234", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { 7, new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2602), "kin@gmail.com", "1234567890", "Kin", "kin1234", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { 8, new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2603), "kapil@gmail.com", "1234567890", "Kapil", "kapil1234", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { 9, new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2604), "kesav@gmail.com", "1234567890", "Kesav", "kesav1234", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2 },
                    { 10, new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2605), "Bob@gmail.com", "1234567890", "Bob", "bob1234", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2 }
                });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Amount", "Date", "Description", "GroupId", "IsSettled", "PaidById" },
                values: new object[,]
                {
                    { 1, 100.00m, new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2673), "Grocery shopping", 1, false, 2 },
                    { 2, 200.00m, new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2674), "Dinner", 1, false, 3 },
                    { 3, 100.00m, new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2675), "Lunch", 3, false, 4 },
                    { 4, 100.00m, new DateTime(2024, 10, 2, 11, 49, 29, 145, DateTimeKind.Local).AddTicks(2677), "Tea", 3, false, 2 }
                });

            migrationBuilder.InsertData(
                table: "GroupUsers",
                columns: new[] { "GroupId", "UserId", "Email", "Id" },
                values: new object[,]
                {
                    { 1, 2, "sai@gmail.com", 1 },
                    { 1, 3, "manu@gmail.com", 2 },
                    { 2, 3, "manu@gmail.com", 3 }
                });

            migrationBuilder.InsertData(
                table: "UserExpenses",
                columns: new[] { "UserExpenseId", "AmountOwed", "ExpenseId", "IsSettled", "UserId" },
                values: new object[] { 1, 50.00m, 1, false, 2 });

            migrationBuilder.InsertData(
                table: "UserExpenses",
                columns: new[] { "UserExpenseId", "AmountOwed", "ExpenseId", "IsSettled", "UserId" },
                values: new object[] { 2, 50.00m, 1, false, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_GroupId",
                table: "Expenses",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_PaidById",
                table: "Expenses",
                column: "PaidById");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUsers_GroupId",
                table: "GroupUsers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserExpenses_ExpenseId",
                table: "UserExpenses",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserExpenses_UserId",
                table: "UserExpenses",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupUsers");

            migrationBuilder.DropTable(
                name: "UserExpenses");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
