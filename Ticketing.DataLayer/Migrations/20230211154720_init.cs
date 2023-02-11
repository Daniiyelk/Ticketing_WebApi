using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ticketing.DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsFinished = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ticket_User_userId",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketQuestion",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ticketId = table.Column<int>(type: "int", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsResponded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketQuestion", x => x.id);
                    table.ForeignKey(
                        name: "FK_TicketQuestion_Ticket_ticketId",
                        column: x => x.ticketId,
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketAnswer",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    adminId = table.Column<int>(type: "int", nullable: false),
                    ticketId = table.Column<int>(type: "int", nullable: false),
                    questionId = table.Column<int>(type: "int", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketAnswer", x => x.id);
                    table.ForeignKey(
                        name: "FK_TicketAnswer_Admin_adminId",
                        column: x => x.adminId,
                        principalTable: "Admin",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketAnswer_TicketQuestion_questionId",
                        column: x => x.questionId,
                        principalTable: "TicketQuestion",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_TicketAnswer_Ticket_ticketId",
                        column: x => x.ticketId,
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Admin",
                columns: new[] { "id", "Email", "Password" },
                values: new object[] { 1, "Admin@gmail.com", "Admin" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "Name", "Password" },
                values: new object[,]
                {
                    { 1, "User1@gmail.com", "User1", "User1" },
                    { 2, "User2@gmail.com", "User2", "User2" }
                });

            migrationBuilder.InsertData(
                table: "Ticket",
                columns: new[] { "Id", "DateTime", "IsFinished", "Title", "userId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 2, 11, 19, 17, 20, 361, DateTimeKind.Local).AddTicks(6240), false, "رفع باگ", 1 },
                    { 2, new DateTime(2023, 2, 11, 19, 17, 20, 361, DateTimeKind.Local).AddTicks(6246), true, "مدیریت خطا ها", 1 }
                });

            migrationBuilder.InsertData(
                table: "TicketQuestion",
                columns: new[] { "id", "Body", "DateTime", "IsResponded", "ticketId" },
                values: new object[] { 1, "لطفا باگ صفحه ورود را برطرف کنید", new DateTime(2023, 2, 11, 19, 17, 20, 361, DateTimeKind.Local).AddTicks(6277), true, 1 });

            migrationBuilder.InsertData(
                table: "TicketAnswer",
                columns: new[] { "id", "Body", "DateTime", "adminId", "questionId", "ticketId" },
                values: new object[] { 1, "در حال بررسی میباشد لطفا صبور باشید", new DateTime(2023, 2, 11, 19, 17, 20, 361, DateTimeKind.Local).AddTicks(6300), 1, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_userId",
                table: "Ticket",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAnswer_adminId",
                table: "TicketAnswer",
                column: "adminId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAnswer_questionId",
                table: "TicketAnswer",
                column: "questionId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketAnswer_ticketId",
                table: "TicketAnswer",
                column: "ticketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketQuestion_ticketId",
                table: "TicketQuestion",
                column: "ticketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketAnswer");

            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "TicketQuestion");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
