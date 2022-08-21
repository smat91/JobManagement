using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class AddCustomerNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "InvoiceRequest");

            migrationBuilder.AddColumn<string>(
                name: "CustomerNumber",
                table: "InvoiceRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerNumber",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerNumber",
                table: "InvoiceRequest");

            migrationBuilder.DropColumn(
                name: "CustomerNumber",
                table: "Customers")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "CustomersHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "InvoiceRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
