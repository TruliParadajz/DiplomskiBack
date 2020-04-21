using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApi.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventTasks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDt = table.Column<DateTime>(nullable: false),
                    EndDt = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Colour = table.Column<string>(nullable: true),
                    Resizable = table.Column<bool>(nullable: true),
                    Draggable = table.Column<bool>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventTasks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    EmailNotification = table.Column<bool>(nullable: false),
                    AppNotification = table.Column<bool>(nullable: false),
                    Hours = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserNotifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventTaskNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserNotificationId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    EventTaskId = table.Column<int>(nullable: false),
                    EmailNotification = table.Column<int>(nullable: false),
                    AppNotification = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTaskNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventTaskNotifications_EventTasks_EventTaskId",
                        column: x => x.EventTaskId,
                        principalTable: "EventTasks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EventTaskNotifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EventTaskNotifications_UserNotifications_UserNotificationId",
                        column: x => x.UserNotificationId,
                        principalTable: "UserNotifications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventTaskNotifications_EventTaskId",
                table: "EventTaskNotifications",
                column: "EventTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_EventTaskNotifications_UserId",
                table: "EventTaskNotifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EventTaskNotifications_UserNotificationId",
                table: "EventTaskNotifications",
                column: "UserNotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_EventTasks_UserId",
                table: "EventTasks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotifications_UserId",
                table: "UserNotifications",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventTaskNotifications");

            migrationBuilder.DropTable(
                name: "EventTasks");

            migrationBuilder.DropTable(
                name: "UserNotifications");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
