using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftLogger.Migrations
{
    /// <inheritdoc />
    public partial class DurationChange3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Duration",
                table: "Shifts",
                type: "time",
                nullable: false,
                computedColumnSql: "CONVERT(TIME,CONVERT(DATETIME, [End]) - CONVERT(DATETIME, [Start]))",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Duration",
                table: "Shifts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldComputedColumnSql: "CONVERT(TIME,CONVERT(DATETIME, [End]) - CONVERT(DATETIME, [Start]))");
        }
    }
}
