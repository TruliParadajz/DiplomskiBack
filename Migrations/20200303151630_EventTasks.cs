﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendApi.Migrations
{
    public partial class EventTasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventTask",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDt = table.Column<DateTime>(nullable: false),
                    EntDt = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Colour = table.Column<string>(nullable: true),
                    AllDay = table.Column<bool>(nullable: true),
                    Resizable = table.Column<bool>(nullable: true),
                    Draggable = table.Column<bool>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventTask_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventTask_UserId",
                table: "EventTask",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventTask");
        }
    }
}
